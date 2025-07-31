using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    Player player;
    Rigidbody2D rigid;

    //���� ������
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float MoveSpeed = 6f;

    //���� ���� �÷���
    bool Enter;  //��Ż ���� ���� �� �÷���

    //�÷���
    bool isEnterPortal;    //��Ż�� ���� true�� �ٲ�
    bool isTargetScene;    //�� �̵� ������ true�� �ٲ�

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //�÷��̾� ��Ż ���� �� ����� �ӵ��� �ɾ����
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;
            player.InvincibleTimer = PortalMoveTime;
            player.isInvincible = true;

            rigid.velocity = new Vector2(player.isHeadToRight * MoveSpeed, 0);
        }
        Enter = false;

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

                //�����Ż������ ���� �������� Scene �����ϱ�
                if (isEnterPortal && !isTargetScene)
                {
                    //(��Ż�� ����) Map�� �ٲٶ�� ��û �̺�Ʈ ����
                    MapEvents.InvokeRequestMapMove_Portal();
                    isEnterPortal = false;
                }
                //������Ż������ ���� �������� InputContext�� Player�� �����ϱ�
                else if(isEnterPortal && isTargetScene)
                {
                    //Context ���� �̺�Ʈ
                    InputEvents.InvokeContextUpdate(InputContext.SceneChange, false);
                    //Ÿ�� ������ �̵��� ���¿��� PlayerPortalMove�� �������� ��
                    PlayerEvents.InvokePlayerPortalMoveOver();
                    isEnterPortal = false;
                    isTargetScene = false;
                }
            }

        }
    }

    private void OnEnable()
    {
        PortalEvents.OnPortalEnter += EnterPortal;
        MapEvents.OnGetPlayerPos += TargetPortal;


    }
    private void OnDisable()
    {
        PortalEvents.OnPortalEnter -= EnterPortal;
        MapEvents.OnGetPlayerPos -= TargetPortal;

    }
    void EnterPortal(string enterP, string targetS, string targetP)
    {
        Enter = true;
        isEnterPortal = true;
    }
    void TargetPortal(Vector2 pos)
    {
        isTargetScene = true;
    }
}
