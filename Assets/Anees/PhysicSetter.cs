using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PhysicSetter : MonoBehaviour
{
    public Vector3Range Force ;
    public Vector3Range Torque;

    Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        var force = new Vector3(
            Random.Range(Force.min.x, Force.max.x),
            Random.Range(Force.min.y, Force.max.y),
            Random.Range(Force.min.z, Force.max.z));
        var torque = new Vector3(
            Random.Range(Torque.min.x, Torque.max.x),
            Random.Range(Torque.min.y, Torque.max.y),
            Random.Range(Torque.min.z, Torque.max.z));

        Rb.AddForce(force * 10f, ForceMode.Impulse);
        Rb.AddTorque(torque * 10f, ForceMode.Impulse);
    }
}

[Serializable]
public class Vector3Range
{
    public Vector3 min = Vector3.zero;
    public Vector3 max = Vector3.one;
}
