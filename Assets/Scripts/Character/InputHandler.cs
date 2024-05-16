using UnityEngine;

public sealed class InputHandler : MonoBehaviour
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
    public CharacterState LastRotationState;

    // private void Awake()
    // {
    //     LastRotationState = _RotateRight;
    // }

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
            rotateState.CompleteEvent.AddListener(() =>
            {
                _Character.StateMachine.ForceSetState(_PrepareingToJump);
                rotateState.CompleteEvent.RemoveAllListeners();
            });
        }
        else if (currentState == _Character.StateMachine.DefaultState)
        {
            _Character.StateMachine.ForceSetState(_PrepareingToJump);
        }
        // if (_Character.Animancer.gameObject.transform.rotation.y > 0)

        // state.CompleteEvent.AddListener(() => _Character.StateMachine.ForceSetState(_PrepareingToJump));
    }

    public void MakeJump()
    {
        _Character.StateMachine.ForceSetState(_Jump);
    }

    public void SetDefaultState()
    {
        _Character.StateMachine.ForceSetState(_Character.StateMachine.DefaultState);
    }
}
