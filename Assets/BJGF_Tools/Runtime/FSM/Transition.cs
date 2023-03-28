namespace BJGF.Tools.FSM
{
    public class Transition : ITransition
    {
        // todo: 增加一个优先级属性

        public Transition(string name, IState from, IState to)
        {
            _to = to;
            _from = from;
            _name = name;
        }

        public string Name { get { return _name; } }
        public IState ToState { get { return _to; } }
        public IState FromState { get { return _from; } }

        /// <summary>
        /// 检测能否转换
        /// </summary>
        /// <returns></returns>
        public virtual bool OnCheck()
        {
            return true;
        }

        /// <summary>
        /// 检测转换函数是否执行完毕
        /// </summary>
        /// <returns></returns>
        public virtual bool OnCompleteCallBack()
        {
            return true;
        }

        protected IState _from;
        protected IState _to;
        protected string _name;
    }
}
