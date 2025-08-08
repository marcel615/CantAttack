using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    //�� ������Ʈ
    Player player;
    Rigidbody2D rigid;
    Animator animator;
    CapsuleCollider2D detectCollider;
    PlayerMove playerMove;

    //���� ������
    float PortalMoveTime = 0.3f;
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
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
                //��Ż���� ���� �ö󰡴� ��� ����� ���⼭
                if(WalkDir != PortalWalkDirection.Up)
                {
                    playerMove.Move(H);
                    PortalMoveTimer -= Time.fixedDeltaTime;
                }
                else  //��Ż���� ���� �ö󰡴� ���� ���⼭ ó��
                {
                    rigid.velocity = new Vector2(H * player.normalSpeed, 1f);
                }
            }
            else
            {
                PortalMoveTimer = 0;
                player.isPortalEnter = false;

                if (isTargetScene)
                {
                    isTargetScene = false;
                    //Context ���� �̺�Ʈ
                    InputEvents.InvokeContextUpdate(InputContext.Player);
                }
            }
        }
    }

    private void OnEnable()
    {
        //Portal.cs���� Portal ���� �� �̺�Ʈ 
        PortalEvents.OnPortalEnter += EnterPortal;
        //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
        MapEvents.OnGetPlayerPos += TargetScene;
    }
    private void OnDisable()
    {
        //Portal.cs���� Portal ���� �� �̺�Ʈ 
        PortalEvents.OnPortalEnter -= EnterPortal;
        //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
        MapEvents.OnGetPlayerPos -= TargetScene;
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
    void TargetScene(Vector2 pos)
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = pos;
        //���� �ݶ��̴�, ��Ʈ�ڽ� �ѱ�
        detectCollider.enabled = true;
        player.playerHitBoxCollider.enabled = true;

        //�÷��� ����
        isTargetScene = true;

        //��Ż���� ���� �ö󰡴� ��쿡�� Ÿ�پ� ��Ż�� Ÿ�� �ʱ� ������ ���⼭ �÷��� �ʱ�ȭ �� Context ��ȯ
        if (WalkDir == PortalWalkDirection.Up)
        {
            isTargetScene = false;
            player.isPortalEnter = false;

            rigid.velocity = new Vector2(0, 0);
            animator.SetTrigger("isIdle");

            //Context ���� �̺�Ʈ
            InputEvents.InvokeContextUpdate(InputContext.Player);
        }
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
                H = 0;
                break;

            case PortalWalkDirection.Down:
                H = 0;
                break;
        }
    }
}
