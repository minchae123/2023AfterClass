using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotTest : MonoBehaviour
{
    public GameObject[] he;
    public SpriteRenderer[] sp;

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(he[0].transform.DOMove(new Vector3(-9.91f, 5, -3), 1));
            seq.Join(sp[0].DOFade(1, 1));
            seq.AppendInterval(0.2f);
            seq.Append(he[0].transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360));
            seq.Append(he[1].transform.DOMove(new Vector3(-8.8f, 5, -3), 1));
            seq.Join(sp[1].DOFade(1, 1));
            seq.AppendInterval(0.2f);
            seq.Append(he[1].transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360));
            seq.Append(he[2].transform.DOMove(new Vector3(-7.8f, 5, -3), 1));
            seq.Join(sp[2].DOFade(1, 1));
            seq.AppendInterval(0.2f);
            seq.Append(he[2].transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360));
            seq.AppendCallback(() => Debug.Log("--"));
        }
    }
}
