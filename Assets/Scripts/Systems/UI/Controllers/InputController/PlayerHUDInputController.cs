using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private PlayerHUD playerHUD;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (playerHUD == null) playerHUD = transform.Find("UICanvas/PlayerHUDPanel")?.GetComponent<PlayerHUD>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //세이브씬 로드될 때 이벤트 구독
        MapEvents.OnSavedSceneLoaded += SetPlayerHUDVisible;
        //PlayerHUD UI를 보이게 할지 조절하기 위한 이벤트
        InputEvents.PlayerHUD.OnPlayerHUDVisible += PlayerHUDVisible;
    }
    private void OnDisable()
    {
        //세이브씬 로드될 때 이벤트 구독
        MapEvents.OnSavedSceneLoaded -= SetPlayerHUDVisible;
        //PlayerHUD UI를 보이게 할지 조절하기 위한 이벤트
        InputEvents.PlayerHUD.OnPlayerHUDVisible -= PlayerHUDVisible;
    }

    //세이브씬 로드될 때 이벤트 구독
    void SetPlayerHUDVisible()
    {
        PlayerHUDVisible(true);
    }
    //PlayerHUDOpen 이벤트 구독
    void PlayerHUDVisible(bool visible)
    {
        playerHUD.PlayerHUDVisible(visible);
    }

}
