using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshTest : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            int r = Random.Range(0, 8);
            DrawBloodParticle(r);
        }
    }

    private void DrawBloodParticle(int idx)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 2);
        vertices[2] = new Vector3(2, 2);
        vertices[3] = new Vector3(2, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        uv[0] = new Vector2(idx / 8f, 0.5f);
        uv[1] = new Vector2(idx / 8f, 1);
        uv[2] = new Vector2(idx / 8f + 0.125f, 1);
        uv[3] = new Vector2(idx / 8f + 0.125f, 0.5f);

        /*uv[0] = new Vector2(1 / 8 * idx, 1);
        uv[1] = new Vector2(1 / 8 * idx, 0.5f);
        uv[2] = new Vector2(1 / 8 * (idx + 1), 0.5f);
        uv[3] = new Vector2(1 / 8 * (idx + 1), 1);*/

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }

}
