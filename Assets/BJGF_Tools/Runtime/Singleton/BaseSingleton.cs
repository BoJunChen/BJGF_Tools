using System;

namespace BJGF.Tools.Singleton
{
    public abstract class BaseSingleton<T> where T : class, new()
    {
        /// <summary>
        /// 创建单例
        /// </summary>
        public static T CreateInstance()
        {
            if (_ins == null)
            {
                lock (_lock)
                {
                    if (_ins == null) _ins = Activator.CreateInstance<T>();
                }
            }
            return _ins;
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static T Ins => _ins;

        public void Dispose()
        {
            OnDispose();
            _ins = null;
        }

        protected virtual void OnDispose()
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        protected BaseSingleton()
        {
            _ins = this as T;
        }

        private static T _ins;
        private static readonly object _lock = new object();

    }
}
