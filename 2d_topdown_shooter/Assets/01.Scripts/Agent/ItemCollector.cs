using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] float magenticRange = 2f, magneticPower = 1f;

    [SerializeField] private LayerMask whatIsItem;

    private List<ItemScript> collectList = new List<ItemScript>(); 

    public UnityEvent<int> OnAmmoAdded = null;
    public UnityEvent<int> OnHealthAdded = null;

    private void FixedUpdate()
    {
        Collider2D[] resources = Physics2D.OverlapCircleAll(transform.position, magenticRange, whatIsItem);
        
        foreach(Collider2D r in resources)
        {
            if(r.TryGetComponent<ItemScript>(out ItemScript item))
            {
                collectList.Add(item);
                item.gameObject.layer = 0;
            }
        }

        for(int i = 0; i < collectList.Count; i++)
        {
            ItemScript item = collectList[i];
            Vector2 dir = (transform.position - item.transform.position).normalized;
            item.transform.Translate(dir * magneticPower * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, item.transform.position) < 0.1f)
            {
                int value = item.ItemData.GetAmount();

                PopupText text = PoolManager.Instance.Pop("PopupText") as PopupText;
                text.Setup(value.ToString(), transform.position + new Vector3(0, 0.5f, 0), item.ItemData.popupTextColor);

                ProcessItem(item.ItemData.itemType, value);
                item.PickUpResource();
                collectList.RemoveAt(i);
                i--;
            }
        }
    }

    private void ProcessItem(ItemType type, int value)
    {
        switch (type)
        {
            case ItemType.Ammo:
                OnAmmoAdded?.Invoke(value);
                break;
            case ItemType.Health:
                OnHealthAdded?.Invoke(value);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, magenticRange);
            Gizmos.color = Color.white;
        }
    }
}
