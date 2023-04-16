using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavAgent : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool cornerCheck = false;
    
    private Vector3 nextPos;
    private int moveIdx = 0;
    private bool isMoving = false; // �̵� ������

    public bool IsArrived = false; // �����ߴ°�

    private PriorityQueue<Node> openList;
    private List<Node> closeList; // �湮�� ��

    private List<Vector3Int> routePath;

    private Vector3Int curPos; // ���� ��ġ
    private Vector3Int destination; // ������
    public Vector3Int Destiation
    {
        get => destination;
        set
        {
            SetCurrentPos();
            destination = value;
            isMoving = CalcRoute();
            moveIdx = 0;
            if(routePath.Count > 0) 
                nextPos = TileMapManager.Instance.GetWolrdPos(routePath[0]);
            DrawRoutePath();
            IsArrived = false;
        }
    }

    public LineRenderer line;

    // �� ������ǥ�� Ÿ�� ��ǥ�� �ٲ㼭 ����
    private void SetCurrentPos()
    {
        curPos = TileMapManager.Instance.GetTilePos(transform.position);
    }

    private void Awake()
    {
        openList = new PriorityQueue<Node>();
        closeList = new List<Node>();
        routePath = new List<Vector3Int>();
        line = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();
    }

    private void Start()
    {
        SetCurrentPos();
        transform.position = TileMapManager.Instance.GetWolrdPos(curPos);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3Int cellPos = TileMapManager.Instance.GetTilePos(pos);
            Destiation = cellPos;
        }

        if (isMoving)
        {
            Vector2 dir = (nextPos - transform.position).normalized;

            transform.Translate(dir * speed * Time.deltaTime, Space.World);

            if(Vector2.Distance(nextPos, transform.position) < 0.1f)
            {
                if (GetNextTarget() == false)
                {
                    IsArrived = true; // ����
                    isMoving = false;
                }
            }
        }
    }

    private bool GetNextTarget()
    {
        moveIdx++;
        if (moveIdx >= routePath.Count)
            return false;
        nextPos = TileMapManager.Instance.GetWolrdPos(routePath[moveIdx]);
        return true;
    }

    private void DrawRoutePath()
    {
        line.enabled = true;
        line.positionCount = routePath.Count;
        line.SetPositions(routePath.Select(p => TileMapManager.Instance.GetWolrdPos(p)).ToArray());
    }

    public void StopImmediately()
    {
        isMoving = false;
    }

    public bool CalcRoute()
    {
        openList.Clear();
        closeList.Clear();

        openList.Push(new Node
        {
            cellPos = curPos,
            parent = null,
            G = 0,
            F = CalcH(curPos)
        }); // �� ó�� ���¸���Ʈ�� ���� �ִ� ��

        bool result = false;

        int cnt = 0;
        while(openList.Count > 0)
        {
            Node n = openList.Pop(); // ���� ������ �� �༮
            FindOpenList(n);
            closeList.Add(n);

            if(n.cellPos == destination)
            {
                result = true;
                break;
            }

            cnt++;
            if (cnt >= 10000)
            {
                Debug.Log("���� !!!!");
                break;
            }
        }

        if (result)
        {
            routePath.Clear();
            Node last = closeList[closeList.Count - 1]; // ������ ���
            while(last.parent != null)
            {
                routePath.Add(last.cellPos);
                last = last.parent;
            }

            //routePath.Add(curPos);
            routePath.Reverse();
        }
        return result;
    }

    private void FindOpenList(Node n)
    {
        for(int y = -1; y <= 1; y++)
        {
            for(int x = -1; x <= 1; x ++)
            {
                if (x == y && x == 0) continue;

                if(cornerCheck && (Mathf.Abs(x) + Mathf.Abs(y) == 2))
                {
                    Vector3Int corner = n.cellPos + new Vector3Int(x, 0);
                    if (TileMapManager.Instance.CanMove(corner) == false) continue;
                    corner = n.cellPos + new Vector3Int(0, y);
                    if (TileMapManager.Instance.CanMove(corner) == false) continue;
                }

                Vector3Int nextPos = n.cellPos + new Vector3Int(x, y, 0);
                Node temp = closeList.Find(x => x.cellPos == nextPos);
                if (temp != null) continue;

                if (TileMapManager.Instance.CanMove(nextPos))
                {
                    float g = (n.cellPos - nextPos).magnitude + n.G;

                    Node nextOpenNode = new Node
                    {
                        cellPos = nextPos,
                        parent = n,
                        G = g,
                        F = g + CalcH(nextPos)
                    };

                    Node exist = openList.Contains(nextOpenNode);
                    if(exist == null)
                    {
                        openList.Push(nextOpenNode);
                    }
                }
            }
        }
    }

    private float CalcH(Vector3Int pos)
    {
        Vector3Int dis = destination - pos;
        return dis.magnitude;
    }
}
