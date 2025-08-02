using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static GameManager Instance { get; private set; }

    //���̺꽽�� ����
    public int slotCount = 30;


    //���� ������ �׽�Ʈ���� üũ�ϴ� �÷���
    public bool isTest;


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

    void SetTimeScale()
    {
        /*
        if (context == InputContext.Player)
        {
            Time.timeScale = 1;
        }
        
        else if()
        {
            if(context != InputContext.SceneChange)
            {
                Time.timeScale = 0;
            }
        }
        */
    }


}
