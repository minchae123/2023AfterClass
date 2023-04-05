using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    private Vector3 quadPos;
    private Vector3 dir;
    private MeshParticleSystem meshParticleSystem;
    private int quadIndex;
    private Vector3 quadSize;
    private float rot;
    private int uvIndex;

    private float moveSpeed;
    private float slowDownFactor;

    private bool isRotate;
    public int QuadIndex => quadIndex;

    public Particle(Vector3 _quadPos, Vector3 _dir, MeshParticleSystem _meshParticleSystem, Vector3 _quadSize, float _rot, int _uvIndex, float _moveSpeed, float _slowDownFactor, bool _isRotate = false)
    {
        quadPos = _quadPos;
        dir = _dir;
        meshParticleSystem = _meshParticleSystem;
        quadSize = _quadSize;
        rot = _rot;
        uvIndex = _uvIndex;
        moveSpeed = _moveSpeed;
        slowDownFactor = _slowDownFactor;
        isRotate = _isRotate;
        quadIndex = meshParticleSystem.AddQuad(quadPos, rot, quadSize, false, uvIndex);
    }

    public void UpdateParticle()
    {
        quadPos += dir * moveSpeed * Time.deltaTime;
        if (isRotate)
        {
            rot += 360f * (moveSpeed * 0.1f) * Time.deltaTime;
        }

        meshParticleSystem.UpdateQuad(quadIndex, quadPos, rot, quadSize, false, uvIndex);

        moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;
    }

    public bool IsComplete()
    {
        return moveSpeed < 0.05f;
    }
}
