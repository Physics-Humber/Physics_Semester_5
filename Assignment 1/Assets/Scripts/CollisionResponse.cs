using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResponse : MonoBehaviour
{
    Vector3 n = new Vector3(2.0f/3.0f, 1.0f/3.0f, 2.0f/3.0f);
    Vector3 pointOfCollision = new Vector3(-8.0f, 1.0f, 0.0f); 
    float coefficient = 0.5f; 

    StarShip starShip;
    Asteroid asteroid;
   
    
    void Start()
    {
        starShip.mass = 100.0f;
        asteroid.mass = 1.0f;

        starShip.initialVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        asteroid.initialVelocity = new Vector3(20000.0f, 10000.0f, 20000.0f);

        starShip.initialAngularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        asteroid.initialAngularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        starShip.centreMass = new Vector3(0.0f, 0.0f, 0.0f);
        asteroid.centreMass = new Vector3(-10.0f, 2.0f, 0.0f);

        starShip.intertia.SetRow(0, new Vector4(20.0f, 0.0f, 0.0f, 0.0f));
        starShip.intertia.SetRow(1, new Vector4(0.0f, 40.0f, 0.0f, 0.0f));
        starShip.intertia.SetRow(2, new Vector4(0.0f, 0.0f, 20.0f, 0.0f));
        starShip.intertia.SetRow(3, new Vector4(0.0f, 0.0f, 0.0f, 20.0f));

        asteroid.intertia.SetRow(0, new Vector4(0.1f, 0.0f, 0.0f, 0.0f));
        asteroid.intertia.SetRow(1, new Vector4(0.0f, 0.1f, 0.0f, 0.0f));
        asteroid.intertia.SetRow(2, new Vector4(0.0f, 0.0f, 0.1f, 0.0f));
        asteroid.intertia.SetRow(3, new Vector4(0.0f, 0.0f, 0.0f, 0.1f));

        float Vr = Vector3.Dot(n, starShip.initialVelocity - asteroid.initialVelocity);
        Vector3 r1 = pointOfCollision - starShip.centreMass;
        Vector3 r2 = pointOfCollision - asteroid.centreMass;

        float JImpulseTmp = -Vr * (coefficient + 1.0f);  
        float tmp = Vector3.Dot(n,  starShip.intertia.inverse * (Vector3.Cross(Vector3.Cross(r1, n), r1)));
        float tmp2 = Vector3.Dot(n, asteroid.intertia.inverse * (Vector3.Cross(Vector3.Cross(r2, n), r2)));

        
        float tmp3 = (1.0f / asteroid.mass) + (1.0f / starShip.mass) + (tmp + tmp2);
        float JImpulse = JImpulseTmp / tmp3;

        Debug.Log("JImpulse = " + JImpulse);

        Vector3 starShipFinalVelocity;
        starShipFinalVelocity.x = (starShip.initialVelocity.x + JImpulse) * (n.x / starShip.mass);
        starShipFinalVelocity.y = (starShip.initialVelocity.y + JImpulse) * (n.y / starShip.mass);
        starShipFinalVelocity.z = (starShip.initialVelocity.z + JImpulse) * (n.z / starShip.mass);

        Debug.Log("Starship Final Velocity = " + starShipFinalVelocity);

        Vector3 starShipFinalAngularVelocity;

        Vector3 rCrossProduct = Vector3.Cross(r1, JImpulse * n);
        starShipFinalAngularVelocity.x = (starShip.initialAngularVelocity.x + starShip.intertia.inverse.m00) * rCrossProduct.x;
        starShipFinalAngularVelocity.y = (starShip.initialAngularVelocity.y + starShip.intertia.inverse.m11) * rCrossProduct.y;
        starShipFinalAngularVelocity.z = (starShip.initialAngularVelocity.z + starShip.intertia.inverse.m22) * rCrossProduct.z;

        Debug.Log("Starship Final Angular Velocity = " + starShipFinalAngularVelocity);
    }
}
                    