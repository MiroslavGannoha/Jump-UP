using Animancer;
using UnityEngine;

public sealed class JumpState : CharacterState
{
    [SerializeField]
    private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => false;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        // _Animation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
        _Animation.Events.OnEnd = () =>
        {
            Character.Animancer.Stop(_Animation);
            AnimEndEvent.Invoke();
        };
    }
}
