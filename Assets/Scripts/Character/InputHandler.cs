using Animancer.FSM;
using Animancer.Units;
using MoreMountains.Feedbacks;
using UnityEngine;

public sealed class InputHandler : MonoBehaviour
{
    [SerializeField]
    private MMF_Player JumpFBStartPlayer;

    [SerializeField]
    private MMF_Player JumpFBEndPlayer;

    [SerializeField]
    private Character _Character;

    [SerializeField]
    private CharacterState _FlyingState;

    [SerializeField]
    private CharacterState _JumpState;

    [SerializeField]
    private CharacterState _PrepareingToJump;

    [SerializeField]
    private CharacterRotationState _RotateLeftState;

    [SerializeField]
    private CharacterRotationState _RotateRightState;

    [SerializeField]
    private CharacterRotationState _LookRightState;

    [SerializeField]
    private CharacterRotationState _LookLeftState;

    private StateMachine<CharacterRotationState>.InputBuffer _InputBuffer;

    [SerializeField, Seconds]
    private float _InputTimeOut = 0.5f;

    private bool IsGrounded = true;

    private void Awake()
    {
        _InputBuffer = new StateMachine<CharacterRotationState>.InputBuffer(
            _Character.RotationStateMachine
        );
    }

    private void Update()
    {
        _InputBuffer.Update();
    }

    public void UpdateRotation(float strength, Vector3 direction)
    {
        if (strength < 1)
            return;
        var rotateState = direction.x < 0 ? _RotateLeftState : _RotateRightState;
        var isRotating = RotateToState(rotateState);
        if (isRotating)
        {
            var isSet = _Character.StateMachine.TrySetState(_JumpState);
            if (isSet)
            {
                _JumpState.SubscribeAnimEnd(
                    () => _Character.StateMachine.ForceSetState(_PrepareingToJump)
                );
            }
        }
        else if (_Character.StateMachine.CurrentState == _Character.StateMachine.DefaultState)
        {
            _Character.StateMachine.TrySetState(_PrepareingToJump);
        }
    }

    private bool RotateToState(CharacterRotationState state)
    {
        var lookState = state == _RotateLeftState ? _LookLeftState : _LookRightState;
        var isSet = _Character.RotationStateMachine.TrySetState(state);
        if (isSet)
        {
            state.SubscribeAnimEnd(() => _Character.RotationStateMachine.ForceSetState(lookState));
        }

        return isSet;
    }

    public void StartFlying()
    {
        _Character.StateMachine.ForceSetState(_FlyingState);
        IsGrounded = false;
    }

    public void PutOnGround()
    {
        _Character.StateMachine.TrySetState(_Character.StateMachine.DefaultState);
        IsGrounded = true;
        MidAirJumpFeedbackEnd();
    }

    public void SetDefaultState()
    {
        _Character.StateMachine.TrySetState(_Character.StateMachine.DefaultState);
    }

    public void MidAirJumpFeedbackStart()
    {
        Debug.Log($"_Character.StateMachine.CurrentState: {_Character.StateMachine.CurrentState}");
        if (
            _Character.StateMachine.CurrentState == _FlyingState
            && JumpFBStartPlayer.IsPlaying == false
        )
        {
            JumpFBStartPlayer.PlayFeedbacks();
            Debug.Log("JumpFBStartPlayer.PlayFeedbacks()");
        }
    }

    public void MidAirJumpFeedbackEnd()
    {
        if (JumpFBStartPlayer.IsPlaying)
        {
            // JumpFBStartPlayer.StopFeedbacks();
            JumpFBEndPlayer.PlayFeedbacks();
        }
    }

    public void HandleCollision(CollisionSide collisionSide)
    {
        switch (collisionSide)
        {
            case CollisionSide.Left:
                _InputBuffer.Buffer(_RotateRightState, _InputTimeOut);
                _RotateRightState.SubscribeAnimEnd(
                    () => _Character.RotationStateMachine.ForceSetState(_LookRightState)
                );

                break;
            case CollisionSide.Right:
                _InputBuffer.Buffer(_RotateLeftState, _InputTimeOut);
                _RotateLeftState.SubscribeAnimEnd(
                    () => _Character.RotationStateMachine.ForceSetState(_LookLeftState)
                );

                break;
            case CollisionSide.Up:
                break;
            case CollisionSide.Down:
                PutOnGround();
                break;
        }
    }
}
