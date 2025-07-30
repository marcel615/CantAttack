using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCMessage")]
public class NPCMessageSO : ScriptableObject
{
    [Header("�ĺ��� ID (�ڵ忡�� ȣ�� �� ���)")]
    public string messageID;

    [TextArea]
    [Header("�޽��� ����")]
    public string messageKor;
}
