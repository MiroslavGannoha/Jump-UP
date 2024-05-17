using Animancer;
using UnityEngine;

public sealed class RotateLeftInAirState : CharacterState
{
    [SerializeReference]
    private ClipTransition _Rotate;

    // [SerializeReference]
    // private ClipTransition _Jump;

    public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

    public override bool CanInterruptSelf => true;

    private void OnEnable()
    {
        // Character.Animancer.Layers[0].Play(_Jump);
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
        _Rotate.Events.OnEnd = () =>
        {
            Debug.Log("RotateLeftInAirState.OnEnd");
            // Character.Animancer.Stop(_Rotate);
            CompleteEvent.Invoke();
            // Character.Animancer.Layers[0].Stop();
        };
    }
}
