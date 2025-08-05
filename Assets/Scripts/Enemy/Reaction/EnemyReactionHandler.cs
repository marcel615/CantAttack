using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactionHandler : MonoBehaviour
{
    [SerializeField] private EnemyFSM FSM;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    //�˹� ����
    Vector2 KnockbackDir;

    //�ǰ� �� ����Ǵ� �ð� �ڷ�ƾ
    Coroutine hitColorCoroutine;


    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void HitWithKnockback(float hitColorTime, Vector2 hittedPos)
    {
        //�˹���� ����
        SetKnockbackDir(hittedPos);
        //�˹� �ֱ�
        rigid.AddForce(KnockbackDir * 5, ForceMode2D.Impulse);

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
