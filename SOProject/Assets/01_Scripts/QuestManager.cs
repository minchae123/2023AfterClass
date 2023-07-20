using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestDataContainer questDataContainer;

    private void Start()
    {
        questDataContainer = Resources.Load<QuestDataContainer>("QuestDataController");
        
        foreach (var questDB in questDataContainer.questDataList)
        {
            Debug.Log($"퀘스트 이름 {questDB.questName}");
            Debug.Log($"퀘스트 설명 {questDB.questDescription}");
            Debug.Log($"퀘스트 보상 {questDB.requiredLV}");
        }
    }

   /* private void LoadQuestDB()
    {
        questDataList = new List<QuestDB>();
        questDataList.Add(new QuestDB { questName = "첫 번쨰 퀘스트", questDescription = "몬스터를 처치하세요", requiredLV = 5 });
        questDataList.Add(new QuestDB { questName = "두 번쨰 퀘스트", questDescription = "아이템을 차지하세요", requiredLV = 10 });
    }*/
}
