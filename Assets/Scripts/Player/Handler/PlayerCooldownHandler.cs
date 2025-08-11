using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownHandler : MonoBehaviour
{
    PlayerController playerController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        //타이머 관련

        //무적시간 타이머
        if (playerController.isInvincible)
        {
            if (playerController.InvincibleTimer > 0)
            {
                playerController.InvincibleTimer -= Time.deltaTime;
            }
            else
            {
                playerController.InvincibleTimer = 0;
                playerController.isInvincible = false;
            }
        }
        //회피기 쿨타임
        if (playerController.isDashCoolTime)
        {
            if (playerController.dashCoolTimer > 0)
            {
                playerController.dashCoolTimer -= Time.deltaTime;
            }
            else
            {
                playerController.dashCoolTimer = 0;
                playerController.isDashCoolTime = false;
            }
        }
        //패링기 쿨타임
        if (playerController.isParryCoolTime)
        {
            if (playerController.parryCoolTimer > 0)
            {
                playerController.parryCoolTimer -= Time.deltaTime;
            }
            else
            {
                playerController.parryCoolTimer = 0;
                playerController.isParryCoolTime = false;
            }
        }
    }
}
