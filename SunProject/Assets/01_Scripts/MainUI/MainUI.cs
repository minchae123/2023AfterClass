using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        // ��ť��Ʈ���� ���� ���ϴ� ������Ʈ�� �����ͼ� ������ �ϰ� �ʹ�

        VisualElement root =  document.rootVisualElement;

        Button btn = root.Q<Button>("BtnClick");
        btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("��ư Ŭ��");
            btn.style.backgroundColor = Random.ColorHSV();
        });
    }


}
