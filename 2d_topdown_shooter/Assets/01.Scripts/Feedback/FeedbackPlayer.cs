using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> feedbackToPlay = null;

    public void PlayFeedback()
    {
        FinishFeedback(); //���� �ǵ�� ������ ����
        foreach (Feedback f in feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }
    
}
