using Animancer;
using UnityEngine;

public sealed class RotateRightState : CharacterRotationState
{
    [SerializeReference]
    private ClipTransitionAsset _Anim;

    // [SerializeReference]
    // private ClipTransition _Jump;
    public override CharacterStatePriority Priority => CharacterStatePriority.High;
    public override CharacterRotationDirection Direction => CharacterRotationDirection.Right;


    public override bool CanInterruptSelf => false;

    public override void OnEnter()
    {
        // Character.Animancer.Layers[0].Play(_Jump);
        Character.Animancer.Layers[1].Play(_Anim);
    }

    private void Awake()
    {
        _Anim.Transition.Events.OnEnd = () =>
        {
            // Character.Animancer.Stop(_Jump);
            // Character.Animancer.Stop(_Rotate);
            AnimEndEvent.Invoke();
        };
    }
}