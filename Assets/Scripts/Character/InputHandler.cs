using UnityEngine;

public sealed class InputHandler : MonoBehaviour
{
    [SerializeField]
    private Character _Character;

    [SerializeField]
    private CharacterState _Flying;

    [SerializeField]
    private CharacterState _RotateLeft;

    [SerializeField]
    private CharacterState _RotateRight;

    [SerializeField]
    private CharacterState _PrepareingToJump;
    public CharacterState LastRotationState;

    private void Awake()
    {
        _RotateRight.SubscribeComplete(() => LastRotationState = _RotateRight);
        _RotateLeft.SubscribeComplete(() => LastRotationState = _RotateLeft);
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
            // LastRotationState = rotateState;
            _Character.StateMachine.ForceSetState(rotateState);
            rotateState.SubscribeCompleteOnce(() =>_Character.StateMachine.ForceSetState(_PrepareingToJump));
        }
        else if (currentState == _Character.StateMachine.DefaultState)
        {
            _Character.StateMachine.ForceSetState(_PrepareingToJump);
        }
        // if (_Character.Animancer.gameObject.transform.rotation.y > 0)

        // state.CompleteEvent.AddListener(() => _Character.StateMachine.ForceSetState(_PrepareingToJump));
    }

    public void StartFlying()
    {
        _Character.StateMachine.ForceSetState(_Flying);
    }

    public void SetDefaultState()
    {
        _Character.StateMachine.ForceSetState(_Character.StateMachine.DefaultState);
    }

    public void HandleCollision(CollisionSide collisionSide)
    {
        switch (collisionSide)
        {
            case CollisionSide.Left:
                _Character.StateMachine.ForceSetState(_RotateRight);
                _RotateRight.SubscribeCompleteOnce(StartFlying);
                break;
            case CollisionSide.Right:
            // if(LastRotationState == _RotateRight) {}
                _Character.StateMachine.ForceSetState(_RotateLeft);
                _RotateLeft.SubscribeCompleteOnce(StartFlying);
                break;
            case CollisionSide.Up:
                // _Character.StateMachine.ForceSetState(_PrepareingToJump);
                break;
            case CollisionSide.Down:
                _Character.StateMachine.ForceSetState(_Character.StateMachine.DefaultState);
                break;
        }
    }
}
