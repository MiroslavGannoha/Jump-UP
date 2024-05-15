using Animancer;
using UnityEngine;

public sealed class IdleState : CharacterState
{
    [SerializeField] private ClipTransition _Animation;

    private void OnEnable()
    {
        Character.Animancer.Play(_Animation);
    }
}