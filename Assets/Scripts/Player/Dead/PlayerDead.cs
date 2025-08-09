using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDead : MonoBehaviour
{
    //�� ������Ʈ
    Player player;
    Rigidbody2D rigid;
    Animator animator;

    //Dead ���� ����
    [SerializeField] private GameObject bloodEffectPrefab;
    float deadSequenceTime = 2f;


    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    //�̺�Ʈ ����
    private void OnEnable()
    {
        //Player�� ������� ��
        PlayerEvents.OnPlayerDead += OnPlayerDead;        
    }
    private void OnDisable()
    {
        //Player�� ������� ��
        PlayerEvents.OnPlayerDead -= OnPlayerDead;
    }
    void OnPlayerDead()
    {
        StartCoroutine(DeadSequence());
    }
    IEnumerator DeadSequence()
    {
        // ��� ����
        //Context ���� �̺�Ʈ
        InputEvents.InvokeContextUpdate(InputContext.PlayerDead);
        //��Ʈ�ڽ� ����
        player.playerHitBoxCollider.enabled = false;
        //������ ���߰�
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        //�� ����Ʈ �����ϰ�
        GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        //�ִϸ��̼� �����ϰ�
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(deadSequenceTime);

        //�� ��ȯ �ǽ�
        SceneTransitionEvents.InvokeDeadToRespawn();
    }
}
