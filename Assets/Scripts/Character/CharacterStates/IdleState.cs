using Animancer;
using UnityEngine;

public sealed class IdleState : CharacterState
{
    [SerializeField] private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;
    public override void OnEnter()
    {
        Character.Animancer.Play(_Animation);
    }
}