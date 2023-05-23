using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, Transform> resourceTrmDic;
    private ResourceTypeListSO resourceTypeList;
    private Transform resourceTemplete;

    private void Awake() {
        resourceTrmDic = new Dictionary<ResourceTypeSO, Transform>();
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTemplete = transform.Find("ResourceTemplete");
        resourceTemplete.gameObject.SetActive(false);

        int index = 0;
        foreach(ResourceTypeSO r in resourceTypeList.list)
        {
            Transform resourceTrm = Instantiate(resourceTemplete, transform);
            resourceTrm.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resourceTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index,0);

            resourceTrm.Find("Image").GetComponent<Image>().sprite = r.sprite;

            resourceTrmDic[r] = resourceTrm;
            index++;
        }

    }

    private void Start() {
        foreach(ResourceTypeSO r in resourceTypeList.list)
        {   Transform resourceTrm = resourceTrmDic[r];
            int amout = ResourceManager.Instance.GetResourceAmount(r);
            resourceTrm.Find("Text").GetComponent<TextMeshProUGUI>().SetText(amout.ToString());
        }
    }
}
