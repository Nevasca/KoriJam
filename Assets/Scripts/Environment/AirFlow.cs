using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlow : MonoBehaviour, IActivable
{
    [SerializeField] private Vector3 windForce;
    [SerializeField] private Transform windTip;
    [SerializeField] private bool active = true;

    private void Start()
    {
        windForce *= -PlayerMovement.BASE_GRAVITY / 100f;
        gameObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        IWindInteractable windInteractable = other.GetComponent<IWindInteractable>();
        if (windInteractable != null)
        {
            windInteractable.SetVelocity(Vector3.zero);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IWindInteractable windInteractable = other.GetComponent<IWindInteractable>();
        if (windInteractable != null)
        {
            Vector3 distanceToBase = other.transform.position - transform.position;
            float distanceNormalized = distanceToBase.y / windTip.position.y;
            windInteractable.AddWindForce(windForce - (windForce * distanceNormalized));
            windInteractable.SetInsideWind(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IWindInteractable windInteractable = other.GetComponent<IWindInteractable>();
        if (windInteractable != null)
            windInteractable.SetInsideWind(false);
    }

    public void ToggleActive()
    {
        active = !active;

        gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return active;
    }
}