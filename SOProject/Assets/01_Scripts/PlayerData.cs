using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    [SerializeField] private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    [SerializeField] private int hp;
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] private int dmg;
    public int Damage
    {
        get { return dmg; }
        set { dmg = value; }
    }
}
