using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simplex : MonoBehaviour
{
    public Vector3 support(Tetrahedron shape1_, Tetrahedron shape2_, Vector3 direction_)
    {
        Vector3 p1 = shape1_.getFarthestPointInDirection(direction_);
        Vector3 p2 = shape2_.getFarthestPointInDirection(direction_);

        Vector3 p3 = p1 - p2;

        return p3; 
    }
}
