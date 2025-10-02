using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Portal")]
public class PortalDataSO : ScriptableObject
{
    [Header("Æ÷Å»ID")]
    public string portalID;
    [Header("Æ÷Å»º° µµÂø Position")]
    public Vector2 position;
    [Header("Æ÷Å»º° ÁøÀÔ ¹æÇâ")]
    public PortalWalkDirection portalWalkDirection;

    [Header("¿¬°á ¾À ÀÌ¸§")]
    public string targetScene;
    [Header("¿¬°á µÇ´Â Æ÷Å»ID")]
    public string targetPortalID;

}
