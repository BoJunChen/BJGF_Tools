namespace BJGF.Tools.FSM
{
    /// <summary>
    /// 分层状态机
    /// Hierarchy Finite State Machine
    /// 使用 StateMachine 即可实现分层状态机
    /// </summary>
    public class HFSM : StateMachine
    {
        public HFSM(string name, IState defaultState) : base(name, defaultState)
        {

        }
    }
}
