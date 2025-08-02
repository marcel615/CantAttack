using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    void SaveSlotToGameScene(int num)
    {
        //Context ���� �̺�Ʈ
        InputEvents.InvokeContextUpdate(InputContext.SceneChange, true);

        StartCoroutine(SaveSlotFadeOut(SceneChangeType.SaveSlot, null, num));
    }
    void PortalToPortal(string targetScene)
    {
        StartCoroutine(PortalFadeOut(SceneChangeType.Portal, targetScene, -1));
    }
    IEnumerator SaveSlotFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //���̺꽽�� UI �ݵ��� �̺�Ʈ ����
        InputEvents.SaveSlot.InvokeSaveSlotClose(InputContext.SceneChange);

        //�ε��� ����
        LoadingSceneLoader.Init(sceneChangeType, fadeTime, targetScene, num);

    }
    IEnumerator PortalFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //�ε��� ����
        LoadingSceneLoader.Init(sceneChangeType, fadeTime, targetScene, num);

    }



}
