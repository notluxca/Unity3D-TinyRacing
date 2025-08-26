// ----------- CAR TUTORIAL SAMPLE PROJECT, Andrew Gotow 2009  -----------------
// -------------------------- Addapted and Modified by RCB ---------------------------------

using TMPro;
using UnityEngine;

public class MovePlayerCar : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public float[] gearRatio;
    public int currentGear = 0;
    public float engineTorque = 600F;
    public float maxEngineRPM = 3000F;
    public float minEngineRPM = 1000F;
    public float maxWheelAngle = 10;
    public GameObject rearLeftLight;
    
    private float engineRPM;
    private Rigidbody rb;
    private bool isBreakActive;

    
    public void Start()
    {
	    rb = GetComponent<Rigidbody>();
	    var centerOfMass = rb.centerOfMass;
        centerOfMass = new Vector3(centerOfMass.x, -1.5F, centerOfMass.z);
        rb.centerOfMass = centerOfMass;
        
        rearLeftLight.SetActive(false);
        isBreakActive = false;

    }

    public void Update()
    {
	    rb.drag = rb.velocity.magnitude / 250F;
	    engineRPM = (frontLeftWheel.rpm + frontRightWheel.rpm) / 2 * gearRatio[currentGear];
        ShiftGears();

        frontLeftWheel.motorTorque  = (engineTorque / gearRatio[currentGear]) * Input.GetAxis("Vertical");
        frontRightWheel.motorTorque = (engineTorque / gearRatio[currentGear]) * Input.GetAxis("Vertical");

        frontLeftWheel.steerAngle  = maxWheelAngle * Input.GetAxis("Horizontal");
        frontRightWheel.steerAngle = maxWheelAngle * Input.GetAxis("Horizontal");

        
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
	        isBreakActive = true;
	        rearLeftLight.SetActive(true);
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
	        isBreakActive = false;
	        rearLeftLight.SetActive(false);
	        frontLeftWheel.brakeTorque = 0;
	        frontRightWheel.brakeTorque = 0;
        }

        if (isBreakActive)
        {
	        frontLeftWheel.brakeTorque = 10000;
	        frontRightWheel.brakeTorque = 10000;  
        }
        
    }

    private void ShiftGears()
    {
	    if (engineRPM >= maxEngineRPM) 
	    {
		    var appropriateGear = currentGear;
		    for (var i = 0; i < gearRatio.Length; i++) 
		    {
			    if (frontLeftWheel.rpm * gearRatio[i] < maxEngineRPM) 
			    {
				    appropriateGear = i;
				    break;
			    }
		    }
		    currentGear = appropriateGear;
	    }

        if (engineRPM <= minEngineRPM)
        {
            var appropriateGear = currentGear;
            for (var j = gearRatio.Length - 1; j >= 0; j--)
            {
                if (frontLeftWheel.rpm * gearRatio[j] > minEngineRPM)
                {
                    appropriateGear = j;
                    break;
                }
            }
            currentGear = appropriateGear;
        }
    }
}
