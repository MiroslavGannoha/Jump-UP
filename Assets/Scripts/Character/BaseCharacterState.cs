using Animancer.FSM;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterStatePriority
{
    // Enums are ints starting at 0 by default.
    Low, // Could specify "Low = 0," if we want to be explicit or change the order.
    Medium, // Medium = 1,
    High, // High = 2,
    // This means you can compare them with numerical operators like < and >.
}

public abstract class CharacterState : StateBehaviour
{
    [System.Serializable]
    public class StateMachine : StateMachine<CharacterState>.WithDefault { }

    [SerializeField]
    private Character _Character;
    public Character Character => _Character;

    // #if UNITY_EDITOR
    //     protected override void OnValidate()
    //     {
    //         base.OnValidate();

    //         gameObject.Get(ref _Character);
    //     }
    // #endif

    public virtual CharacterStatePriority Priority => CharacterStatePriority.Low;

    public virtual bool CanInterruptSelf => false;
    public UnityEvent CompleteEvent = new UnityEvent();

    public override bool CanExitState
    {
        get
        {
            // There are several different ways of accessing the state change details:
            // var nextState = StateChange<CharacterState>.NextState;
            // var nextState = this.GetNextState();
            var nextState = _Character.StateMachine.NextState;
            if (nextState == this)
                return CanInterruptSelf;
            else if (Priority == CharacterStatePriority.Low)
                return true;
            else
                return nextState.Priority > Priority;
        }
    }
}
