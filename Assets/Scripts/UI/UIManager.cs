using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static UIManager Instance;

    //�ڽ� ������Ʈ (UI ������Ʈ)
    //Canvas
    public GameObject UICanvas;
    //HPPanel
    public GameObject HPPanel;
    public GameObject Portrait;
    public GameObject HPContainer;
    //??Panel



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

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (UICanvas == null) UICanvas = transform.Find("UICanvas")?.gameObject;
        if (HPPanel == null) HPPanel = transform.Find("UICanvas/HPPanel")?.gameObject;
        if (Portrait == null) Portrait = transform.Find("UICanvas/HPPanel/Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("UICanvas/HPPanel/HPContainer")?.gameObject;


    }

}
