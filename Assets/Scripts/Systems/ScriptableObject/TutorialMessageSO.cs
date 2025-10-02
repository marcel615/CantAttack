using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialMessage")]
public class TutorialMessageSO : ScriptableObject
{
    [Header("식별용 ID (코드에서 호출 시 사용)")]
    public string messageID;

    [TextArea]
    [Header("메시지 내용")]
    public string messageKor;
}
