using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//他の人のソースコードを参考にして、自身で修正を施した

namespace AIBOSSCreator
{
public abstract class StateFulObjectBase<T, TEnum> : MonoBehaviour
where T : class where TEnum : System.IConvertible
{
        protected List<State<T>> stateList = new List<State<T>>();

        protected StateMachine<T> stateMachine;
        public virtual void ChangeState(TEnum state)
        {
            if(stateMachine == null)
            {
                return;
            }
            stateMachine.ChangeState(stateList[state.ToInt32(null)]);
        }
        public virtual bool isCrtState(TEnum state)
        {
            if(stateMachine == null)
            {
                return false;
            }
            return stateMachine.CurrentState ==stateList[ state.ToInt32(null)];
        }
        protected virtual void Update()
        {
            if(stateMachine != null)
            {
                stateMachine.Update();
            } 
        }
}
}


// This Namespace is use for Management The State
// And StateMachine
namespace AIBOSSCreator
{
    // Mainly use for Statement 
    //No Doing Something
    public class State<T>
    {
        protected T owner;
        public State(T owner)
        {
            this.owner = owner;
        }
        public virtual void OnEnterState() { }
        public virtual void Execute() { }
        public virtual void OnExitState() { }
    }
    // Mainly is use for Management the BOSS StateMachine
    public class StateMachine<T>
    {
        State<T> crtState;
        public StateMachine()
        {
            crtState = null;
        }
        public State<T> CurrentState
        {
            get { return crtState; }
        }
        public void ChangeState(State<T> state)
        {

            if (crtState != null)
            {
                crtState.OnExitState();
            }
            crtState = state;
            crtState.OnEnterState();
        }
        public void Update()
        {
            if (crtState != null)
            {
                crtState.Execute();
            }
        }
    }
}



