using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct StarShip
{
    public float mass;
    public Vector3 initialVelocity;
    public Vector3 initialAngularVelocity;
    public Vector3 centreMass; 
    public Matrix4x4 intertia;
}
