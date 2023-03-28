using System.Collections.Generic;

namespace BJGF.Tools.Event
{
    /// <summary>
    /// 事件控制器
    /// </summary>
    public class EventController : IEventController
    {
        public string Name => _name;

        public EventController(string name)
        {
            _name = name;
            _events = new Dictionary<string, EventNode>();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void AddEvent(string eventName, EventAction action)
        {
            if (_events.TryGetValue(eventName, out var node))
            {
                node.AddEvent(action);
                return;
            }

            var tempNode = EventNode.GetEventNode(eventName);
            tempNode.Name = eventName;
            tempNode.AddEvent(action);
            _events.Add(eventName, tempNode);
        }
        
        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void RemoveEvent(string eventName, EventAction action)
        {
            if (!_events.ContainsKey(eventName)) return;

            var node = _events[eventName];

            var isClear = false;
            isClear = node.RemoveEvent(action);
            if (!isClear) return;

            _events.Remove(eventName);
            EventNode.Release(node);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="msg"></param>
        public void SendEvent(string eventName, IEventMessage msg)
        {
            _events.TryGetValue(eventName, out var node);
            node?.DoEvent(msg);
        }

        /// <summary>
        /// 清理事件
        /// </summary>
        /// <param name="eventName"></param>
        public void ClearEvent(string eventName)
        {
            if (!_events.ContainsKey(eventName)) return;

            var node = _events[eventName];
            EventNode.Release(node);

            _events.Remove(eventName);            
        }

        /// <summary>
        /// 清理所有
        /// </summary>
        public void ClearAll()
        {
            var keys = _events.Keys;
            foreach (var key in keys)
            {
                ClearEvent(key);
            }
            _events.Clear();
        }

        private string _name;
        private Dictionary<string, EventNode> _events;
    }
}
