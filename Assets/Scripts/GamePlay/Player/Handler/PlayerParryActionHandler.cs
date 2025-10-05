using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryActionHandler : MonoBehaviour
{
    PlayerController playerController;

    //현재 선택된 방패 및 패리 모드
    ShieldDataSO currentShield;
    ParryModeDataSO currentParryMode;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        //현재 선택된 방패가 업데이트 되었을 때
        PlayerEvents.OnCurrentShieldUpdated += CurrentShieldUpdated;

        //현재 선택된 패리모드가 업데이트 되었을 때
        PlayerEvents.OnCurrentParryModeUpdated += CurrentParryModeUpdated;

        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess += ParrySuccess;

        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried += ProjectileParried;

    }
    private void OnDisable()
    {
        //현재 선택된 방패가 업데이트 되었을 때
        PlayerEvents.OnCurrentShieldUpdated -= CurrentShieldUpdated;

        //현재 선택된 패리모드가 업데이트 되었을 때
        PlayerEvents.OnCurrentParryModeUpdated -= CurrentParryModeUpdated;

        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess -= ParrySuccess;

        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried -= ProjectileParried;
    }
    //현재 선택된 방패가 업데이트 되었을 때
    void CurrentShieldUpdated(ShieldDataSO curShield, int curIndex)
    {
        currentShield = curShield;
    }
    //현재 선택된 패리모드가 업데이트 되었을 때
    void CurrentParryModeUpdated(ParryModeDataSO curParryMode, int curIndex)
    {
        currentParryMode = curParryMode;
    }

    //PlayerParry가 성공했을 때
    void ParrySuccess()
    {
        //패리 성공 보상
        //일시적 무적
        playerController.InvincibleTimer = playerController.parrySuccessInvincibleTime;
        playerController.isInvincible = true;
        //공중에서 기술 사용횟수 초기화
        playerController.isParriedInAir = false;
        playerController.isDashedInAir = false;
        playerController.jumpCount = 1;
    }

    //PlayerParry가 투사체 패링에 성공했을 때
    void ProjectileParried(ProjectileBase prefab, GameObject sender)
    {
        //반사
        GameObject ball = Instantiate(prefab.gameObject, prefab.gameObject.transform.position, Quaternion.identity);
        ball.GetComponent<ProjectileBase>().SetTarget(sender, gameObject);
        ball.layer = LayerMask.NameToLayer("PlayerAttack");

    }


}
