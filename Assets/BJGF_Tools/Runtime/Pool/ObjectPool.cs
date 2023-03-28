using System;
using System.Collections.Generic;



namespace BJGF.Tools.Pool
{
    /// <summary>
    /// 对象创建函数
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public delegate object CreateFunc(params object[] args);

    /// <summary>
    /// 对象池
    /// </summary>
    public class ObjectPool
    {
        /// <summary>
        /// 获取对象池
        /// </summary>
        /// <param name="name"> 对象池名 </param>
        /// <param name="create"> 对象创建函数 </param>
        /// <param name="init"> 对象初始化 </param>
        /// <param name="release"> 对象释放函数 </param>
        /// <param name="destroy"> 对象卸载函数 </param>
        /// <returns> 对象池 </returns>
        public static ObjectPool GetPool(string name, CreateFunc create, Action<object> init, Action<object> release, Action<object> destroy)
        {
            var pool = new ObjectPool(name);
            pool._createFunc = create;
            pool._init = init;
            pool._release = release;
            pool._destroy = destroy;

            return pool;
        }

        /// <summary>
        /// 对象池名
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 设置容量
        /// 如果不设置，则代表无线容量
        /// </summary>
        /// <param name="capacity"></param>
        public void SetCapacity(int capacity)
        {
            _capacity = capacity;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"> 对象类型 </typeparam>
        /// <param name="name"> 对象名 </param>
        /// <param name="args"> 获取时创建参数 </param>
        /// <returns> 对象 </returns>
        public T GetObject<T>(string name, params object[] args) where T : class
        {
            _pools.TryGetValue(name, out var queue);
            object obj = null;
            if (queue == null || queue.Count == 0)
            {
                obj = _createFunc?.Invoke(args);
            }
            else
            {
                obj = queue.Dequeue();
            }

            _init?.Invoke(obj);
            return obj as T;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void Recycle(string name, object obj)
        {
            _release?.Invoke(obj);

            if (!_pools.ContainsKey(name))
            {
                _pools.Add(name, new Queue<object>());
            }

            _pools.TryGetValue(name, out var queue);
            if (_capacity == 0 || queue.Count < _capacity)
            {
                queue.Enqueue(obj);
            }
            else
            {
                _destroy?.Invoke(obj);
            }
        }

        /// <summary>
        /// 清理某类对象池
        /// </summary>
        /// <param name="name"></param>
        public void ClearClass(string name)
        {
            _pools.TryGetValue(name, out var queue);
            if (queue == null || queue.Count == 0) return;

            foreach (var obj in queue)
            {
                _destroy?.Invoke(obj);
            }
            _pools.Remove(name);
        }

        /// <summary>
        /// 清理所有对象池
        /// </summary>
        public void ClearAll()
        {
            var keys = _pools.Keys;
            foreach (var key in keys)
            {
                foreach (var obj in _pools[key])
                {
                    _destroy?.Invoke(obj);
                }
            }

            _pools.Clear();
        }


        private ObjectPool(string name)
        {
            _name = name;
            _pools = new Dictionary<string, Queue<object>>();
        }

        private int _capacity;

        private string _name;
        private Action<object> _init;
        private CreateFunc _createFunc;
        private Action<object> _release;
        private Action<object> _destroy;
        protected Dictionary<string, Queue<object>> _pools;
    }
}
