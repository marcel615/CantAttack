using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static MapManager Instance;

    //�� �������� LocalMapManager
    public LocalMapManager localMapManager;

    //��Ż�� �� �̵� �� ���� ����
    string enterPortalID;
    string targetScene;
    string targetPortalID;
    Vector2 targetPortalPos;
    bool isPortalSceneChange;




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
        //PlayerPortal���� �� �̵��϶�� �̺�Ʈ ����
        MapEvents.OnRequestMapMove_Portal += MoveScene_Portal;
        //�� �ε� �� �̺�Ʈ
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        //LocalMapManager�� Awake()���� �̺�Ʈ ����
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
        //Portal���� Portal ���� �� �̺�Ʈ ����
        PortalEvents.OnPortalEnter -= EnterPortal;
        //PlayerPortal���� �� �̵��϶�� �̺�Ʈ ����
        MapEvents.OnRequestMapMove_Portal -= MoveScene_Portal;
        //�� �ε� �� �̺�Ʈ
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;        
    }

    void EnterPortal(string enterP, string targetS, string targetP)
    {
        enterPortalID = enterP;
        targetScene = targetS;
        targetPortalID = targetP;
    }
    void MoveScene_Portal()
    {
        isPortalSceneChange = true;
        //Map �ٲٰڴٰ� �̺�Ʈ ����
        MapEvents.InvokeStartChangeScene();

        //���̵� ���� �� �ε� �� ���� �� Ÿ�� �� �ε�
        FadeEvents.InvokeFadeOpen(targetScene, FadeDirection.FadeOut);
        //�ε����� ���ؼ� Ÿ�� �� �ε�
        //LoadingSceneLoader.LoadScene(targetScene);
        //SceneManager.LoadScene(targetScene);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(isPortalSceneChange && scene.name != "LoadingScene")
        {
            targetPortalPos = localMapManager.GetPortalPos(targetPortalID);
            MapEvents.InvokeGetPlayerPos(targetPortalPos);
            isPortalSceneChange = false;
        }
    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
