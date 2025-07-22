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
        SystemEvents.OnDataLoadFinished += InitFromSave;
    }
    private void OnDisable()
    {
        //���̺� �ε� ���� �ʱ�ȭ
        SystemEvents.OnDataLoadFinished -= InitFromSave;
    }

    //���̺� �ε� ���� �ʱ�ȭ
    void InitFromSave()
    {

    }
}
