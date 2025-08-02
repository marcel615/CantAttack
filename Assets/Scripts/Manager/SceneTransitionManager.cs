using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static SceneTransitionManager Instance;

    //���̵� �ð�
    public float fadeTime = 1f;

    //�� ��ȯ �߿� ������
    SceneChangeType sceneChangeType;
    string targetScene;
    int saveSlotNum;


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
        //���θ޴� Continue ��ư���� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnContinueToGameScene += ContinueToGameScene;
        //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnSaveSlotToGameScene += SaveSlotToGameScene;
        //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnPortalToPortal += PortalToPortal;

        //�� �ε� �� �̺�Ʈ (�ε����� �ε�Ǿ����� Ȯ���ϱ� ����)
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        //���θ޴� Continue ��ư���� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnContinueToGameScene -= ContinueToGameScene;
        //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnSaveSlotToGameScene -= SaveSlotToGameScene;
        //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
        SceneTransitionEvents.OnPortalToPortal -= PortalToPortal;

        //�� �ε� �� �̺�Ʈ (�ε����� �ε�Ǿ����� Ȯ���ϱ� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //LoadingScene �ε� �Ϸ� �� ������ ������ SceneChangeType�� ���� ������ �г� ���� �̺�Ʈ ����
        if (scene.name == "LoadingScene")
        {
            switch (sceneChangeType)
            {
                case SceneChangeType.MainMenuContinue:
                    LoadingEvents.InvokeSaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, saveSlotNum);

                    break;

                case SceneChangeType.SaveSlot:
                    LoadingEvents.InvokeSaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, saveSlotNum);

                    break;

                case SceneChangeType.Portal:
                    LoadingEvents.InvokePortalLoadingOpen(fadeTime, targetScene);

                    break;
            }
        }
        else
        {
            //Ÿ�� ���� �������� �� ���̵��� �۾� �� Context �������� ����
            StartCoroutine(TargetSceneFadeIn());
        }
    }

    void ContinueToGameScene()
    {
        //Context ���� �̺�Ʈ
        InputEvents.InvokeContextUpdate(InputContext.SceneChange, true);

        //�� ��ȯ �߿� ���� ����
        sceneChangeType = SceneChangeType.MainMenuContinue;
        targetScene = null;
        saveSlotNum = -1;

        //Fade �ڷ�ƾ ����
        StartCoroutine(ContinueFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }
    void SaveSlotToGameScene(int num)
    {
        //Context ���� �̺�Ʈ
        InputEvents.InvokeContextUpdate(InputContext.SceneChange, true);

        //�� ��ȯ �߿� ���� ����
        sceneChangeType = SceneChangeType.SaveSlot;
        targetScene = null;
        saveSlotNum = num;

        //Fade �ڷ�ƾ ����
        StartCoroutine(SaveSlotFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }
    void PortalToPortal(string targetS)
    {
        //�� ��ȯ �߿� ���� ����
        sceneChangeType = SceneChangeType.Portal;
        targetScene = targetS;
        saveSlotNum = -1;

        //Fade �ڷ�ƾ ����
        StartCoroutine(PortalFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }

    //�ڷ�ƾ��
    IEnumerator ContinueFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //���θ޴� UI �ݵ��� �̺�Ʈ ����
        InputEvents.MainMenu.InvokeMainMenuClose(InputContext.SceneChange);

        //�ε��� ����
        SceneManager.LoadScene("LoadingScene");
        //LoadingSceneLoader.Init(sceneChangeType, fadeTime, targetScene, num);

    }
    IEnumerator SaveSlotFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //���̺꽽�� UI �ݵ��� �̺�Ʈ ����
        InputEvents.SaveSlot.InvokeSaveSlotClose(InputContext.SceneChange);

        //�ε��� ����
        SceneManager.LoadScene("LoadingScene");
        //LoadingSceneLoader.Init(sceneChangeType, fadeTime, targetScene, num);

    }
    IEnumerator PortalFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //���̵�ƿ� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //�ε��� ����
        SceneManager.LoadScene("LoadingScene");
        //LoadingSceneLoader.Init(sceneChangeType, fadeTime, targetScene, num);

    }
    //Ÿ�� ���� �������� �� ���̵��� �۾� �� Context �������� ����
    IEnumerator TargetSceneFadeIn()
    {
        //���̵��� ����
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        //���ؽ�Ʈ ������Ʈ
        InputEvents.InvokeContextUpdate(InputContext.SceneChange, false);
    }



}
