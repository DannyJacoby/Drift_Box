using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class SpeedGates : MonoBehaviour
{
    public GameObject car;
    public float newSpeed = 50f;
    public static bool Triggered = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (car == null)
        {
            car = other.transform.parent.gameObject;
        }
        
        if (!Triggered)
        {
            car.GetComponentInChildren<KartController>().acceleration += newSpeed;
            Triggered = true;
        }
        else
        {
            car.GetComponentInChildren<KartController>().acceleration -= newSpeed;
            Triggered = false;
        }
    }
}
