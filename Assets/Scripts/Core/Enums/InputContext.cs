using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputContext
{
    Boot,         // 부팅 중
    Player,       // 플레이어
    ParryMode,    // 패리 모드 변경 UI
    SystemMenu,   // 시스템 메뉴 UI
    Setting,      // 세팅 UI
    MainMenu,     // 메인 메뉴 UI
    SaveSlot,     // 세이브 슬롯 리스트 UI
    Tutorial,     // 튜토리얼 UI
    Dialogue,     // 메시지(Dialogue) UI
    Whatever,     // Context에 영향을 주지 않는 UI (Tutorial UI 등)
    SceneChange,  // 씬 간에 이동할 때
    Fade,         // Fade In & Out UI
    PlayerDead,   // 플레이어가 사망할 때
    Sequence,     // 연출이 시작될 때
    SavePointMenu // 세이브포인트에서 메뉴를 열 때
    //Inventory,    // 인벤토리
    //DialogueUI
}
