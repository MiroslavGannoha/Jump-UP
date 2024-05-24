using Animancer;
using UnityEngine;

public sealed class PreparingToJump : CharacterState
{
    [SerializeReference]
    private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;

    public override bool CanInterruptSelf => true;

    private void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }

    private void OnExit()
    {
        Character.Animancer.Stop(_Animation);
    }

    private void Awake()
    {
        _Animation.Events.OnEnd = AnimEndEvent.Invoke;
    }
}
