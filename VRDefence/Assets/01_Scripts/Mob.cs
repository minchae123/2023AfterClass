using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mob : MonoBehaviour
{
    private float hueMin = 0, hueMax = 1;
    private float saturationMin = 0.7f, saturationMax = 1;
    private float valueMin = 0.7f, valueMax = 1;

    public float arrangeRange = 0.5f;

    public float emissionIntensity = 5f;

    public float destroyDelay = 1f;
    private bool isDestroy = false;

    public ParticleSystem environmentParticle;
    public MeshRenderer holeMeshRenderer;

    public ParticleSystem destroyParticle;
    public AudioSource destroyAudio;
    public GameObject modelObj;
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(new Vector3(-3f, 5f, 2f));
        agent.speed *= Random.Range(0.8f, 1.5f);
        RandomColor();

        Invoke(nameof(Destroy), 3f);
    }

    private void RandomColor()
    {
        var color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        var main = environmentParticle.main;

        main.startColor = new ParticleSystem.MinMaxGradient(color, color * Random.Range(1f - arrangeRange, 1 + arrangeRange));

        var renderer = environmentParticle.GetComponent<ParticleSystemRenderer>();
        renderer.material.SetColor("_EmissionColor", color * emissionIntensity);
        holeMeshRenderer.material.SetColor("_EmissionColor", color * emissionIntensity);


        main = destroyParticle.main;

        main.startColor = new ParticleSystem.MinMaxGradient(color, color * Random.Range(1f - arrangeRange, 1 + arrangeRange));

        renderer = destroyParticle.GetComponent<ParticleSystemRenderer>();
        renderer.material.SetColor("_EmissionColor", color * emissionIntensity);
    }

    private void Destroy()
    {
        if (isDestroy)
            return;
        isDestroy = true;

        destroyParticle.Play();
        destroyAudio.Play();

        environmentParticle.Stop();
        agent.enabled = false;
        modelObj.SetActive(false);

        Destroy(gameObject, destroyDelay);
    }
}
