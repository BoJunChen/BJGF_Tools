using System.Collections.Generic;

namespace BJGF.Tools.FSM
{
    public interface IState
    {
        float Timer { get; }
        string Name { get; }
        string Tag { get; set; }
        void OnEnter(IState prev);
        void OnExit(IState next);
        void OnUpdate(float deltaTime);
        List<ITransition> Transitions { get; }
        void AddTransition(ITransition transition);
        IStateMachine Parent { get; }
        void SetStateMachine(IStateMachine stateMachine);
    }
}
