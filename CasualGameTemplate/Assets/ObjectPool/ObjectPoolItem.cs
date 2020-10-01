using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace A_rosuko.ObjectPool
{
    public sealed class ObjectPoolItem : MonoBehaviour
    {
        [HideInInspector]
        public int PrefabID;                        //プレハブのIDをキャッシュ
        [HideInInspector]
        public ObjectPoolItem LinkedListRoot;       //接続先をキャッシュ
        [HideInInspector]
        public ObjectPoolItem next;                 //片方向リンクリスト
        [HideInInspector]
        public bool _sleep = false;                 //二度寝防止

        public UnityEvent OnEventWakeUp;            //初期化イベント登録用
        public UnityEvent OnEventSleep;             //スリープイベント登録用
        public UnityEvent OnEventPreSleep;          //遅延スリープイベント登録用

        [HideInInspector]
        public GameObject _gameObject;  //キャッシュ
        [HideInInspector]
        public Transform _transform;    //キャッシュ
        [HideInInspector]
        public Rigidbody _rigidbody;   //キャッシュ

        private void Awake()
        {
            _gameObject = gameObject;
            _transform = transform;

            //Rigidbodyがアタッチされていれば初期化に追加
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            if (_rigidbody != null) OnEventPreSleep.AddListener(RigidbodyInit);
        }

        //再利用時の初期化
        public void OnWakeUp()
        {
            OnEventWakeUp.Invoke();
        }

        //スリープ用
        public void OnSleep()
        {
            OnEventSleep.Invoke();
        }

        //物理停止
        public void RigidbodyInit()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.ResetInertiaTensor();
        }

        public void DelayPool(int delayFrameCount)
        {
            OnEventPreSleep.Invoke();
            StartCoroutine(DelayPoolCoroutine(delayFrameCount));
        }

        /// <summary>
        /// 指定フレーム後にプールする
        /// </summary>
        /// <param name="delayFrameCount"></param>
        /// <returns></returns>
        private IEnumerator DelayPoolCoroutine(int delayFrameCount)
        {
            for (var i = 0; i < delayFrameCount; i++)
            {
                yield return null;
            }
            ObjectPool.Instance.SleepGameObject(this);
        }
    }
}

