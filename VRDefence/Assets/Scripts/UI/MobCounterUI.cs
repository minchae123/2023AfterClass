using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobCounterUI : MonoBehaviour
{
    private int killCount;
    private int spawnCount;

    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public void OnSpawn()
    {
        spawnCount++;
        UpdateUI();
    }

    public void OnKill()
    {
        killCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!enabled) return;

        textUI.text=$"Kill/Alive/Spawn\n{killCount}/{spawnCount-killCount}/{spawnCount}";
    }

    private void OnEnable()
    {
        killCount = spawnCount = 0;
        UpdateUI();
    }
}
