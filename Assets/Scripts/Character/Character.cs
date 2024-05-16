using System;
using System.Collections;
using Animancer;
using UnityEngine;
using UnityEngine.Events;

public enum CollisionSide
{
    Up,
    Down,
    Left,
    Right
}

[DefaultExecutionOrder(-10000)]
public sealed class Character : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Energy energyState;

    [SerializeField]
    private float rechargeDelay = 2f;

    [SerializeField]
    private float shotPower = 150f;

    IEnumerator restoreEnergyCoroutine;

    [SerializeField]
    private AnimancerComponent _Animancer;
    public AnimancerComponent Animancer => _Animancer;

    [SerializeField]
    private CharacterState.StateMachine _StateMachine;
    public CharacterState.StateMachine StateMachine => _StateMachine;

    public UnityEvent<CollisionSide> CollisionEvent = new UnityEvent<CollisionSide>();

    private void Awake()
    {
        StateMachine.InitializeAfterDeserialize();
        // StateMachine.DefaultState = _Idle;
        rb = GetComponent<Rigidbody>();
        // _AnimationController = GetComponent<AnimationController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // lineAimer.OnAimingEnd.AddListener(OnAimingEnd);
        RechargeEnergy();
        // _Animancer.Play(_Move);
    }

    public void PerformJump(float strength, Vector3 direction)
    {
        var limitedCost = Math.Min(strength, 10f) / 10;
        float energyToSpend = Math.Min(limitedCost, energyState.energy);
        energyState.energy -= energyToSpend;
        rb.AddForce(energyToSpend * 10 * direction * shotPower, ForceMode.Impulse);
        energyState.cost = 0;
        if (restoreEnergyCoroutine != null)
        {
            StopCoroutine(restoreEnergyCoroutine);
        }
        restoreEnergyCoroutine = RechargeEnergy();
        StartCoroutine(restoreEnergyCoroutine);
    }

    public void CancelJump()
    {
        energyState.cost = 0;

        if (restoreEnergyCoroutine == null)
        {
            restoreEnergyCoroutine = RechargeEnergy();
            StartCoroutine(restoreEnergyCoroutine);
        }
    }

    public void DisplayEnergyCost(float strength, Vector3 direction)
    {
        var limitedCost = Math.Min(strength, 10f) / 10;
        energyState.cost = limitedCost / energyState.energy;
    }

    IEnumerator RechargeEnergy()
    {
        yield return new WaitForSeconds(rechargeDelay);
        while (energyState.energy < 1)
        {
            energyState.energy += 0.005f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            {
                Vector3 hit = collision.contacts[0].normal;
                float angle = Vector3.Angle(hit, Vector3.up);

                if (Mathf.Approximately(angle, 0))
                {
                    //Down
                    CollisionEvent.Invoke(CollisionSide.Down);
                }
                if (Mathf.Approximately(angle, 180))
                {
                    //Up
                    CollisionEvent.Invoke(CollisionSide.Up);
                }
                if (Mathf.Approximately(angle, 90))
                {
                    // Sides
                    Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                    if (cross.y > 0)
                    {
                        CollisionEvent.Invoke(CollisionSide.Left);
                    }
                    else
                    {
                        CollisionEvent.Invoke(CollisionSide.Right);
                    }
                }
            }
        }
    }
}
