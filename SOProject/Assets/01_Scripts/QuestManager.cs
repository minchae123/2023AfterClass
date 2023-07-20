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
            Debug.Log($"����Ʈ �̸� {questDB.questName}");
            Debug.Log($"����Ʈ ���� {questDB.questDescription}");
            Debug.Log($"����Ʈ ���� {questDB.requiredLV}");
        }
    }

   /* private void LoadQuestDB()
    {
        questDataList = new List<QuestDB>();
        questDataList.Add(new QuestDB { questName = "ù ���� ����Ʈ", questDescription = "���͸� óġ�ϼ���", requiredLV = 5 });
        questDataList.Add(new QuestDB { questName = "�� ���� ����Ʈ", questDescription = "�������� �����ϼ���", requiredLV = 10 });
    }*/
}
