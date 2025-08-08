using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveHandler : MonoBehaviour, ISaveLoadable
{
    //���̺굥���� ���� �� �ҷ����� ����
    [SerializeField] Player player;
    [SerializeField] PlayerStatus playerStatus;

    private void Awake()
    {
        //�� ������Ʈ ����
        player = GetComponent<Player>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    //ISaveLoadable �������̽� ����
    public string DicKey => "PlayerSaveHandler";

    public object Save()
    {
        return new PlayerSaveData
        {
            MaxHP = playerStatus.MaxHP,
            CurrentHP = playerStatus.CurrentHP,
            position = player.savePosition,

        };
    }
    public void Load(object saveData)
    {
        PlayerSaveData playerSaveData = saveData as PlayerSaveData;
        if (playerSaveData != null)
        {
            playerStatus.MaxHP = playerSaveData.MaxHP;
            playerStatus.CurrentHP = playerSaveData.CurrentHP;
            player.savePosition = playerSaveData.position;
        }

    }
    //�̺�Ʈ ����

    private void OnEnable()
    {
        SystemEvents.OnSaveDicKeyRequest += SaveDicKey;

        //SystemMenu �гο��� ���θ޴��� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ (���̺��ϴ� ���)
        //SceneTransitionEvents.OnSystemMenuToMainMenu += SavePlayerPos;

        //SavePoint���� �����ϱ� ������ ������ �̺�Ʈ
        SystemEvents.OnSavePointNotice += SavePlayerPos;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveDicKeyRequest -= SaveDicKey;

        //SystemMenu �гο��� ���θ޴��� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ (���̺��ϴ� ���)
        //SceneTransitionEvents.OnSystemMenuToMainMenu -= SavePlayerPos;

        //SavePoint���� �����ϱ� ������ ������ �̺�Ʈ
        SystemEvents.OnSavePointNotice -= SavePlayerPos;
    }
    //SaveManager���� ��ųʸ� �����ϴ� ����
    void SaveDicKey(SaveManager saveManager)
    {
        saveManager.GetDicKey(this);
    }
    void SavePlayerPos(Transform saveTransform)
    {
        player.savePosition = saveTransform.position;
    }

}
