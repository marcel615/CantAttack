using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : ItemBase
{
    public override void OnAcquire()
    {
        List<string> skillMessages = new List<string>();
        skillMessages.Add(itemDataSO.description);

        //Dialogue 열고 스킬 설명 리스트도 전달하도록 구현
        InputEvents.Dialogue.InvokeDialogueOpen(InputContext.Player, skillMessages);

        if(itemDataSO.itemName == "DoubleJump")
        {
            PlayerEvents.InvokeDoubleJumpUnlock();
        }
    }
}
