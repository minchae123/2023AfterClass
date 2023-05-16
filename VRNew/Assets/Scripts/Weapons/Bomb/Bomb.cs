using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public enum State
    {
        Idle,
        Drop
    }

    public float explosionRadius = 2f;
    public LayerMask explosionHittableMask;


}
