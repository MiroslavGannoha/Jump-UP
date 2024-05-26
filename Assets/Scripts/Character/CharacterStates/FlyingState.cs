using Animancer;
using UnityEngine;

public sealed class FlyingState : CharacterState
{
    [SerializeField]
    private ClipTransitionAsset _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;

    public override bool CanInterruptSelf => true;

    public override void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }
}
