using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    //�� ������Ʈ
    Player player;
    CapsuleCollider2D detectCollider;
    PlayerMove playerMove;

    //���� ������
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float H;    //�̵� ���⿡ ���� PlayerMove�� �Ѱ��� ��

    //��Ż ���� ���� ����
    bool Enter;                     //��Ż ���� ���� �� �÷���
    PortalWalkDirection WalkDir;    //��Ż ���� ���� ����

    //�÷���
    bool isTargetScene;    //Ÿ�� ���� �� true

    private void Awake()
    {
        player = GetComponent<Player>();
        detectCollider = GetComponent<CapsuleCollider2D>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void FixedUpdate()
    {
        //�÷��̾� ��Ż ���� �� ����� �ӵ��� �ɾ����
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;
            playerMove.Move(H);
        }
        Enter = false;

        if (player.isPortalEnter)
        {
            if (PortalMoveTimer > 0)
            {
                playerMove.Move(H);
                PortalMoveTimer -= Time.fixedDeltaTime;
            }
            else
            {
                PortalMoveTimer = 0;
                player.isPortalEnter = false;

                if (isTargetScene)
                {
                    isTargetScene = false;
                    //Context ���� �̺�Ʈ
                    InputEvents.InvokeContextUpdate(InputContext.SceneChange, false);
                }
            }
        }
    }

    private void OnEnable()
    {
        //Portal.cs���� Portal ���� �� �̺�Ʈ 
        PortalEvents.OnPortalEnter += EnterPortal;
        //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
        MapEvents.OnGetPlayerPos += TargetPortal;
    }
    private void OnDisable()
    {
        //Portal.cs���� Portal ���� �� �̺�Ʈ 
        PortalEvents.OnPortalEnter -= EnterPortal;
        //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
        MapEvents.OnGetPlayerPos -= TargetPortal;
    }
    //Portal.cs���� Portal ���� �� �̺�Ʈ 
    void EnterPortal(string enterP, string targetS, string targetP, PortalWalkDirection walkDir)
    {
        //�÷��� ����
        Enter = true;

        //���� ��Ż�� ���� �ٷ� ����
        if (isTargetScene) return;        

        //��� ��Ż�� ���� ����

        //���� �ݶ��̴�, ��Ʈ�ڽ� ����
        detectCollider.enabled = false;
        player.playerHitBoxCollider.enabled = false;

        //ī�޶� Follow ���� �̺�Ʈ ����
        CameraEvents.InvokeCameraFollowReset();

        //WalkDir �����ϵ��� -> ��߰� ���� ��Ż �� �� ���� �������� �����̵���
        WalkDir = walkDir;
        SetWalkDir();

    }
    //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
    void TargetPortal(Vector2 pos)
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = pos;
        //���� �ݶ��̴�, ��Ʈ�ڽ� �ѱ�
        detectCollider.enabled = true;
        player.playerHitBoxCollider.enabled = true;

        //�÷��� ����
        isTargetScene = true;
    }

    //���� WalkDir�� ���� �÷��̾ ������ ���� ���� 
    void SetWalkDir()
    {
        switch (WalkDir)
        {
            case PortalWalkDirection.Left:
                H = -1f;
                break;

            case PortalWalkDirection.Right:
                H = 1f;
                break;

            case PortalWalkDirection.Up:

                break;

            case PortalWalkDirection.Down:

                break;
        }
    }
}
