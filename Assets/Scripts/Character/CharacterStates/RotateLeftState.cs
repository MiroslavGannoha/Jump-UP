using Animancer;
using UnityEngine;

public sealed class RotateLeftState : CharacterRotationState
{
    [SerializeReference]
    private ClipTransition _RotateAnim;
    public override CharacterRotationDirection Direction => CharacterRotationDirection.Left;

    // [SerializeReference]
    // private ClipTransition _Jump;

    public override CharacterStatePriority Priority => CharacterStatePriority.High;

    public override bool CanInterruptSelf => false;

    public override void OnEnter()
    {
        // Character.Animancer.Layers[0].Play(_Jump);
        Character.Animancer.Layers[1].Play(_RotateAnim);
        // state.Weight = 0.5f;
        // jumpState.NormalizedTime = 0;
        // state.NormalizedTime = 0;
    }

    private void Awake()
    {
        _RotateAnim.Events.OnEnd = () =>
        {
            // Character.Animancer.Stop(_Jump);
            // Character.Animancer.Stop(_Rotate);
            AnimEndEvent.Invoke();
        };
    }
}
