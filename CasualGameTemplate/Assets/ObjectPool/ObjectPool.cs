using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace A_rosuko.ObjectPool
{
    /// <summary>
    /// ObjectPoolItemコンポーネントがアタッチされたゲームオブジェクトをプールする
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;

        // シングルトン
        public static ObjectPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    // シーン上から取得する
                    _instance = FindObjectOfType<ObjectPool>();

                    if (_instance == null)
                    {
                        // ゲームオブジェクトを作成しObjectPoolコンポーネントを追加
                        _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                    }
                }
                return _instance;
            }
        }

        private Dictionary<int, ObjectPoolItem> _pool = new Dictionary<int, ObjectPoolItem>();
        private Dictionary<int, int> _poolCount = new Dictionary<int, int>();//プール数管理用

        // ゲームオブジェクトをプールから取得、無ければ生成
        public GameObject GetGameObject(GameObject prefab, Vector3 v3_position, Quaternion qt_rotation)
        {
            // プレハブのインスタンスIDをkeyとする
            int key = prefab.GetInstanceID();

            // Dictionaryにkeyが存在しなければ作成
            if (_pool.ContainsKey(key) == false)
            {
                DictionaryAdd(key);
            }

            ObjectPoolItem item = _pool[key].next;

            // プールにあれば
            if (item != null)
            {
                //2個目を先頭につなぐ
                _pool[key].next = item.next;
                item.next = null;

                //カウントを減らす
                --_poolCount[key];

                item._transform.position = v3_position;
                item._transform.rotation = qt_rotation;
                item._gameObject.SetActive(true);
                item._sleep = false;
                item.OnWakeUp();//初期化処理
                return item._gameObject;
            }

            // プールが無いので新たに生成
            GameObject go = Instantiate(prefab, v3_position, qt_rotation);
            ObjectPoolItem itemScript = go.GetComponent<ObjectPoolItem>();
            itemScript.LinkedListRoot = _pool[key];
            itemScript.PrefabID = key;

            //エディタではテストしやすいようにobjectpoolの子にしておく
#if UNITY_EDITOR
            go.transform.parent = gameObject.transform;
#endif

            return go;
        }

        /// <summary>
        /// プールして非アクティブにする。
        /// </summary>
        /// <param name="item"></param>
        public void SleepGameObject(ObjectPoolItem item)
        {
            if (item == null)
            {
                Debug.Log("ObjectPoolItem is null");
                return;
            }
            if (item.LinkedListRoot == null)
            {
                //シーンに直接置いた時など、GetGameObjectしてないのでプール先が無い状態はDestoroy
                Destroy(item._gameObject);
                return;
            }
            if (item._sleep == true) return;//二度寝防止

            //リンクリストに接続
            item.next = item.LinkedListRoot.next;
            item.LinkedListRoot.next = item;

            //接続数を増やす
            ++_poolCount[item.PrefabID];

            //スリープ処理
            item.OnSleep();
            item._sleep = true;
            item._gameObject.SetActive(false);
        }

        /// <summary>
        /// プールして非アクティブにする
        /// GetComponentしてSleepGameObject(ObjectPoolItem)をする(速度的に非推奨)
        /// ObjectPoolItemがなければDestoroy
        /// </summary>
        /// <param name="obj"></param>
        public void SleepGameObject(GameObject obj)
        {
            ObjectPoolItem item = obj.GetComponent<ObjectPoolItem>();
            if (item == null)
            {
                Destroy(obj);
            }
            SleepGameObject(item);
        }
        /// <summary>
        /// 指定フレーム後にプールして非アクティブにする
        /// </summary>
        /// <param name="item"></param>
        /// <param name="delayFrameCount"></param>
        public void SleepGameObject(ObjectPoolItem item, int delayFrameCount)
        {
            item.DelayPool(delayFrameCount);
        }
        /// <summary>
        /// 指定フレーム後にプールして非アクティブにする
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="delayFrameCount"></param>
        public void SleepGameObject(GameObject obj, int delayFrameCount)
        {
            ObjectPoolItem item = obj.GetComponent<ObjectPoolItem>();
            if (item == null)
            {
                Debug.LogError("ObjectPoolItemComponent is null");
                Destroy(obj, delayFrameCount);//フレームではなく時間なので注意
            }
            item.DelayPool(delayFrameCount);
            //SleepGameObject(item, delayFrameCount);
        }

        /// <summary>
        /// 事前にプールする
        /// </summary>
        /// <param name="prefab"></param>
        public void InstantiateSleapPool(GameObject prefab)
        {
            // プレハブのインスタンスIDをkeyとする
            int key = prefab.GetInstanceID();

            // Dictionaryにkeyが存在しなければ作成
            if (_pool.ContainsKey(key) == false)
            {
                DictionaryAdd(key);
            }

            //新たに生成する
            GameObject go = Instantiate(prefab);
            ObjectPoolItem itemScript = go.GetComponent<ObjectPoolItem>();
            itemScript.LinkedListRoot = _pool[key];
            itemScript.PrefabID = key;

            //エディタではテストしやすいようにObjectPoolの子にしておく
#if UNITY_EDITOR
            go.transform.parent = gameObject.transform;
#endif

            SleepGameObject(itemScript);//非アクティブ化
        }

        /// <summary>
        /// プールからオブジェクトを1つ削除
        /// </summary>
        /// <param name="prefab"></param>
        public void DestroyPool(GameObject prefab)
        {
            int key = prefab.GetInstanceID();
            if (_pool.ContainsKey(key) == false) return;//プール無し

            ObjectPoolItem item = _pool[key].next;
            // プールがあれば削除
            if (item != null)
            {
                _pool[key].next = item.next;//2個目を先頭につなぐ
                --_poolCount[key];
                Destroy(item._gameObject);
            }
        }

        /// <summary>
        /// プールしている数を返す
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int CountPool(GameObject prefab)
        {
            int key = prefab.GetInstanceID();
            if (_poolCount.ContainsKey(key) == false) return 0;//キー登録無し
            return _poolCount[key];
        }

        /// <summary>
        /// キーを追加
        /// </summary>
        /// <param name="key"></param>
        private void DictionaryAdd(int key)
        {
            _pool.Add(key, this.gameObject.AddComponent<ObjectPoolItem>());//新たにコンポーネントをキーで追加
            _poolCount.Add(key, 0);//カウント用に追加

            _pool[key].next = null;
            _pool[key].PrefabID = key;
        }

        private void OnDestroy()
        {
            _instance = null;
        }


    }
}
