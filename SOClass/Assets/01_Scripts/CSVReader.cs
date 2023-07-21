using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CSVReader : MonoBehaviour
{
    private static string csvFilePath = "/Resources/monsterCSV.csv";

    [MenuItem("Utill/CSVtoSO")]
    public static void SetMonster()
    {
        string[] strData = File.ReadAllLines(Application.dataPath + csvFilePath);

        foreach(string sData in strData)
        {
            string[] data = sData.Split(',');

            MonsterSO monster = ScriptableObject.CreateInstance<MonsterSO>();
            monster.monsterName = data[0];
            monster.atk = int.Parse(data[1]);
            monster.hp = int.Parse(data[2]);
            monster.mp = int.Parse(data[3]);

            AssetDatabase.CreateAsset(monster, $"Assets/Monster/{monster.monsterName}.asset");
        }
    }

}
