using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetrahedron : MonoBehaviour
{

	public bool sharedVertices = false;

	Vector3 A = new Vector3(0.5f, 0.8f, 0.3f);
	Vector3 B = new Vector3(0.0f, 0.0f, 0.0f);
	Vector3 C = new Vector3(0.5f, 0.0f, 0.9f);
	Vector3 D = new Vector3(1.0f, 0.0f, 0.0f);


	Vector3 E = new Vector3(0.5f, 0.1f, 0.3f);
	Vector3 F = new Vector3(0.0f, -0.7f, 0.0f);
	Vector3 G = new Vector3(0.5f, -0.7f, 0.9f);
	Vector3 H = new Vector3(1.0f, -0.7f, 0.0f);

	Vector3 p0;
	Vector3 p1;
	Vector3 p2;
	Vector3 p3;

	public void Rebuild()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}

		
	
		if (gameObject.tag == "Tetra1")
		{
			p0 = new Vector3(A.x, A.y, A.z);
			p1 = new Vector3(B.x, B.y, B.z);
			p2 = new Vector3(C.x, C.y, C.z);
			p3 = new Vector3(D.x, D.y, D.z);

			Debug.Log("Tetra1: " + p0 + ", " + p1 + ", " + p2 + ", " + p3);
		}

		else if (gameObject.tag == "Tetra2")
        {
			p0 = new Vector3(E.x, E.y, E.z);
			p1 = new Vector3(F.x, F.y, F.z);
			p2 = new Vector3(G.x, G.y, G.z);
			p3 = new Vector3(H.x, H.y, H.z);
		}

		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null)
		{
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		mesh.Clear();
		if (sharedVertices)
		{
			mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
			mesh.triangles = new int[]{
				0,1,2,
				0,2,3,
				2,1,3,
				0,3,1
			};
			// basically just assigns a corner of the texture to each vertex
			mesh.uv = new Vector2[]{
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
			};
		}
		else
		{
			mesh.vertices = new Vector3[]{
				p0,p1,p2,
				p0,p2,p3,
				p2,p1,p3,
				p0,p3,p1
			};
			mesh.triangles = new int[]{
				0,1,2,
				3,4,5,
				6,7,8,
				9,10,11
			};

			Vector2 uv0 = new Vector2(0, 0);
			Vector2 uv1 = new Vector2(1, 0);
			Vector2 uv2 = new Vector2(0.5f, 1);

			mesh.uv = new Vector2[]{
				uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2
			};

		}

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.Optimize();
	}

	public Vector3 getFarthestPointInDirection(Vector3 direction_)
    {
		Vector4 shape1DotProduct;
		shape1DotProduct.x = Vector3.Dot(A, direction_);
		shape1DotProduct.y = Vector3.Dot(B, direction_);
		shape1DotProduct.z = Vector3.Dot(C, direction_);
		shape1DotProduct.w = Vector3.Dot(D, direction_);

		float maxDot = Mathf.Max(shape1DotProduct.x, shape1DotProduct.y, shape1DotProduct.z, shape1DotProduct.w);

		Vector3 point1 = new Vector3(0.0f, 0.0f, 0.0f); 
		if(maxDot.Equals(shape1DotProduct.x))
        {
			point1 = A; 
        }

		else if (maxDot.Equals(shape1DotProduct.y))
        {
			point1 = B;
		}

		else if (maxDot.Equals(shape1DotProduct.z))
		{
			point1 = C;
		}

		else if (maxDot.Equals(shape1DotProduct.w))
		{
			point1 = D;
		}


		Vector4 shape2DotProduct;
		shape2DotProduct.x = Vector3.Dot(E, direction_);
		shape2DotProduct.y = Vector3.Dot(F, direction_);
		shape2DotProduct.z = Vector3.Dot(G, direction_);
		shape2DotProduct.w = Vector3.Dot(H, direction_);

		float minDot = Mathf.Min(shape2DotProduct.x, shape2DotProduct.y, shape2DotProduct.z, shape2DotProduct.w);

		Vector3 point2 = new Vector3(0.0f, 0.0f, 0.0f);
		if (minDot.Equals(shape2DotProduct.x))
		{
			point2 = E;
		}

		else if (minDot.Equals(shape2DotProduct.y))
		{
			point2 = F;
		}

		else if (minDot.Equals(shape2DotProduct.z))
		{
			point2 = G;
		}

		else if (minDot.Equals(shape2DotProduct.w))
		{
			point2 = H;
		}

		Vector3 newResult = point1 - point2;

		return newResult;
    }

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Rebuild();
	}
}