using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simplex : MonoBehaviour
{
    Vector3[] pointsList = new Vector3[4];
    public void support(int pointIndex_, Tetrahedron tetrahedron_, Vector3 direction_)
    {

        Vector3 newPoint = tetrahedron_.getFarthestPointInDirection(direction_);

        if (pointIndex_ < 4)//Make sure the index isn't more than 4, to assure we only have 4 points 
        {
            pointsList[pointIndex_] = newPoint; //using an array and the pointIndex, we can swap points! 
        }
    }

    public Vector3 getPoint(int pointIndex_)
    {
        return pointsList[pointIndex_];
    }
}
