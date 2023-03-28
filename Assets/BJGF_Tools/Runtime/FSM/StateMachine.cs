using System.Collections.Generic;
using System.Linq;

namespace BJGF.Tools.FSM
{
    /// <summary>
    /// 可以直接实现分层状态机
    /// </summary>
    public class StateMachine : BaseFsmState, IStateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        public IState CurState { get { return _curState; } }
        /// <summary>
        /// 默认状态
        /// </summary>
        public IState DefaultState { get { return _defaultState; } }
        /// <summary>
        /// 所有状态
        /// </summary>
        public Dictionary<string, IState> States { get { return _states; } }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultState"></param>
        public StateMachine(string name, IState defaultState) : base(name)
        {
            _states = new Dictionary<string, IState>();
            AddState(defaultState);
            _defaultState = defaultState;
            _curState = defaultState;
        }

        /// <summary>
        /// 启动状态机
        /// </summary>
        public void StartMachine()
        {
            _isWork = true;
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(IState state)
        {
            if (state == null) return;

            if (!_states.ContainsKey(state.Name))
            {
                _states.Add(state.Name, state);
                state.SetStateMachine(this);
            }
            else
            {
                // log warining: 已有该 state 状态
            }
        }

        /// <summary>
        /// 获得状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IState GetStateWithName(string name)
        {
            return _states != null ? _states[name] : null;
        }

        /// <summary>
        /// 获得状态
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IState GetStateWithTag(string tag)
        {
            foreach (var state in _states.Values)
            {
                if (state.Tag == tag)
                    return state;
            }
            return null;
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="state"></param>
        public void RemoveState(IState state)
        {
            if (_states.ContainsKey(state.Name)) _states.Remove(state.Name);
            if (_defaultState == _curState && _curState == state)
            {
                _curState = _states.Count > 0 ? _states[_states.Keys.ToArray()[0]] : null;
                _defaultState = _curState;
            }
            else if (_defaultState != _curState && _defaultState == state)
            {
                _defaultState = _curState;
            }
            else if (_curState != _defaultState && _curState == state)
            {
                _curState = _states.Count > 0 ? _states[_states.Keys.ToArray()[0]] : null;
            }
            else if (_defaultState == null && _curState != null)
            {
                _defaultState = _curState;
            }
        }

        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="state"></param>
        public void SetCurState(IState state)
        {
            if (state == null)
            {
                // log warining : 传入的 state 为空！
                return;
            }

            _curState = state;
        }

        public override void OnEnter(IState prev)
        {
            base.OnEnter(prev);
            _curState.OnEnter(prev);
        }

        public override void OnExit(IState next)
        {
            base.OnExit(next);
            _curState.OnExit(next);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (_curState == null) { return; }
            _curState.OnUpdate(deltaTime);
        }

        public void Clear()
        {
            _states.Clear();
            _curState = null;
            _defaultState = null;
        }

        protected IState _curState;
        protected IState _defaultState;
        protected Dictionary<string, IState> _states;
    }
}
