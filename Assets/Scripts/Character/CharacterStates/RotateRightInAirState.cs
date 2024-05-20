using Animancer;
using UnityEngine;

public sealed class RotateRightInAirState : CharacterState
{
    [SerializeReference]
    private ClipTransition _Rotate;

    public override CharacterStatePriority Priority => CharacterStatePriority.High;

    public override bool CanInterruptSelf => true;
    public override void OnEnter()
    {
        Character.Animancer.Layers[1].Play(_Rotate);
    }

    private void Awake()
    {
        _Rotate.Events.OnEnd = () =>
        {
            AnimEndEvent.Invoke();
            Character.Animancer.Stop(_Rotate);
        };
    }
}
