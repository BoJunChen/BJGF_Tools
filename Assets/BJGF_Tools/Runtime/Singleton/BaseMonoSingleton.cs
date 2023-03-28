using System;
using UnityEngine;

namespace BJGF.Tools.Singleton
{
    public abstract class BaseMonoSingleton<T> : MonoBehaviour, IDisposable where T : MonoBehaviour
    {
        public static T CreateMonoInstance(GameObject root = null)
        {
            var name = typeof(T).Name;

            if (root == null)
            {
                root = GameObject.Find(name);
                if (root == null)
                {
                    root = new GameObject(name);
                }
            }

            _ins = root.GetComponent<T>();
            if (_ins == null)
            {
                _ins = root.AddComponent<T>();
            }

            return _ins;
        }

        public static T Ins => _ins;

        public void Dispose()
        {
            OnDispose();
        }


        protected virtual void Awake()
        {
            if (_ins == null) _ins = this as T;
        }

        protected virtual void OnDestroy()
        {
            Dispose();
            _ins = null;
        }

        protected virtual void OnDispose()
        {

        }

        protected static T _ins;
    }
}
