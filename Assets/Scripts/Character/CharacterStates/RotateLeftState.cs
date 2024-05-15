using Animancer;
using UnityEngine;

public sealed class RotateLeftState : CharacterState
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
        // jumpState.NormalizedTime = 0;
    }

    private void Awake()
    {
        // _Animation.Events.OnEnd = CompleteEvent.Invoke;
        // _Animation.Events.OnEnd = () => Debug.Log("RotateLeftState.OnEnd");
        // _Rotate.Events.OnEnd = () => {
        //     Character.Animancer.Layers[1].Stop();
        //     _Rotate.Events.Clear();
        // };
        _Jump.Events.OnEnd = () =>
        {
            CompleteEvent.Invoke();
            // Character.Animancer.Layers[0].Stop();
            Character.Animancer.Stop(_Jump);
        };
    }
}
