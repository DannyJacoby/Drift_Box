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

    private bool started = false;
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
           
           foreach (AxleInfo axleInfo in axleInfos) {
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

               if (started) continue;
               axleInfo.leftWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
               axleInfo.rightWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
           }

           started = true;
    }
}
    
