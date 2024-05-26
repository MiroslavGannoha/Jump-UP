using Animancer;
using UnityEngine;

public sealed class PreparingToJump : CharacterState
{
    [SerializeReference]
    private ClipTransitionAsset _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;

    public override bool CanInterruptSelf => true;

    public override void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }

    // public override void OnExit()
    // {
        // Character.Animancer.Stop(_Animation);
    // }

    private void Awake()
    {
        _Animation.Transition.Events.OnEnd = AnimEndEvent.Invoke;
    }
}
