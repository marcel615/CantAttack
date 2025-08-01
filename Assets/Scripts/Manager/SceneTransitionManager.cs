using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static SceneTransitionManager Instance;

    public float fadeTime = 1f;

    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnSaveSlotToGameScene += SaveSlotToGameScene;
        //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnPortalToPortal += PortalToPortal;
    }

    private void OnDisable()
    {
        //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnSaveSlotToGameScene -= SaveSlotToGameScene;
        //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnPortalToPortal -= PortalToPortal;
    }
    void SaveSlotToGameScene(string targetScene)
    {

    }
    void PortalToPortal(string targetScene)
    {
        StartCoroutine(FadeOutToLoadingScene(targetScene, SceneChangeType.Portal));
    }
    IEnumerator FadeOutToLoadingScene(string targetScene, SceneChangeType sceneChangeType)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //�ε��� ����
        LoadingSceneLoader.Init(fadeTime, targetScene, sceneChangeType);

    }



}
