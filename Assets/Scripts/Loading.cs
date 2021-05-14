using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private double m_LoadingTime = 20;

    private void Update()
    {
        if (m_LoadingTime > 0.0f)
        {
            m_LoadingTime -= Math.Ceiling(Time.deltaTime);
            return;
        }

        SceneManager.LoadScene("OpeningScene");
    }
}
