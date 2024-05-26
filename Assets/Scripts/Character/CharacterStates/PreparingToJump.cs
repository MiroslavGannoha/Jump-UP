using Animancer;
using UnityEngine;

public sealed class PreparingToJump : CharacterState
{
    [SerializeReference]
    private ClipTransition _Animation;

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
        _Animation.Events.OnEnd = AnimEndEvent.Invoke;
    }
}
