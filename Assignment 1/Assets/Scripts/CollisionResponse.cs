using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResponse : MonoBehaviour
{
    Vector3 n = new Vector3(2.0f/3.0f, 1.0f/3.0f, 2.0f/3.0f);
    float coefficient = 0.5f; 

    StarShip starShip;
    Asteroid asteroid;
    Quaternion matrix;
    
    void Start()
    {
        
        starShip.initialVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        asteroid.initialVelocity = new Vector3(20000.0f, 10000.0f, 20000.0f);

        starShip.mass = 100.0f;
        asteroid.mass = 1.0f; 

        starShip.intertia = new Vector3(20.0f, 40.0f, 20.0f);
        asteroid.intertia = new Vector3(0.1f, 0.1f, 0.1f);

        float Vr = Vector3.Dot(n, starShip.initialVelocity - asteroid.initialVelocity);
        float JImpulse = -Vr * (coefficient + 1.0f) / (1.0f / asteroid.mass) + (1.0f / starShip.mass);
        asteroid.intertia.len
        float test = Vector3.Dot(n, Mathf.Pow(asteroid.intertia.length(), -1) * new Vector3());
       
    }

    
}
                    