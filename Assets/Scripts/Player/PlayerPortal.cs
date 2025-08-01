using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    //�� ������Ʈ
    Player player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D detectCollider;

    //���� ������
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float MoveSpeed = 6f;

    //��Ż ���� ���� ����
    bool Enter;                     //��Ż ���� ���� �� �÷���
    PortalWalkDirection WalkDir;    //��Ż ���� ���� ����

    //�÷���
    bool isEnterPortal;    //��Ż�� ���� true�� �ٲ�
    bool isTargetScene;    //�� �̵� ������ true�� �ٲ�

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectCollider = GetComponent<CapsuleCollider2D>();
    }
    private void FixedUpdate()
    {
        //�÷��̾� ��Ż ���� �� ����� �ӵ��� �ɾ����
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;

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
                    isEnterPortal = false;
                }
                //������Ż������ ���� �������� InputContext�� Player�� �����ϱ�
                else if(isEnterPortal && isTargetScene)
                {
                    isEnterPortal = false;
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
        //��� ��Ż������ ����
        if (!isTargetScene)
        {
            //���� �ݶ��̴�, ��Ʈ�ڽ� ����
            detectCollider.enabled = false;
            player.playerHitBoxCollider.enabled = false;

            //ī�޶� Follow ���� �̺�Ʈ ����
            CameraEvents.InvokeCameraFollowReset();

            //WalkDir �����ϵ��� -> ��߰� ���� ��Ż �� �� ���� �������� �����̵���
            WalkDir = walkDir;
            SetWalkDir();
        }
        //�÷��� ����
        Enter = true;
        isEnterPortal = true;
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
                player.isHeadToRight = -1f;
                spriteRenderer.flipX = true;
                break;

            case PortalWalkDirection.Right:
                player.isHeadToRight = 1f;
                spriteRenderer.flipX = false;
                break;

            case PortalWalkDirection.Up:

                break;

            case PortalWalkDirection.Down:

                break;
        }
    }
}
