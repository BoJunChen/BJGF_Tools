using System.Collections.Generic;
using UnityEngine;

namespace BJGF.Tools.FSM
{
    public class BaseFsmState : IState
    {
        public string Name { get { return _name; } }
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public float Timer { get { return _timer; } }
        public IStateMachine Parent { get { return _parent; } }
        public List<ITransition> Transitions { get { return _transitions; } }

        public IState PreState => _preState;
        public IState NextState => _nextState;

        public BaseFsmState(string name)
        {
            _name = name;
            _transitions = new List<ITransition>();
        }

        /// <summary>
        /// 添加状态转换
        /// </summary>
        /// <param name="transition"></param>
        public void AddTransition(ITransition transition)
        {
            if (!_transitions.Contains(transition))
                _transitions.Add(transition);
            else
            {
                // log warining : 该转换已经存在
            }

        }

        /// <summary>
        /// 进入本状态
        /// </summary>
        /// <param name="prev"></param>
        public virtual void OnEnter(IState prev)
        {
            _timer = 0;
            _index = 0;
            _isWork = true;
            _preState = prev;
            _nextState = null;
        }

        /// <summary>
        /// 离开本状态
        /// </summary>
        /// <param name="next"></param>
        public virtual void OnExit(IState next)
        {
            _timer = 0;
            _index = 0;
            _isWork = false;
            _preState = null;
            _nextState = next;
        }

        /// <summary>
        /// 每帧执行
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void OnUpdate(float deltaTime)
        {
            //FQLiteLog.Log($"FSM - CurrentState : {Name}");
            if (!_isWork) return;
            _timer += deltaTime;
            if (_transitions.Count < 1) return;

            // 如果检测能够转换,则进入转换函数中
            if (_workingTransition != null && _workingTransition.OnCheck())
            {
                // 如果转换完成
                if (_workingTransition.OnCompleteCallBack())
                {
                    DoTransition(_workingTransition);
                }
                return;
            }

            _index += 1;
            if (_index >= _transitions.Count) _index = 0;
            _workingTransition = _transitions[_index];
        }

        /// <summary>
        /// 设置本状态的父状态机
        /// </summary>
        /// <param name="stateMachine"></param>
        public void SetStateMachine(IStateMachine stateMachine)
        {
            _parent = stateMachine;
        }

        protected string _tag;
        protected string _name;
        protected float _timer;
        protected bool _isWork;
        protected int _index;
        protected IStateMachine _parent;
        protected List<ITransition> _transitions;
        protected ITransition _workingTransition;
        protected IState _preState;
        protected IState _nextState;

        /// <summary>
        /// 执行转换
        /// </summary>
        /// <param name="transition"></param>
        protected virtual void DoTransition(ITransition transition)
        {
            Parent.CurState.OnExit(transition.ToState);
            Parent.SetCurState(transition.ToState);
            Parent.CurState.OnEnter(transition.FromState);
            Debug.Log($"FSM:{transition.FromState.Name} -> {transition.ToState.Name}");
        }
    }
}
