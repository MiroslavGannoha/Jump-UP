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
        var state = direction.x < 0 ? _RotateLeft : _RotateRight;
        if(_Character.StateMachine.CurrentState != state) {
            Debug.Log(_Character.StateMachine.CurrentState != state);
            _Character.StateMachine.ForceSetState(state);
            state.CompleteEvent.AddListener(() => Debug.Log("CompleteEvent"));
        }
    
            // state.CompleteEvent.AddListener(() => _Character.StateMachine.ForceSetState(_PrepareingToJump));
    }
}
