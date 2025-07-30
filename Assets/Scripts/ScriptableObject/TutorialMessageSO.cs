using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialMessage")]
public class TutorialMessageSO : ScriptableObject
{
    [Header("�ĺ��� ID (�ڵ忡�� ȣ�� �� ���)")]
    public string messageID;

    [TextArea]
    [Header("�޽��� ����")]
    public string messageKor;
}
