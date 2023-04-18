using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUIManager : MonoBehaviour
{
    private TextMeshProUGUI tmpCurAmmo;
    private TextMeshProUGUI tmpMaxAmmo;

    private void Awake()
    {
        tmpCurAmmo = transform.Find("TxtCurrent").GetComponent<TextMeshProUGUI>();
        tmpMaxAmmo = transform.Find("TxtMax").GetComponent<TextMeshProUGUI>();
    }

    public void SetAmmo(int cur, int count)
    {
        tmpCurAmmo.SetText(cur.ToString());
        tmpMaxAmmo.SetText(count.ToString());
    }

    public void SetCurrentAmmo(int cur)
    {
        tmpCurAmmo.SetText(cur.ToString());
    }
}
