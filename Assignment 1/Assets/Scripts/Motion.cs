using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Rigidbody rigidBody;

    private Vector3 Velocity;
    private Vector3 Acceleration;
    private Vector4 angularVelocity;
    private Quaternion newOrientation; 
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.mass = 25.0f; 

        if (gameObject.tag == "Tetra1")
        {
            ApplyForce(new Vector3(10.0f, 0, 0.0f));
            angularVelocity = new Vector4(rigidBody.angularVelocity.x, 45.0f, rigidBody.angularVelocity.z, 0.0f); //Apply angular velocity of 45 degress along the y-axis, while also taking into account 
        }

        else if (gameObject.tag == "Tetra2")
        {
            ApplyForce(new Vector3(-10.0f, 0, 0.0f));
            angularVelocity = new Vector4(rigidBody.angularVelocity.x, -45.0f, rigidBody.angularVelocity.z, 0.0f);
        }
    }

    void ApplyForce(Vector3 force_)
    {
        Acceleration = force_ / rigidBody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        //Linear Motion
        gameObject.transform.position += new Vector3(Velocity.x * Time.deltaTime, Velocity.y * Time.deltaTime, Velocity.z * Time.deltaTime); 
        Velocity += Acceleration * Time.deltaTime;

        //Linear Rotation
        transform.rotation = transform.rotation * Quaternion.AngleAxis(angularVelocity.magnitude * Time.deltaTime / 2.0f, angularVelocity);
    }
}
