using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour,IReloadable
{
    public UnityEvent<int> OnBulletsChanged;
    public UnityEvent<float> OnChargeChanged;

    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadEnd;

    public int maxBullets = 20;
    public float chargintTime = 2f;
    private int currentBullets;

    private int CurrentBullets
    {
        get => currentBullets;
        set
        {
            if (value < 0)
                currentBullets = 0;
            else if (value > maxBullets)
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnBulletsChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartReload()
    {
        if (CurrentBullets == maxBullets) return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }
    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();

        var beginTime = Time.time;
        var beginBullets = CurrentBullets;
        var enoughPercent = 1f - ((float)CurrentBullets / maxBullets);
        var enoughChargingTime = chargintTime * enoughPercent;

        while (true)
        {
            var t = (Time.time - beginTime) / enoughChargingTime;
            if (t >= 1f) break;

            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t);
            yield return null;
        }
        CurrentBullets = maxBullets;

        OnReloadEnd?.Invoke();
    }

}
