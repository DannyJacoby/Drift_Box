using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public static string PickedCar = "";
    public static string PickedLvl = "";
    public bool amOpening;

    private bool m_HasPickedCar;
    private bool m_HasPickedLvl;
    
    private void Start()
    {
        if (amOpening) return;
        Debug.Log("You're going to use car " + PickedCar);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && amOpening)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    Debug.Log("You've chosen the car " + hit.collider.gameObject.name);
                    PickedCar = hit.collider.gameObject.name;
                    m_HasPickedCar = true;
                }

                if (hit.collider.CompareTag("Level"))
                {
                    Debug.Log("You've chosen the level " + hit.collider.name);
                    PickedLvl = hit.collider.gameObject.name;
                    m_HasPickedLvl = true;
                }

            }
        }

        if (!m_HasPickedCar || !m_HasPickedLvl) return;
        Debug.Log("With car " + PickedCar + " and level " + PickedLvl + " starting game");
        SceneManager.LoadScene(PickedLvl);

    }
    
}
