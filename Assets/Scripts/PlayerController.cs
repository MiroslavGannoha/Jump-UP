using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Energy energyState;

    [SerializeField]
    private LineAimer lineAimer;

    [SerializeField]
    private float rechargeDelay = 2f;

    [SerializeField]
    private float shotPower = 150f;

    IEnumerator restoreEnergyCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineAimer.OnAiming.AddListener(OnAiming);
        lineAimer.OnAimingEnd.AddListener(OnAimingEnd);
        RechargeEnergy();
    }

    // Update is called once per frame
    void Update() { }

    private void OnAimingEnd(float strength, Vector3 direction)
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

    private void OnAiming(float strength, Vector3 direction)
    {
        Debug.Log(strength);
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
}
