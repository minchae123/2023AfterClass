using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StageData
{
    public int Level;
    public Stage Prefab;
}

[CreateAssetMenu (menuName = "SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public List<StageData> StagePair;
}
