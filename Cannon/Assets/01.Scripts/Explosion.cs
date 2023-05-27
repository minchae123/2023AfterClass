using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }


    public void PlayParticle( )
    {
        _particleSystem.Play();

        Destroy(gameObject, 2f);
    }

}
