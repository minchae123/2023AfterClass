using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private ParticleSystem trailPrefab;
    [SerializeField] private ParticleSystem boxPrefab;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DestroyBox(Vector2 dir)
    {
        ParticleSystem trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);

        var trailVModule = trail.velocityOverLifetime;
        trailVModule.x = dir.x;
        trailVModule.y = dir.y;

        trail.Play();
        
        ParticleSystem box = Instantiate(boxPrefab, transform.position, Quaternion.identity);

        var boxVModule = box.velocityOverLifetime;
        boxVModule.x = dir.x;
        boxVModule.y = dir.y;

        box.Play();

        Destroy(trail.gameObject, 2f);
        Destroy(box.gameObject, 2f);
        
        Destroy(gameObject);
    }


    public void SlideBox()
    {
        Texture2D original = spriteRenderer.sprite.texture;
        Rect originRect = spriteRenderer.sprite.rect;

        Texture2D tex = new Texture2D((int)originRect.width, (int)originRect.height);
        tex.filterMode = FilterMode.Point;

        for (int i = 0; i < originRect.height; i++)
        {
            for (int j = 0; j < originRect.width; j++)
            {
                Color c = original.GetPixel((int)originRect.x + j, (int)originRect.y + i);
                if (i < j) 
                {
                    c.a = 0;
                }
                tex.SetPixel(j, i, c);
            }
        }
        tex.Apply();
        Sprite s = Sprite.Create(tex, new Rect(0, 0, originRect.width, originRect.height), Vector2.one * 0.5f, 18);

        MakeDebri(transform.position, s, new Vector2(1, 1));
    }

    public void SpeiceBox()
    {
        Texture2D original = spriteRenderer.sprite.texture;
        Rect originRect = spriteRenderer.sprite.rect;
        Vector2 dir = new Vector2(2f, 1f);
    
        float w = 3f, h = 3f;
        for(int i = 0; i < 20; i++)
        {
            float x = Random.Range(originRect.x, originRect.x + originRect.width - w);
            float y = Random.Range(originRect.y, originRect.y + originRect.height - h);

            Rect rect = new Rect(x, y, w, h);
            Sprite s = Sprite.Create(original, rect, Vector2.one * 0.5f, 18);
            MakeDebri(transform.position, s, dir * Random.Range(0.5f, 6f));
        }
        Destroy(gameObject);
    }    

    private void MakeDebri(Vector3 pos, Sprite sprite, Vector2 dir)
    {
        GameObject debri = new GameObject();
        debri.transform.position = pos;
        debri.AddComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        debri.AddComponent<SpriteRenderer>().sprite = sprite;
        debri.AddComponent<PolygonCollider2D>();
    }
}
