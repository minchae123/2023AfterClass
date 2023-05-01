using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(DownloadTexture());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(GetJsonData());
        }
    }

    private IEnumerator GetJsonData()
    {
        string url = "https://ddragon.leagueoflegends.com/cdn/13.8.1/data/ko_KR/champion.json";

        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        string jsonText = req.downloadHandler.text;

        LOLItemJson json = JsonUtility.FromJson<LOLItemJson>(jsonText);
        Debug.Log($"{json.version}, {json.type}");
    }

    private IEnumerator DownloadTexture()
    {
        string url = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA1MjhfMTY1%2FMDAxNjUzNzE4NjU3OTA5.-cF_5oRWg2piRpsM7K7u-ObLwEWcFyCfpni79S2Wy24g.jxVSKAR63JBKmbwoCXwFguCTN3TC7nrFAKZZxdS94rsg.JPEG.jsgmh7695%2FIMG_5122.JPG&type=sc960_832";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(req);
            Debug.Log(texture);

            float w = texture.width;
            float h = texture.height;
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, w, h), Vector2.one * 0.5f, 32);
            gameObject.AddComponent<SpriteRenderer>().sprite = sp;
        }
        else
        {
            Debug.LogError("전송실패");
            Debug.Log(req.responseCode);
            Debug.Log(req.error);
        }
    }
}
