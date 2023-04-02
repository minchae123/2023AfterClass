using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset chatMsgTemplate;

    private UIDocument document;
    private VisualElement root;

    private List<VisualElement> chatList;
    private int idx = 0;

    private TextField txtChat;
    private Button sendBtn;
    private ScrollView chatScroll;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        root = document.rootVisualElement;
        chatList = root.Query<VisualElement>(className: "chat").ToList();

        txtChat = root.Q<TextField>("TextChat");
        sendBtn = root.Q<Button>("BtnSend");
        chatScroll = root.Q<ScrollView>("ChatContent");

        sendBtn.RegisterCallback<ClickEvent>(SendClickHandle);

        txtChat.RegisterCallback<KeyUpEvent>(e =>
        {
            if (e.keyCode == KeyCode.Return)
            {
                SendProcess();
            }
        });

    }

    private void SendProcess()
    {
        VisualElement chatXML = chatMsgTemplate.Instantiate();
        VisualElement chat = chatXML.Q<VisualElement>("ChatMsg");
        chat.AddToClassList("chat");

        Label la = chatXML.Q<Label>("MsgLabel");
        la.text = txtChat.value;
        chatScroll.Add(chatXML);
        txtChat.SetValueWithoutNotify("");
        StartCoroutine(AddOnClass(chat));
    }

    private void SendClickHandle(ClickEvent e)
    {
        SendProcess();
    }

    IEnumerator AddOnClass(VisualElement ta)
    {
        yield return new WaitForSeconds(0.1f);
        ta.AddToClassList("on");

        chatScroll.verticalScroller.value = chatScroll.verticalScroller.highValue > 0 ? chatScroll.verticalScroller.highValue : 0;
    }

    IEnumerator WaitTime(float t)
    {
        VisualElement chatXML = chatMsgTemplate.Instantiate();
        chatScroll.Add(chatXML);
        VisualElement chat = chatXML.Q<VisualElement>("ChatMsg");
        chat.AddToClassList("chat");

        Label la = chatXML.Q<Label>("MsgLabel");
        la.text = txtChat.value;
        yield return new WaitForSeconds(t);
        chat.AddToClassList("on");
    }

    private void Load()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (idx < chatList.Count)
            {
                chatList[idx].AddToClassList("on");
                idx++;
            }
            else
            {
                chatList.ForEach(x => x.RemoveFromClassList("on"));
            }
        }
    }
}
