using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Fade fade;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (fade == null) fade = transform.Find("UICanvas/FadePanel")?.GetComponent<Fade>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //FadeOpen �̺�Ʈ ����
        FadeEvents.OnFadeOpen += FadeOpen;
        //FadeClose �̺�Ʈ ����
        FadeEvents.OnFadeClose += FadeClose;
    }
    private void OnDisable()
    {
        //FadeOpen �̺�Ʈ ����
        FadeEvents.OnFadeOpen -= FadeOpen;
        //FadeClose �̺�Ʈ ����
        FadeEvents.OnFadeClose -= FadeClose;
    }

    //FadeOpen �̺�Ʈ ����
    void FadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        fade.FadeOpen(fadeTime, fadeDirection);
    }
    //FadeClose �̺�Ʈ ����
    void FadeClose()
    {
        fade.FadeClose();
    }

}
