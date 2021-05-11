using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GodHand : MonoBehaviour
{
    public CinemachineVirtualCamera cvc;

    public GameObject AE86;
    public GameObject Miata;
    public GameObject MuscleCar;
    public Transform Spawn;
    
    private void Start()
    {
        var pickedCar = Ghost.PickedCar;
        switch (pickedCar)
        {
            case "AE86":
            {
                var ae86 = Instantiate(AE86);
                // chance transform for location (box and forest sea level)
                ae86.transform.position = Spawn.position;
                ae86.transform.rotation = Spawn.rotation;
            
                cvc.Follow = ae86.transform.GetChild(0).transform;
                cvc.LookAt = ae86.transform.GetChild(0).transform;
                break;
            }
            case "Miata":
            {
                var miata = Instantiate(Miata);
                miata.transform.position = Spawn.position;
                miata.transform.rotation = Spawn.rotation;
            
                cvc.Follow = miata.transform.GetChild(0).transform;
                cvc.LookAt = miata.transform.GetChild(0).transform;
                break;
            }
            case "MuscleCar":
            {
                var muscleCar = Instantiate(MuscleCar);
                muscleCar.transform.position = Spawn.position;
                muscleCar.transform.rotation = Spawn.rotation;
            
                cvc.Follow = muscleCar.transform.GetChild(0).transform;
                cvc.LookAt = muscleCar.transform.GetChild(0).transform;
                break;
            }
            case "":
                var car = GameObject.FindGameObjectWithTag("Player") == null 
                    ? Instantiate(AE86) : GameObject.FindGameObjectWithTag("Player");
                
                car.transform.position = Spawn.position;
                car.transform.rotation = Spawn.rotation;
                
                cvc.Follow = car.transform.GetChild(0).transform;
                cvc.LookAt = car.transform.GetChild(0).transform;
                break;
        }
    }
}
