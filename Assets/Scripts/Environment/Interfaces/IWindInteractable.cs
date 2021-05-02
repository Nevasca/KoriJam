using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindInteractable
{
    void AddWindForce(Vector3 force);
    Vector3 GetVelocity();
    void SetVelocity(Vector3 velocity);
    void SetInsideWind(bool value);
}
