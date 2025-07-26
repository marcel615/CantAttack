using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //���̺�, �ε� ����
    public int MaxHP;
    public int CurrentHP;


    private void OnEnable()
    {
        //���̺� �ε� ���� �ʱ�ȭ
        SystemEvents.OnDataLoadFinished += InitFromSaveFileLoad;
    }
    private void OnDisable()
    {
        //���̺� �ε� ���� �ʱ�ȭ
        SystemEvents.OnDataLoadFinished -= InitFromSaveFileLoad;
    }

    //���̺� �ε� ���� �ʱ�ȭ
    void InitFromSaveFileLoad()
    {

    }
}
