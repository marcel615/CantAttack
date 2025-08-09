using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDead : MonoBehaviour
{
    //내 컴포넌트
    Player player;
    Rigidbody2D rigid;
    Animator animator;

    //Dead 연출 관련
    [SerializeField] private GameObject bloodEffectPrefab;
    float deadSequenceTime = 2f;


    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    //이벤트 구독
    private void OnEnable()
    {
        //Player가 사망했을 때
        PlayerEvents.OnPlayerDead += OnPlayerDead;        
    }
    private void OnDisable()
    {
        //Player가 사망했을 때
        PlayerEvents.OnPlayerDead -= OnPlayerDead;
    }
    void OnPlayerDead()
    {
        StartCoroutine(DeadSequence());
    }
    IEnumerator DeadSequence()
    {
        // 사망 연출
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.PlayerDead);
        //히트박스 끄고
        player.playerHitBoxCollider.enabled = false;
        //움직임 멈추고
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        //피 이펙트 실행하고
        GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        //애니메이션 설정하고
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(deadSequenceTime);

        //씬 전환 실시
        SceneTransitionEvents.InvokeDeadToRespawn();
    }
}
