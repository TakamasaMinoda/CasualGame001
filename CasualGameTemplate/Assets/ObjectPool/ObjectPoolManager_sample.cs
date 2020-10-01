using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace A_rosuko.ObjectPool
{
    /// <summary>
    /// Updateのタイミングでオブジェクトプール数を調整する
    /// /// </summary>
    public class ObjectPoolManager_sample : MonoBehaviour
    {
        [SerializeField]
        public List<PoolPrefab> poolList = new List<PoolPrefab>();

        [System.SerializableAttribute]
        public class PoolPrefab
        {
            public Object poolPrefab;
            public int init = 30;
            public int min = 8;
            public int max = 400;
            [HideInInspector]
            public int counter;
        }
        void Awake()
        {
            Init();
        }
        void Update()
        {
            Adjust();
        }

        /// <summary>
        /// 初期設定の数値確認
        /// </summary>
        private void Init()
        {
            for (int i = 0; i < poolList.Count; ++i)
            {
                NunmerChack(poolList[i]);
            }
        }

        private void NunmerChack(PoolPrefab prefab)
        {
            //初期プールより最大数が少ない場合は設定を同じにする
            if (prefab.init > prefab.max)
            {
                prefab.max = prefab.init;
            }
            //最小数が最大数より大きい場合は設定を同じにする
            if (prefab.min > prefab.max)
            {
                prefab.min = prefab.max;
            }

            if (prefab.poolPrefab == null)
            {
                Debug.Log("ObjectPoolManager.poolPrefab is none");
            }
        }

        /// <summary>
        /// プール数を1個づつ調整
        /// </summary>
        public void Adjust()
        {
            //foreach(PoolPrefab poolPrefab in poolList)
            for (int i = 0; i < poolList.Count; ++i)
            {
                PoolPrefab poolPrefab = poolList[i];

                if (poolPrefab.counter < poolPrefab.init)
                {
                    poolPrefab.counter += 1;
                    ObjectPool.Instance.InstantiateSleapPool((GameObject)poolPrefab.poolPrefab);
                    return;
                }
                else
                {
                    int count = ObjectPool.Instance.CountPool((GameObject)poolPrefab.poolPrefab);
                    if (poolPrefab.min > count)
                    {
                        ObjectPool.Instance.InstantiateSleapPool((GameObject)poolPrefab.poolPrefab);
                        return;
                    }
                    else if (count > poolPrefab.max)
                    {
                        ObjectPool.Instance.DestroyPool((GameObject)poolPrefab.poolPrefab);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// プールするプレハブをリストに追加(同じプレハブは設定を上書き)
        /// </summary>
        /// <param name="poolPrefab"></param>
        /// <param name="init">初期作成数</param>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        public void AddPoolList(GameObject poolPrefab, int init, int min, int max)
        {
            if (poolPrefab == null) return;

            PoolPrefab new_PoolPrefab = GetPoolPrefab(poolPrefab);
            new_PoolPrefab.poolPrefab = poolPrefab;
            new_PoolPrefab.init = init;
            new_PoolPrefab.min = min;
            new_PoolPrefab.max = max;
            NunmerChack(new_PoolPrefab);
        }

        /// <summary>
        /// リストから同じプレハブを探す、無ければリストに追加する。
        /// </summary>
        private PoolPrefab GetPoolPrefab(GameObject poolPrefab)
        {
            for (int i = 0; i < poolList.Count; ++i)
            {
                if (poolList[i].poolPrefab == poolPrefab)
                {
                    return poolList[i];
                }
            }
            PoolPrefab new_PoolPrefab = new PoolPrefab();
            poolList.Add(new_PoolPrefab);
            return new_PoolPrefab;
        }

        public void RemovePoolList(GameObject poolPrefab)
        {
            if (poolPrefab == null) return;

            for (int i = 0; i < poolList.Count; ++i)
            {
                if (poolList[i].poolPrefab == poolPrefab)
                {
                    poolList.RemoveAt(i);
                    return;
                }
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ObjectPoolManager_sample))]
    public class ObjectPoolManager_testEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ObjectPoolManager_sample t = this.target as ObjectPoolManager_sample;
            GUILayout.BeginHorizontal();
            int count = EditorGUILayout.IntField(t.poolList.Count, GUILayout.MaxWidth(100));
            if (GUILayout.Button("+"))
            {
                EditorUtility.SetDirty(t);
                t.poolList.Add(new ObjectPoolManager_sample.PoolPrefab());
            }
            if (GUILayout.Button("-"))
            {
                EditorUtility.SetDirty(t);
                t.poolList.RemoveAt(t.poolList.Count - 1);
            }
            GUILayout.EndHorizontal();
            foreach (ObjectPoolManager_sample.PoolPrefab item in t.poolList)
            {
                GUILayout.BeginHorizontal();
                item.poolPrefab = EditorGUILayout.ObjectField(item.poolPrefab, typeof(GameObject), false, GUILayout.MinWidth(250f));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("init:", GUILayout.MaxWidth(32f));
                item.init = EditorGUILayout.IntField(item.init, GUILayout.MaxWidth(100f));
                EditorGUILayout.LabelField("min:", GUILayout.MaxWidth(32f));
                item.min = EditorGUILayout.IntField(item.min, GUILayout.MaxWidth(100f));
                EditorGUILayout.LabelField("max:", GUILayout.MaxWidth(32f));
                item.max = EditorGUILayout.IntField(item.max, GUILayout.MaxWidth(100f));
                GUILayout.EndHorizontal();
            }

            DrawDefaultInspector();
        }
    }
#endif
}