using Animancer;
using UnityEngine;

public sealed class LookLeftState : CharacterRotationState
{
    [SerializeReference]
    private ClipTransitionAsset _Anim;

    public override bool CanInterruptSelf => false;
    public override CharacterRotationDirection Direction => CharacterRotationDirection.Left;

    public override void OnEnter()
    {
        Character.Animancer.Layers[1].Play(_Anim);
    }
}
