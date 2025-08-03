using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static GameManager Instance { get; private set; }

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
    //�̺�Ʈ ����
    private void OnEnable()
    {
        //GameManager���� TimeScale �ٲ�޶�� ��û�ϴ� �̺�Ʈ
        SystemEvents.OnChangeTimeScale += ChangeTimeScale;
    }
    private void OnDisable()
    {
        //GameManager���� TimeScale �ٲ�޶�� ��û�ϴ� �̺�Ʈ
        SystemEvents.OnChangeTimeScale += ChangeTimeScale;
    }

    void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }


}
