using Animancer;
using UnityEngine;

public sealed class RotateRightState : CharacterRotationState
{
    [SerializeReference]
    private ClipTransition _RotateAnim;

    // [SerializeReference]
    // private ClipTransition _Jump;
    public override CharacterStatePriority Priority => CharacterStatePriority.High;
    public override CharacterRotationDirection Direction => CharacterRotationDirection.Right;


    public override bool CanInterruptSelf => false;

    public override void OnEnter()
    {
        // Character.Animancer.Layers[0].Play(_Jump);
        Character.Animancer.Layers[1].Play(_RotateAnim);
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


// Should be 4 states:
/*
- RotatedRight (Low)
- RotatedLeft (Low)
- RotatingRight (Medium)
- RotatingLeft (Medium)
*/
