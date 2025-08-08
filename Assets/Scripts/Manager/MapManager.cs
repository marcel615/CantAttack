using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static MapManager Instance;

    //���̺�, �ε� ����
    public string saveScene;

    //�� �������� LocalMapManager
    public LocalMapManager localMapManager;

    //��Ż�� �� �̵� �� ���� ����
    string enterPortalID;
    string targetScene;
    string targetPortalID;
    PortalWalkDirection walkDirection;
    Vector2 targetPortalPos;
    bool isPortalSceneChange;
    bool isTargetScene;

    //���̺꽽�Կ��� ���Ӿ����� �̵� �� ���� ����
    bool isSavedSceneLoaded;




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

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //LocalMapManager�� Awake()���� �̺�Ʈ ����
        MapEvents.OnLocalMapManagerInit += GetLocalMapManager;
        //Portal���� Portal ���� �� �̺�Ʈ ����
        PortalEvents.OnPortalEnter += EnterPortal;
        //�� �ε� �� �̺�Ʈ
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        //LocalMapManager�� Awake()���� �̺�Ʈ ����
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
        //Portal���� Portal ���� �� �̺�Ʈ ����
        PortalEvents.OnPortalEnter -= EnterPortal;
        //�� �ε� �� �̺�Ʈ
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;
        saveScene = localMapManager.mapDataSO.sceneName;
    }

    void EnterPortal(string enterP, string targetS, string targetP, PortalWalkDirection walkDir)
    {
        if (isTargetScene)
        {
            isTargetScene = false;  //�÷��� ���� �����ϵ���
            return;  //Ÿ�� ���� ������ ������ ��Ż�̸� �ٷ� ����
        }

        //�����Ż�� ���� ����
        enterPortalID = enterP;
        targetScene = targetS;
        targetPortalID = targetP;
        walkDirection = walkDir;

        isPortalSceneChange = true;
        isTargetScene = false;

        //�� ��ȯ �ǽ�
        SceneTransitionEvents.InvokePortalToPortal(targetScene);
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //���θ޴��� ������ ���̺꽽�� �ε��� �� �ʿ��� �÷��� false�� �ʱ�ȭ
        if(scene.name == "MainMenu")
        {
            isSavedSceneLoaded = false;
        }
        //�� �ε� �� ��Ż �� ü�������̰�, �� �̸��� ������ �����ߴ� ������Ż�� �̸��� ���ٸ�
        if(isPortalSceneChange && scene.name == targetScene)
        {
            if(walkDirection != PortalWalkDirection.Up) //WalkDirection�� Up�̾��� ��쿡�� isTargetScene�� true�� Ȱ��ȭX
                isTargetScene = true;

            targetPortalPos = localMapManager.GetPortalPos(targetPortalID);
            MapEvents.InvokeGetPlayerPos(targetPortalPos);
            isPortalSceneChange = false;
        }
        //���̺꽽�Կ������� �ε��� ���̰�, �� �̸��� ����� �� �̸��� ���ٸ�
        if (!isSavedSceneLoaded && scene.name == saveScene)
        {
            MapEvents.InvokeSavedSceneLoaded();
            //Context ������Ʈ
            InputEvents.InvokeContextUpdate(InputContext.Player);
            isSavedSceneLoaded = true;
        }
    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
