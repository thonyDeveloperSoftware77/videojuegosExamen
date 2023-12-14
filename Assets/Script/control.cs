using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public float accelerationPower = 20f;
    public float steeringPower = 16f;
    public float maxSpeed = 90f;
    public float brakePower = 15f;
    public float drag = 0.2f;

    private Rigidbody carRigidbody;
    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float speed = 0;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        speed = Vector3.Dot(carRigidbody.velocity, transform.forward);

        if (accelerationInput > 0 && speed < maxSpeed)
        {
            carRigidbody.AddForce(transform.forward * accelerationPower * accelerationInput);
            carRigidbody.drag = 0;
        }
        else if (accelerationInput < 0)
        {
            carRigidbody.AddForce(transform.forward * brakePower * accelerationInput);
        }
        else
        {
            carRigidbody.drag = drag;
        }

        if (Mathf.Abs(speed) > 0.09f)
        {
            float steeringAdjustment = speed / maxSpeed;
            carRigidbody.AddTorque(transform.up * steeringPower * steeringInput * speed * steeringAdjustment);

            if (steeringInput != 0)
            {
                Quaternion turnRotation = Quaternion.Euler(0f, steeringInput * steeringPower * steeringAdjustment, 0f);
                carRigidbody.MoveRotation(carRigidbody.rotation * turnRotation);
            }
        }
    }

}
