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
    private void Start()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        
        var pickedCar = Ghost.PickedCar;
        if (pickedCar.Equals("AE86"))
        {
            GameObject AE86Running = Instantiate(AE86);
            AE86Running.transform.position = new Vector3(0f, 1f, 0f);
            AE86Running.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            
            cvc.Follow = AE86Running.transform;
            cvc.LookAt = AE86Running.transform;
        }
    }
}
