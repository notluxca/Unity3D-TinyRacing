using UnityEngine;
using UnityEngine.SceneManagement;

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    [Header("Deceleration")]
    public float idleDeceleration = 500f; // força de freio motor

    [Header("Car Lights")]
    [SerializeField] private GameObject[] frontLights;
    [SerializeField] private GameObject[] backLights;

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Ajusta centro de massa para estabilidade
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        // Pega todas as rodas
        wheels = GetComponentsInChildren<WheelControl>();

        // Garante que luzes começam apagadas
        SetLights(frontLights, false);
        SetLights(backLights, false);
    }

    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed) && Mathf.Abs(vInput) > 0.01f;

        foreach (var wheel in wheels)
        {
            // Direção
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Acelerando normal
                if (wheel.motorized)
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;

                wheel.WheelCollider.brakeTorque = 0f;
            }
            else if (Mathf.Abs(vInput) > 0.01f)
            {
                // Freando manualmente (invertendo direção)
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
            else
            {
                // Freio motor quando solta o acelerador
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = idleDeceleration;
            }
        }


        if (vInput > 0.01f) // andando pra frente
        {
            SetLights(frontLights, true);
            SetLights(backLights, false);
        }
        else if (vInput < -0.01f) // ré
        {
            SetLights(frontLights, false);
            SetLights(backLights, true);
        }
        else // parado
        {
            SetLights(frontLights, false);
            SetLights(backLights, false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void SetLights(GameObject[] lights, bool state)
    {
        foreach (var lightObj in lights)
        {
            if (lightObj != null)
                lightObj.SetActive(state);
        }
    }
}
