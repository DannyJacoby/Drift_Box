using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public static string PickedCar = "";
    public static string PickedLvl = "";
    public bool amOpening;

    public GameObject carSelector;
    public GameObject lvlSelector;
    
    private bool m_HasPickedCar;
    private bool m_HasPickedLvl;
    
    private void Start()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
        
        if (!amOpening) return;
        carSelector = GameObject.FindGameObjectWithTag("carLight");
        lvlSelector = GameObject.FindGameObjectWithTag("lvlLight");

    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && amOpening)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    // Debug.Log("You've chosen the car " + hit.collider.gameObject.name);
                    PickedCar = hit.collider.gameObject.name;
                    m_HasPickedCar = true;
                    var position = hit.collider.transform.position;
                    carSelector.transform.position = new Vector3(position.x,10f, position.z);
                }

                if (hit.collider.CompareTag("Level"))
                {
                    // Debug.Log("You've chosen the level " + hit.collider.name);
                    PickedLvl = hit.collider.gameObject.name;
                    m_HasPickedLvl = true;
                    var position = hit.collider.transform.position;
                    lvlSelector.transform.position = new Vector3(position.x,10f, position.z);
                }

            }
        }

        if (!m_HasPickedCar || !m_HasPickedLvl) return;
        // Debug.Log("With car " + PickedCar + " and level " + PickedLvl + " starting game");
        SceneManager.LoadScene(PickedLvl);

    }
    
}
