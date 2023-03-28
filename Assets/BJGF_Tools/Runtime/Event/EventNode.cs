using BJGF.Tools.Pool;

namespace BJGF.Tools.Event
{
    /// <summary>
    /// 事件委托
    /// </summary>
    /// <param name="args"></param>
    public delegate void EventAction(IEventMessage msg);


    /// <summary>
    /// 事件节点
    /// </summary>
    internal class EventNode
    {
        public static EventNode GetEventNode(string name)
        {
            var node = _pool.GetObject<EventNode>(EVENTNODE, name);
            return node;
        }

        public static void Release(EventNode node)
        {
            _pool.Recycle(EVENTNODE, node);
        }

        /// <summary>
        /// 事件名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void AddEvent(EventAction action)
        {
            if (action == null) return;

            _action += action;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="action"></param>
        /// <returns> [true 此事件下无注册函数了] [false 此事件下还有注册函数] </returns>
        public bool RemoveEvent(EventAction action)
        {
            if (action == null) return false;

            _action -= action;
            var count = _action.GetInvocationList().Length;
            return count == 0;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="args"></param>
        public void DoEvent(IEventMessage msg)
        {
            _action(msg);
        }

        /// <summary>
        /// 清理事件
        /// </summary>
        public void Clear()
        {
            _action = null;
        }

        private EventNode(string name)
        {
            _name = name;
        }

        private string _name;
        private event EventAction _action;
        private const string EVENTNODE = "EventNode";


        #region pool
        private static EventNode Create(params object[] args)
        {
            var node = new EventNode((string)args[0]);
            return node;
        }

        private static void Init(object obj)
        {
            var node = obj as EventNode;

        }

        private static void Release(object obj)
        {
            var node = obj as EventNode;
            node.Clear();
        }

        static EventNode()
        {
            _pool = ObjectPool.GetPool("EventNode_Pool", Create, Init, Release, null);
        }
        private static ObjectPool _pool;
        #endregion
    }
}


