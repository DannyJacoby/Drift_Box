using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GodHand : MonoBehaviour
{
    public CinemachineVirtualCamera cvc;

    public GameObject AE86;
    public GameObject Miata;
    public GameObject MuscleCar;
    public Transform center;
    private void Start()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        
        var pickedCar = Ghost.PickedCar;
        if (pickedCar.Equals("AE86"))
        {
            GameObject ae6Running = Instantiate(AE86);
            ae6Running.transform.position = new Vector3(0f, 1f, 0f);
            ae6Running.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            
            cvc.Follow = ae6Running.transform;
            cvc.LookAt = ae6Running.transform;
        }
        if (pickedCar.Equals(""))
        {
            cvc.Follow = center;
            cvc.LookAt = center;
        }
    }
}
