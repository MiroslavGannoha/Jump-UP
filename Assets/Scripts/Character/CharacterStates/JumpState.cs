using Animancer;
using UnityEngine;

public sealed class ActionState : CharacterState
{
    [SerializeField]
    private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => true;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        _Animation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
    }
}
