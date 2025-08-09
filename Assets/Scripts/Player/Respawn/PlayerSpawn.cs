using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    Player player;
    Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnSavedSceneLoaded += OnSavedSceneLoaded;
        //�÷��̾ �������ؼ� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnRespawnSceneLoaded += OnRespawnSceneLoaded;
    }
    private void OnDisable()
    {
        //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnSavedSceneLoaded -= OnSavedSceneLoaded;
        //�÷��̾ �������ؼ� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnRespawnSceneLoaded -= OnRespawnSceneLoaded;
    }
    //���̺� �ε� �� ������ ���� �ʱ�ȭ
    void OnSavedSceneLoaded()
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = player.savePosition;

        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.InvokePlayerSpawned_HPUIManager(player.status.MaxHP, player.status.CurrentHP);

        //Context ������Ʈ
        InputEvents.InvokeContextUpdate(InputContext.Player);
    }
    //�÷��̾ �������ؼ� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
    void OnRespawnSceneLoaded()
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = player.savePosition;

        //ü�� Ǯ�� ä���
        player.status.CurrentHP = player.status.MaxHP;
        //��Ʈ�ڽ� �ٽ� Ű��
        player.playerHitBoxCollider.enabled = true;
        //�ִϸ��̼� �����ϰ�
        animator.SetBool("isDead", false);

        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.InvokePlayerSpawned_HPUIManager(player.status.MaxHP, player.status.CurrentHP);

        //Context ������Ʈ
        InputEvents.InvokeContextUpdate(InputContext.Player);

    }

}
