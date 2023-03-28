namespace BJGF.Tools.Event
{
    /// <summary>
    /// 事件控制接口
    /// </summary>
    public interface IEventController
    {

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        void AddEvent(string eventName, EventAction action);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        void RemoveEvent(string eventName, EventAction action);

        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        void SendEvent(string eventName, IEventMessage msg);

        /// <summary>
        /// 清理事件
        /// </summary>
        /// <param name="eventName"></param>
        void ClearEvent(string eventName);

        /// <summary>
        /// 清理所有
        /// </summary>
        void ClearAll();
    }
}
