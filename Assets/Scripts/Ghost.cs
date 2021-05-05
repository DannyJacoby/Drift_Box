using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public static string PickedCar;
    public static string PickedLvl;
    public bool amOpening;

    private bool hasPickedCar;
    private bool hasPickedLvl;
    
    private void Start()
    {
        if (!amOpening)
        {
            if (SceneManager.GetActiveScene().name == "BoxLevel")
            {
                PickedCar = "AE86";
                PickedLvl = "BoxLevel";
            }
            Debug.Log("You're going to use car " + PickedCar);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && amOpening)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    Debug.Log("You've chosen the car " + hit.collider.gameObject.name);
                    PickedCar = hit.collider.gameObject.name;
                    hasPickedCar = true;
                }

                if (hit.collider.CompareTag("Level"))
                {
                    Debug.Log("You've chosen the level " + hit.collider.name);
                    PickedLvl = hit.collider.gameObject.name;
                    hasPickedLvl = true;
                }

            }
        }

        if (hasPickedCar && hasPickedLvl)
        {
            Debug.Log("With car " + PickedCar + " and level " + PickedLvl + " starting game");
            UnityEngine.SceneManagement.SceneManager.LoadScene(PickedLvl);
        }
        
    }
    
}
