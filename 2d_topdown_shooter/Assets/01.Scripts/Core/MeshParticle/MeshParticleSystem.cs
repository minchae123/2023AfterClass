using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;

    [Serializable] public struct ParticleUVPixel
    {
        public Vector2Int uv00Pixel;
        public Vector2Int uv11Pixel;
    }

    public struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField]
    private ParticleUVPixel[] uvPixelArr;
    private UVCoords[] uvCoordArr;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    private bool updateVertices;
    private bool updateUV;
    private bool updateTriangles; 

    private void Awake()
    {
        mesh = new Mesh();
        vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        triangles = new int[6 * MAX_QUAD_AMOUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // 메시의 경계바운드가 작으면 특정 좌표 이상 화면 밖으로 나가면 메시 전체가 그려지지 않는다.
        // 따라서 경계 바운드를 넓혀줘야한다.

        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Agent"; // 레이어 설정 코드로 하기
        meshRenderer.sortingOrder = 0; // 플레이어나 적군보다는 아래쪽

        Texture mainTex = meshRenderer.material.mainTexture;
        int tWidth = mainTex.width;
        int tHeight = mainTex.height;

        List<UVCoords> uvCoordList = new List<UVCoords>();

        foreach(ParticleUVPixel pixelUV in uvPixelArr)
        {
            UVCoords temp = new UVCoords
            {
                uv00 = new Vector2((float)pixelUV.uv00Pixel.x / tWidth, (float)pixelUV.uv00Pixel.y / tHeight),
                uv11 = new Vector2((float)pixelUV.uv11Pixel.x / tWidth, (float)pixelUV.uv11Pixel.y / tHeight),
            };
            uvCoordList.Add(temp);
        }
        uvCoordArr = uvCoordList.ToArray();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3 quadSize = new Vector3(1, 1, 0);
            float rot = 0;
            int uvIndex = GetRandomeShellIndex();
            int qIndex = AddQuad(pos, rot, quadSize, false, uvIndex);
            FunctionUpdater.Instance.Create(() =>
            {
                pos += new Vector3(1, 1) * 0.8f * Time.deltaTime;
                quadSize += new Vector3(1, 1) * Time.deltaTime;
                rot += 360f * Time.deltaTime;

                UpdateQuad(quadIndex, pos, rot, quadSize, false, uvIndex);
            });
        }        
    }

    public int GetRandomBloodIndex()
    {
        return Random.Range(0, 7);
    }

    public int GetRandomeShellIndex()
    {
        return Random.Range(8, 9);
    }

    private int quadIndex = 0;

    public int AddQuad(Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIdx)
    {
        UpdateQuad(quadIndex, pos, rot, quadSize, skewed, uvIdx);

        int spawnedQuadIndex = quadIndex;
        quadIndex = (quadIndex + 1) % MAX_QUAD_AMOUNT;

        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIdx, Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIdx)
    {
        int vIndex0 = quadIdx * 4;
        int vIndex1 = vIndex0 + 1;
        int vIndex2 = vIndex0 + 2;
        int vIndex3 = vIndex0 + 3;

        if (skewed)
        {

        }
        else
        {
            vertices[vIndex0] = pos + Quaternion.Euler(0, 0, rot - 180) * quadSize;
            vertices[vIndex1] = pos + Quaternion.Euler(0, 0, rot - 270) * quadSize;
            vertices[vIndex2] = pos + Quaternion.Euler(0, 0, rot - 0) * quadSize;
            vertices[vIndex3] = pos + Quaternion.Euler(0, 0, rot - 90) * quadSize;
        }

        UVCoords _uv = uvCoordArr[uvIdx];
        uv[vIndex0] = _uv.uv00;
        uv[vIndex1] = new Vector2(_uv.uv00.x, _uv.uv11.y);
        uv[vIndex2] = _uv.uv11;
        uv[vIndex3] = new Vector2(_uv.uv11.x, _uv.uv00.y);

        int tIndex = quadIdx * 6;
        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        updateVertices = true;
        updateUV = true;
        updateTriangles = true;
    }

    private void LateUpdate()
    {
        if (updateVertices)
        {
            mesh.vertices = vertices;
            updateVertices = false;
        }

        if (updateUV)
        {
            mesh.uv = uv;
            updateUV = false;
        }

        if (updateTriangles)
        {
            mesh.triangles = triangles;
            updateTriangles = false;
        }
    }

    public void DestroyQuad(int quadIdx)
    {
        int vIndex0 = quadIndex * 4;
        int vIndex1 = quadIndex + 1;
        int vIndex2 = quadIndex + 2;
        int vIndex3 = quadIndex + 3;

        vertices[vIndex0] = Vector3.zero;
        vertices[vIndex1] = Vector3.zero;
        vertices[vIndex2] = Vector3.zero;
        vertices[vIndex3] = Vector3.zero;

        updateVertices = true;
    }

    public void DestroyAllQuad()
    {
        Array.Clear(vertices, 0, vertices.Length);
        quadIndex = 0;
        updateVertices = true;
    }
}
