using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactionHandler : MonoBehaviour
{
    EnemyFSM FSM;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //�˹� ���� ����
    Vector2 KnockbackDir; //�˹� ����
    float KnockbackPower;

    //�ǰ� �� ����Ǵ� �ð� �ڷ�ƾ
    Coroutine hitColorCoroutine;
    //�÷��� ����Ǵ� �ð� �ڷ�ƾ
    Coroutine KnockbackFlagCouroutine;


    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void HitWithKnockback(float hitColorTime, Vector2 hittedPos)
    {
        KnockbackPower = FSM.enemyController.knockbackPower;

        //�˹���� ����
        SetKnockbackDir(hittedPos);
        //�˹� �ֱ�
        rigid.AddForce(KnockbackDir * KnockbackPower, ForceMode2D.Impulse);
        //�˹� �÷��� ��ȭ
        CancelChangeFlag();
        KnockbackFlagCouroutine = StartCoroutine(ChangeKnockbackFlag());

        // ���� ��ȭ
        CancelhitColor(); //���� �������� �ǰݵ� ��� �ڷ�ƾ ��ġ�°� ����
        hitColorCoroutine = StartCoroutine(ChangeColor(hitColorTime));
    }
    public void HitWithoutKnockback(float hitColorTime)
    {
        // ���� ��ȭ
        CancelhitColor(); //���� �������� �ǰݵ� ��� �ڷ�ƾ ��ġ�°� ����
        StartCoroutine(ChangeColor(hitColorTime));
    }
    void SetKnockbackDir(Vector2 hittedPos)
    {
        KnockbackDir = ((Vector2)transform.position - hittedPos).normalized;
    }
    IEnumerator ChangeKnockbackFlag()
    {
        //�ǰ� �� �˹�����÷��� ����
        FSM.enemyController.isKnockbacked = true;

        // knockbackCantMoveTime��ŭ ��ٷȴٰ�
        yield return new WaitForSeconds(FSM.enemyController.knockbackCantMoveTime);

        //�˹�����÷��� ������Ʈ
        FSM.enemyController.isKnockbacked = false;
    }
    void CancelChangeFlag()
    {
        // ���� Ÿ�̸� ���
        if (KnockbackFlagCouroutine != null)
        {
            StopCoroutine(KnockbackFlagCouroutine);
        }
    }
    IEnumerator ChangeColor(float hitColorTime)
    {
        //�ǰ� �� �������ϰ� ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // hitColorTime��ŭ ��ٷȴٰ�
        yield return new WaitForSeconds(hitColorTime);

        //�ǰ� �� �������ϰ� �Ǿ����� �ǵ�����
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
    void CancelhitColor()
    {
        // ���� Ÿ�̸� ���
        if (hitColorCoroutine != null)
        {
            StopCoroutine(hitColorCoroutine);
        }
    }
}
