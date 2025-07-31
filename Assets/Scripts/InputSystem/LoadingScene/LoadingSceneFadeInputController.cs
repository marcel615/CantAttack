using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneFadeInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private LoadingSceneFade fade;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (fade == null) fade = transform.Find("LoadingUICanvas/FadePanel")?.GetComponent<LoadingSceneFade>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //LoadingScene�� FadeOpen �̺�Ʈ ����
        FadeEvents.OnLoadingSceneFadeOpen += FadeOpen;
        //LoadingScene�� FadeClose �̺�Ʈ ����
        FadeEvents.OnLoadingSceneFadeClose += FadeClose;
    }
    private void OnDisable()
    {
        //LoadingScene�� FadeOpen �̺�Ʈ ����
        FadeEvents.OnLoadingSceneFadeOpen -= FadeOpen;
        //LoadingScene�� FadeClose �̺�Ʈ ����
        FadeEvents.OnLoadingSceneFadeClose -= FadeClose;
    }

    //LoadingSceneFadeOpen �̺�Ʈ ����
    void FadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        fade.FadeOpen(fadeTime, fadeDirection);
    }
    //LoadingSceneFadeOpen �̺�Ʈ ����
    void FadeClose()
    {
        fade.FadeClose();
    }
}
