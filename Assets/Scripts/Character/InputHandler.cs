using System;
using Unity.VisualScripting;
using UnityEngine;

public sealed class InputHandler : MonoBehaviour
{
    [SerializeField]
    private Character _Character;

    [SerializeField]
    private CharacterState _FlyingState;

    [SerializeField]
    private CharacterState _RotateLeftState;

    [SerializeField]
    private CharacterState _RotateRightState;

    [SerializeField]
    private CharacterState _RotateLeftInAirState;

    [SerializeField]
    private CharacterState _RotateRightInAirState;

    [SerializeField]
    private CharacterState _PrepareingToJump;
    public CharacterState LastRotationState;

    private void Awake()
    {
        _RotateRightState.SubscribeEnterState(() => LastRotationState = _RotateRightState);
        _RotateRightInAirState.SubscribeEnterState(() => LastRotationState = _RotateRightState);
        _RotateLeftState.SubscribeEnterState(() => LastRotationState = _RotateLeftState);
        _RotateLeftInAirState.SubscribeEnterState(() => LastRotationState = _RotateLeftState);
    }

    public void UpdateRotation(float strength, Vector3 direction)
    {
        if (strength < 1)
            return;
        var currentState = _Character.StateMachine.CurrentState;
        var rotateState = direction.x < 0 ? _RotateLeftState : _RotateRightState;
        if (
            rotateState != LastRotationState
            // && (
            //     currentState == _PrepareingToJump
            //     || currentState == _Character.StateMachine.DefaultState
            // )
        )
        {
            // LastRotationState = rotateState;
            _Character.StateMachine.TrySetState(rotateState);
            rotateState.SubscribeAnimEnd(
                () => _Character.StateMachine.ForceSetState(_PrepareingToJump)
            );
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
        _Character.StateMachine.ForceSetState(_FlyingState);
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
                _Character.StateMachine.ForceSetState(_RotateRightInAirState);
                _RotateRightInAirState.SubscribeAnimEnd(StartFlying);
                break;
            case CollisionSide.Right:
                // Console.WriteLine($"LastRotationState {LastRotationState}", DateTime.Now);
                _Character.StateMachine.ForceSetState(_RotateLeftInAirState);
                _RotateLeftInAirState.SubscribeAnimEnd(StartFlying);
                break;
            case CollisionSide.Up:
                break;
            case CollisionSide.Down:
                _Character.StateMachine.ForceSetState(_Character.StateMachine.DefaultState);
                break;
        }
    }
}
