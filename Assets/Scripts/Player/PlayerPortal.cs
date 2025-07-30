using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    Player player;
    Rigidbody2D rigid;

    //���� ������
    float PortalMoveTime = 2f;
    float PortalMoveTimer;
    float MoveSpeed = 6f;
    bool isPortalEnterMoving;
    float prevGravity;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //�÷��̾� ��Ż ���� �� ����� �ӵ��� �ɾ����
        if (player.isPortalEnter && !isPortalEnterMoving)
        {
            PortalMoveTimer = PortalMoveTime;
            isPortalEnterMoving = true;

            player.InvincibleTimer = PortalMoveTime;
            player.isInvincible = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(player.isHeadToRight * MoveSpeed, 0);
        }
        if (player.isPortalEnter)
        {
            if (PortalMoveTimer > 0)
            {
                rigid.velocity = new Vector2(player.isHeadToRight * MoveSpeed, 0);
                PortalMoveTimer -= Time.fixedDeltaTime;
            }
            else
            {
                PortalMoveTimer = 0;
                player.isPortalEnter = false;
                isPortalEnterMoving = false;

                rigid.gravityScale = prevGravity;
                MapEvents.InvokeRequestMapMove();
            }

        }
    }

    private void OnEnable()
    {
        PortalEvents.OnPortalEnter += EnterPortal;
        
    }
    private void OnDisable()
    {
        PortalEvents.OnPortalEnter -= EnterPortal;

    }
    void EnterPortal()
    {

        player.isPortalEnter = true;
    }
}
