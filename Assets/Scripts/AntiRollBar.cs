using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
   public WheelCollider wheelL;
   private Transform m_WheelLTransform;
   public WheelCollider wheelR;
   private Transform m_WheelRTransform;
   public Rigidbody carRigidBody;
   public float antiRoll = 1000.0f;

   void Start()
   {
      m_WheelLTransform = wheelL.transform;
      m_WheelRTransform = wheelR.transform;
   }
   private void FixedUpdate()
   {
      var travelL = 1.0f;
      var travelR = 1.0f;

      var groundedL = wheelL.GetGroundHit(out var hit);
      if (groundedL)
         travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;

      var groundedR = wheelR.GetGroundHit(out hit);
      if(groundedR) 
         travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;

      var antiRollForce = (travelL - travelR) * antiRoll;
      
      if(groundedL) 
         carRigidBody.AddForceAtPosition(wheelL.transform.up * -antiRollForce, m_WheelLTransform.position);
      
      if(groundedR)
         carRigidBody.AddForceAtPosition(wheelR.transform.up * antiRollForce, m_WheelRTransform.position);
      
   }
}
