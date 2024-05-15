using UnityEngine;

public sealed class BasicCharacterBrain : MonoBehaviour
{
    [SerializeField]
    private Character _Character;

    [SerializeField]
    private CharacterState _Jump;

    [SerializeField]
    private CharacterState _RotateLeft;

    [SerializeField]
    private CharacterState _RotateRight;

    [SerializeField]
    private CharacterState _PrepareingToJump;
    public CharacterState LastRotationState = new RotateRightState();

    private void Update()
    {
        // UpdateMovement();
        // UpdateAction();
    }

    private void UpdateMovement()
    {
        // float forward = ExampleInput.WASD.y;
        // if (forward > 0)
        // {
        //     _Character.StateMachine.TrySetState(_Move);
        // }
        // else
        // {
        //     _Character.StateMachine.TrySetDefaultState();
        // }
    }

    private void UpdateAction()
    {
        // if (ExampleInput.LeftMouseUp)
        //     _Character.StateMachine.TryResetState(_Action);
    }

    public void UpdateRotation(float strength, Vector3 direction)
    {
        if (strength < 1)
            return;
        var currentState = _Character.StateMachine.CurrentState;
        var rotateState = direction.x < 0 ? _RotateLeft : _RotateRight;
        if (
            rotateState != LastRotationState
            && (
                currentState == _PrepareingToJump
                || currentState == _Character.StateMachine.DefaultState
            )
        )
        {
            LastRotationState = rotateState;
            _Character.StateMachine.ForceSetState(rotateState);
            rotateState.CompleteEvent.AddListener(
                () => _Character.StateMachine.ForceSetState(_PrepareingToJump)
            );
        }
        // if (_Character.Animancer.gameObject.transform.rotation.y > 0)

        // state.CompleteEvent.AddListener(() => _Character.StateMachine.ForceSetState(_PrepareingToJump));
    }
}
