using Animancer;
using UnityEngine;

public sealed class JumpState : CharacterState
{
    [SerializeField]
    private ClipTransitionAsset _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => false;

    public override void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }

    private void Awake()
    {
        _Animation.Transition.Events.OnEnd = () =>
        {
            Character.Animancer.Stop(_Animation);
            AnimEndEvent.Invoke();
        };
    }
}
