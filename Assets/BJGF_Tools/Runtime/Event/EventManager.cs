
namespace BJGF.Tools.Event
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager : IEventController
    {
        /// <summary>
        /// 事件管理器名
        /// </summary>
        public string Name { get; protected set; }


        public void OnEditorGUI()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnExit()
        {

        }

        public EventManager()
        {
            _eventCtrl = new EventController(EVENTCTRLNAME);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void AddEvent(string eventName, EventAction action)
        {
            _eventCtrl.AddEvent(eventName, action);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void RemoveEvent(string eventName, EventAction action)
        {
            _eventCtrl.RemoveEvent(eventName, action);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="msg"></param>
        public void SendEvent(string eventName, IEventMessage msg)
        {
            _eventCtrl.SendEvent(eventName, msg);
        }

        /// <summary>
        /// 清理事件
        /// </summary>
        /// <param name="eventName"></param>
        public void ClearEvent(string eventName)
        {
            _eventCtrl.ClearEvent(eventName);
        }

        /// <summary>
        /// 清理所有
        /// </summary>
        public void ClearAll()
        {
            _eventCtrl.ClearAll();
        }

        private EventController _eventCtrl;
        private const string EVENTCTRLNAME = "EventMgrCtrl";
    }
}




