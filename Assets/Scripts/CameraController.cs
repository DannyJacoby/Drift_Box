using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    public float y;
    public float z;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, y, z);
    }

    // Update is called once per frame
    private void Update()
    {
        // if (Input.anyKey)
        // {
            Vector3 flatSpeed = player.GetComponent<Rigidbody>().velocity;
            flatSpeed.y = 0;
            // stores the flat velocity of your player so it can put the camera always behind it

            Quaternion wantedRotation;
            // error when velocity is 0
            if (flatSpeed != Vector3.zero)
            {
                float targetAngle = Quaternion.LookRotation(flatSpeed).eulerAngles.y;
                wantedRotation = Quaternion.Euler(0, targetAngle, 0);
            }
            else
            {
                return;
            }

            transform.position = player.transform.position + (wantedRotation * offset);
            transform.LookAt(player.transform);
        // }
       
    }
}
