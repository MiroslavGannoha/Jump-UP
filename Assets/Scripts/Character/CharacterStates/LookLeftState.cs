using Animancer;
using UnityEngine;

public sealed class LookLeftState : CharacterRotationState
{
    [SerializeReference]
    private ClipTransition _Anim;

    // [SerializeReference]
    // private ClipTransition _Jump;

    public override bool CanInterruptSelf => false;
    public override CharacterRotationDirection Direction => CharacterRotationDirection.Left;

    public override void OnEnter()
    {
        // Character.Animancer.Layers[0].Play(_Jump);
        Character.Animancer.Layers[1].Play(_Anim);
    }

    private void Awake()
    {
        // _Jump.Events.OnEnd = () =>
        // {
            // Character.Animancer.Stop(_Jump);
            // Character.Animancer.Stop(_Rotate);
            // AnimEndEvent.Invoke();
        // };
    }
}


// Should be 4 states:
/*
- RotatedRight (Low)
- RotatedLeft (Low)
- RotatingRight (Medium)
- RotatingLeft (Medium)
*/
