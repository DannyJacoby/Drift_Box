using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TimeTrial : MonoBehaviour
{
   public TextMeshProUGUI timeText;
   public TextMeshProUGUI bestTimeText;
   
   public static float Time = 0.0f;
   public static float BestTime = 0.0f;
   private static float m_WorstTime = 100000.0f;
   
   public string trialName;

   public bool isStart;
   public static bool TrackingTime;

   private void Awake()
   {
      if (PlayerPrefs.GetFloat(trialName) == 0)
      {
         Reset();
      }
      UpdateTime(0);
      Debug.Log(PlayerPrefs.GetFloat(trialName));
   }

   private void FixedUpdate()
   {
      if (!TrackingTime) return;
      Time += UnityEngine.Time.deltaTime;
      UpdateTime(Time);
   }

   private void UpdateTime(float myTime)
   {
      var minutes = Mathf.FloorToInt(Time / 60);
      var seconds = Mathf.Floor(Time % 60);
      var milliseconds = (Time % 1) * 1000;
      
      var timeStr = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
      timeText.SetText("Time " + trialName + ": " + timeStr);
   }
   
   private void UpdateBestTime(float myTime)
   {
      BestTime = Time;
      var minutes = Mathf.FloorToInt(Time / 60);
      var seconds = Mathf.Floor(Time % 60);
      var milliseconds = (Time % 1) * 1000;
      
      var timeStr = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
      bestTimeText.SetText("Best " + trialName + ": " + timeStr);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.transform.parent.gameObject.CompareTag("Player") && isStart)
      {
         Time = 0.0f;
         TrackingTime = true;
      }

      if (other.transform.parent.gameObject.CompareTag("Player") && !isStart)
      {
         if (Time < PlayerPrefs.GetFloat(trialName, m_WorstTime))
         {
            PlayerPrefs.SetFloat(trialName, Time);
            UpdateBestTime(Time);
         }
         TrackingTime = false;
      }
   }

   private void OnPreRender()
   {
      // Reset();
   }

   public void Reset()
   {
      PlayerPrefs.DeleteKey(trialName);
      PlayerPrefs.SetFloat(trialName, m_WorstTime);
   }
}
