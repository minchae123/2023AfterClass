using System;
using UnityEngine;

public enum CharacterStack
{
    Int = 0, Hp = 1, Str = 2
}

[Serializable]
public class ItemAbility
{
    public CharacterStack characterStack;
    public int valStack;

    [SerializeField] private int min;
    public int Min => min;
    [SerializeField] private int max;
    public int Max => max;

    public ItemAbility(int min, int max)
    {
        this.min = min;
        this.max = max;

        GetStackVal();
    }

    public void GetStackVal()
    {
        valStack = UnityEngine.Random.Range(min, max);
    }

    public void AddStackVal(ref int v)
    {
        v += valStack;
    }
}
