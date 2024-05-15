using Animancer;
using UnityEngine;

public sealed class RotateRightState : CharacterState
{
    [SerializeReference]
    private ClipTransition _Rotate;

    [SerializeReference]
    private ClipTransition _Jump;

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => true;

    private void OnEnable()
    {
        Character.Animancer.Layers[0].Play(_Jump);
        Character.Animancer.Layers[1].Play(_Rotate);
    }

    // private void OnDisable()
    // {
    //     Character.Animancer.Layers[1].Stop();
    // }

    private void Awake()
    {
        // Character.Animancer.Layers[1]
        // _Rotate.Events.OnEnd = () => {
            // Character.Animancer.Layers[0].Stop();
            // _Rotate.Events.Clear();
        // };
        _Jump.Events.OnEnd = () =>
        {
            CompleteEvent.Invoke();
            Character.Animancer.Stop(_Jump);
        };
    }
}
