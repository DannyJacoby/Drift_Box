using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GodHand : MonoBehaviour
{
    public CinemachineVirtualCamera cvc;
    public Ghost ghost;
    private void Start()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        // cvc.Follow = transform
        // cvc.LookAt = transform
    }
}
