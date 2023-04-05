using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGaugeUI : MonoBehaviour
{
    [SerializeField] private Transform relaodBar;

    public void ReloadGaugeNormal(float value)
    {
        relaodBar.transform.localScale = new Vector3(value, 1, 1);
    }
}
