using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour 
{
    private NavAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavAgent>();
    }

    private void Update()
    {
        Vector3 worldPos = TileMapManager.Instance.GetWolrdPos(agent.Destiation);

        /*if(worldPos.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);*/

        Vector3 dir =  worldPos - transform.position;
        float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, degree);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 0.4f);
    }
}
