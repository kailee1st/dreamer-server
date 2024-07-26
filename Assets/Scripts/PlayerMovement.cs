using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Orientation;
    public GameObject LookDirection;

    //힘에 대한 정보
 
    public float forwardForce;
    public float horizontalForce;
    public float horizontalMoveCoefficient = 10f;
    public float forwardMoveCoefficient = 10f;
    public float speedLimit = 10f;

    public float xRotationSpeed;
    public float yRotationSpeed;
    public float JumpForce;

    private Rigidbody rb;
    private float xRotation;
    private float yRotation;

    public float rho = 0f;


    void Start()
    {
        forwardForce = 0;
        horizontalForce = 0;
        xRotationSpeed = 0;
        yRotationSpeed = 0;
        JumpForce = 0;
        rb = GetComponent<Rigidbody>();
        xRotation = 0;
        yRotation = 0;
    }


    void Update()
    {
        Vector3 forwardDir = Orientation.transform.forward;
        rb.AddForce(forwardDir.normalized * forwardForce * forwardMoveCoefficient);
        Vector3 horizontalDir = Orientation.transform.right;
        rb.AddForce(horizontalDir.normalized * horizontalForce * horizontalMoveCoefficient);
        rb.AddForce(rb.velocity * rho * -1.0f); //공기저항
        SpeedControl();

        xRotation -= xRotationSpeed;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //상하 시선이동 범위 제한
        yRotation += yRotationSpeed;
        Orientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
        LookDirection.transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
    }

    private void SpeedControl()
    {
        //속도제한
        Vector3 currentVelocity = rb.velocity;
        if (currentVelocity.magnitude > speedLimit)
        {
            rb.velocity = currentVelocity.normalized * speedLimit;
        }
    }
}
