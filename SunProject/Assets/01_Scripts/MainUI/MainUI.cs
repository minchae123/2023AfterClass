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
        // 도큐먼트에서 내가 원하는 오브젝트만 가져와서 뭔가를 하고 싶다

        VisualElement root =  document.rootVisualElement;

        Button btn = root.Q<Button>("BtnClick");
        btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("버튼 클릭");
            btn.style.backgroundColor = Random.ColorHSV();
        });
    }


}
