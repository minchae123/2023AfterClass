using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private void Awake() {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        enemyWaveManager.OnWaveNumverChanged += EnemyWaveManager_OnWaveNumverChange;
    }

    private void EnemyWaveManager_OnWaveNumverChange(object sender, System.EventArgs e)
    {
            SetWaveNumberText("Wave" + enemyWaveManager.GetWaveNumver());

    }
    private void Update() {
        float waveSpawnerTimer = enemyWaveManager.GetNextSpawnerTimer();
        if(waveSpawnerTimer <= 0)
        {
            SetWaveMessageText("");
        }
        else{
            SetWaveMessageText($"Next Wave in" + waveSpawnerTimer.ToString("F1")+"s");
        }
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }

        private void SetWaveMessageText(string text)
    {
        waveMessageText.SetText(text);
    }
}

