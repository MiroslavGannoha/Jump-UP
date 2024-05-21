
using Animancer.FSM;

public enum CharacterRotationDirection
{
    Left,
    Right,
}

public abstract class CharacterRotationState : CharacterState
{
    [System.Serializable]
    new public class StateMachine : StateMachine<CharacterRotationState>.WithDefault {}
    public virtual CharacterRotationDirection Direction => CharacterRotationDirection.Right;

    public override bool CanExitState
    {
        get
        {
            // There are several different ways of accessing the state change details:
            // var nextState = StateChange<CharacterState>.NextState;
            // var nextState = this.GetNextState();
            var nextState = Character.RotationStateMachine.NextState;

            if(nextState.Direction == Direction){
                return false;
            }
            if (nextState == this)
                return CanInterruptSelf;
            else if (Priority == CharacterStatePriority.Low)
                return true;
            else
                return nextState.Priority > Priority;
        }
    }
}
