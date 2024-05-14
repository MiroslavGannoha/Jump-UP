using System;
using System.Linq;
using Animancer;
using Animancer.Examples.Events;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private ClipTransition _Idle;
    private AnimationClip _Move;

    [SerializeField]
    private ClipTransition _Fly;

    [SerializeField]
    private ClipTransition _Jump;

    [SerializeField]
    private ClipTransition _JumpSimple;

    [SerializeField]
    private AnimationClip _RotateLeft;

    [SerializeField]
    private AnimationClip _RotateRight;

    [SerializeField]
    private AnimancerComponent _Animancer;

    private AnimancerState jumpState;

    private AnimancerLayer _BaseLayer;
    private AnimancerLayer _ActionLayer;

    public static readonly Action LogCurrentEvent = () =>
    {
        Debug.Log(
            $"An {nameof(AnimancerEvent)} was triggered:"
                + $"\n- Event: {AnimancerEvent.CurrentEvent}"
                + $"\n- State: {AnimancerEvent.CurrentState.GetDescription()}",
            AnimancerEvent.CurrentState.Root?.Component as UnityEngine.Object
        );
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; i < _Jump.Events.Count; i++)
        {
            _Jump.Events.AddCallback(i, EventUtilities.LogCurrentEvent);
        }
        // var state = _Animancer.Play(_Move);
    }

    public void OnEnable()
    {
        _Animancer.Layers[0].Play(_Idle);
    }

    // Update is called once per frame
    void Update() { }

    public void StartJump()
    {
        _Animancer.Layers[0].Play(_Jump);
        // _PepareJump.Events.AddCallback(
        //     0,
        //     () =>
        //     {
        //         state.IsPlaying = false;
        //     }
        // );
    }

    public void StartFly()
    {
        _Animancer.Play(_Fly);
        // _PepareJump.Events.AddCallback(
        //     0,
        //     () =>
        //     {
        //         state.IsPlaying = false;
        //     }
        // );
    }

    public void PauseAnimation()
    {
        AnimancerEvent.CurrentState.IsPlaying = false;
        // AnimancerEvent.CurrentState.NormalizedTime = AnimancerEvent.CurrentEvent.normalizedTime;
    }

    public void StopAnimation()
    {
        var state = _Animancer.States[_Jump];
        state.Stop();
        OnEnable();
    }

    public void ResumeAnimation()
    {
        var state = _Animancer.States[_Jump];
        state.IsPlaying = true;
        // state.Stop();
    }

    public void RotateLeft()
    {
        if (_Animancer.gameObject.transform.rotation.y > 0)
        {
            _Animancer.Stop();
            _Animancer.Layers[1].Play(_RotateLeft);
            _Animancer.Layers[0].Play(_JumpSimple);
            StartJump();
            // _Animancer.gameObject.transform.Rotate(0, -90, 0);
        }
    }

    public void RotateRight()
    {
        if (_Animancer.gameObject.transform.rotation.y < 0)
        {
            _Animancer.Stop();
            _Animancer.Layers[1].Play(_RotateRight);
            _Animancer.Layers[0].Play(_JumpSimple);
            StartJump();
            // _Animancer.gameObject.transform.Rotate(0, 90, 0);
        }
    }
}
