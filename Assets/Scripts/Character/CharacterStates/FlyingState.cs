using Animancer;
using UnityEngine;

public sealed class FlyingState : CharacterState
{
    [SerializeField]
    private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;

    public override bool CanInterruptSelf => true;

    public override void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        // _Animation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
    }
}
