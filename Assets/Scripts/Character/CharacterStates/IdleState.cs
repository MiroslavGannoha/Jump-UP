using Animancer;
using UnityEngine;

public sealed class IdleState : CharacterState
{
    [SerializeField] private ClipTransition _Animation;

    public override CharacterStatePriority Priority => CharacterStatePriority.Low;
    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }
}