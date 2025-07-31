using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputContext
{
    Boot,         // ���� ��
    Player,       // �÷��̾� 
    SystemMenu,   // �ý��� �޴� UI
    Setting,      // ���� UI
    MainMenu,     // ���� �޴� UI
    SaveSlot,     // ���̺� ���� ����Ʈ UI
    Dialogue,     // �޽���(Dialogue) UI
    Whatever,     // Context�� ������ ���� �ʴ� UI (Tutorial UI ��)
    SceneChange,  // �� ���� �̵��� ��
    //Inventory,    // �κ��丮
    //DialogueUI
}
