using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
    public bool isBackWheels; // is this axel the rear wheels?
}

public class CarController : MonoBehaviour { 
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public float speedThreshold;
    public int stepsBelowThreshold;
    public int stepsAboveThreshold;

    private bool m_Started = false;
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        var visualWheel = collider.transform.GetChild(0);
        var visualWheelTransform = visualWheel.transform;
        
        collider.GetWorldPose(out var position, out var rotation);
        
        visualWheelTransform.position = position;
        visualWheelTransform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            this.transform.localPosition = new Vector3(0, 1, 0);
            this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        
        var motor = maxMotorTorque * Input.GetAxis("Vertical");
        var steering = maxSteeringAngle * Input.GetAxis("Horizontal");
           
           foreach (var axleInfo in axleInfos) {
               if (axleInfo.steering) {
                   axleInfo.leftWheel.steerAngle = steering; 
                   axleInfo.rightWheel.steerAngle = steering;
               } 
               
               if (axleInfo.motor) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
               }

               if (axleInfo.isBackWheels)
               {
                   if (Input.GetKey(KeyCode.Space))
                   {
                       axleInfo.leftWheel.brakeTorque = 200000.0f;
                       axleInfo.rightWheel.brakeTorque = 200000.0f;
                   }
                   else
                   {
                       axleInfo.leftWheel.brakeTorque = 0.0f;
                       axleInfo.rightWheel.brakeTorque = 0.0f;
                   }
               }

               ApplyLocalPositionToVisuals(axleInfo.leftWheel);
               ApplyLocalPositionToVisuals(axleInfo.rightWheel);

               if (m_Started) continue;
               axleInfo.leftWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
               axleInfo.rightWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
           }

           m_Started = true;
    }
}
    
