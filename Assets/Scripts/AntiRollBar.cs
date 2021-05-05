using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
   public WheelCollider WheelL;
   public WheelCollider WheelR;
   public Rigidbody CarRigidBody;
   public float AntiRoll = 1000.0f;

   private void FixedUpdate()
   {
      WheelHit hit;
      var travelL = 1.0f;
      var travelR = 1.0f;

      var groundedL = WheelL.GetGroundHit(out hit);
      if (groundedL)
         travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

      var groundedR = WheelR.GetGroundHit(out hit);
      if(groundedR) 
         travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

      var antiRollForce = (travelL - travelR) * AntiRoll;
      
      if(groundedL) 
         CarRigidBody.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
      
      if(groundedR)
         CarRigidBody.AddForceAtPosition(WheelR.transform.up * -antiRollForce, WheelR.transform.position);
      
   }
}
