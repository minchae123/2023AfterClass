using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private HeartUI heartUIPrefab;

    private List<HeartUI> childHearts = null;

    [SerializeField] private Sprite fullHeart, emptyHeart;

    public void InitSetup(int count)
    {
        childHearts = new List<HeartUI>();
        for(int i = 0; i < count; i++)
        {
            HeartUI heart = Instantiate<HeartUI>(heartUIPrefab, transform);
            childHearts.Add(heart);
        }
    }

    public void ChangeHeartUI(int cur, int max)
    {
        for(int i = 0; i < childHearts.Count; i++)
        {
           childHearts[i].SetSprite(i < cur ? fullHeart : emptyHeart);
        }
    }
}
