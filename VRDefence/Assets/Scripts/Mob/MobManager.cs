using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    private static MobManager instance;
    public static MobManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<MobManager>();
            return instance;
        }
    }

    public UnityEvent<Mob> OnSpawn, OnDestroy;
    private List<Mob> mobs = new List<Mob>();

    private void Awake()
    {
        instance = this;
    }

    public void OnSpawned(Mob mob)
    {
        mobs.Add(mob);
        OnSpawn?.Invoke(mob);
    }

    public void OnDestoyed(Mob mob)
    {
        if (mobs.Remove(mob))
        {
            OnDestroy?.Invoke(mob);
        }
    }
}
