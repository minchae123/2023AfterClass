using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _trailSystemPrefab;
    [SerializeField]
    private ParticleSystem _boxSystemPrefab;

    public void DestroyBox(Vector2 dir)
    {
        ParticleSystem trail = Instantiate(_trailSystemPrefab, transform.position, Quaternion.identity);
        var trailVModule = trail.velocityOverLifetime;
        trailVModule.x = dir.x;
        trailVModule.y = dir.y;
        trail.Play();

        ParticleSystem box = Instantiate(_boxSystemPrefab, transform.position, Quaternion.identity);
        
        var boxVModule = box.velocityOverLifetime;
        boxVModule.x = dir.x;
        boxVModule.y = dir.y;
        box.Play();

        Destroy(trail, 2f);
        Destroy(box, 2f);


        Destroy(gameObject);
    }

}
