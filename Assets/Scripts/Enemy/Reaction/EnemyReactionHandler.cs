using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactionHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    //넉백 방향
    Vector2 KnockbackDir;
    //피격 색 변경 시간
    float hitColorTime = 0.3f;

    //피격 색 변경되는 시간 코루틴
    Coroutine hitColorCoroutine;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void HitWithKnockback(Vector2 hittedPos)
    {
        //넉백방향 설정
        SetKnockbackDir(hittedPos);
        //넉백 주기
        rigid.AddForce(KnockbackDir * 15, ForceMode2D.Impulse);

        // 투명도 변화
        CancelhitColor(); //만약 연속으로 피격될 경우 코루틴 겹치는거 방지
        hitColorCoroutine = StartCoroutine(ChangeColor(hitColorTime));
    }
    public void HitWithNoKnockback()
    {
        // 투명도 변화
        CancelhitColor(); //만약 연속으로 피격될 경우 코루틴 겹치는거 방지
        StartCoroutine(ChangeColor(hitColorTime));
    }
    void SetKnockbackDir(Vector2 hittedPos)
    {
        KnockbackDir = ((Vector2)transform.position - hittedPos).normalized;
    }
    IEnumerator ChangeColor(float hitColorTime)
    {
        //피격 시 반투명하게 됨
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // hitColorTime만큼 기다렸다가
        yield return new WaitForSeconds(hitColorTime);

        //피격 시 반투명하게 되었던거 되돌리기
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
    void CancelhitColor()
    {
        // 기존 타이머 취소
        if (hitColorCoroutine != null)
        {
            StopCoroutine(hitColorCoroutine);
        }
    }
}
