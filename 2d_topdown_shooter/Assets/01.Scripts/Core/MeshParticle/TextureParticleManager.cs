using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureParticleManager : MonoBehaviour
{
    public static TextureParticleManager Instance;
    private MeshParticleSystem meshParticleSystem;

    private List<Particle> shellList;
    private List<Particle> bloodList;

    private void Awake()
    {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        bloodList = new List<Particle>();
        shellList = new List<Particle>();
    }

    private void Update()
    {
        for(int i =0; i < bloodList.Count; i++)
        {
            Particle p = bloodList[i];
            p.UpdateParticle();
            if (p.IsComplete())
            {
                bloodList.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < shellList.Count; i++)
        {
            Particle p = shellList[i];
            p.UpdateParticle();
            if (p.IsComplete())
            {
                shellList.RemoveAt(i);
                i--;
            }
        }

    }

    public void SpawnShell(Vector3 pos, Vector3 dir)
    {
        int uvIndex = meshParticleSystem.GetRandomeShellIndex();
        float moveSpeed = Random.Range(1.5f, 2.5f);
        Vector3 quadSize = new Vector3(0.15f, 0.15f);
        float slowDownFactor = Random.Range(2f, 2.5f);
        shellList.Add(new Particle(pos, dir, meshParticleSystem, quadSize, Random.Range(0,359f), uvIndex, moveSpeed, slowDownFactor, true));
    }

    public void SpawnBlood(Vector3 pos, Vector3 dir, float size = 1f)
    {
        int uvIndex = meshParticleSystem.GetRandomBloodIndex();
        float moveSpeed = Random.Range(0.3f, 0.5f);
        Vector3 quadSize = new Vector3(1, 1) * size;
        float slowDownFactor = Random.Range(0.8f, 1.5f);
        bloodList.Add(new Particle(pos, dir, meshParticleSystem, quadSize, Random.Range(0, 359f), uvIndex, moveSpeed, slowDownFactor, true));
    }


    public void ClearBloodAndShell()
    {
        meshParticleSystem.DestroyAllQuad();
    }
}
