using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    //�� ������Ʈ
    Animator animator;

    //�ڽ� ������Ʈ
    public GameObject beforeInteractText;
    public GameObject afterInteractText;

    //���̺�����Ʈ ������
    public SavePointSO savePointSO;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowInteractableMessage()
    {
        beforeInteractText.SetActive(true);
    }
    public void HideInteractableMessage()
    {
        beforeInteractText.SetActive(false);
        afterInteractText.SetActive(false);
    }
    public void DoSave(GameObject interactTarget)
    {
        animator.SetTrigger("isInteract");
        HideInteractableMessage();

        //SavePoint���� �����ϱ� ������ ������ �̺�Ʈ
        SystemEvents.InvokeSavePointNotice(transform);

        //���̺� ��û �̺�Ʈ ����
        SystemEvents.InvokeSaveRequest();
    }
    //�̺�Ʈ ����
    private void OnEnable()
    {
        //���̺� �����ٴ� �̺�Ʈ
        SystemEvents.OnSaveEnd += OnSaveFinished;
    }
    private void OnDisable()
    {
        //���̺� �����ٴ� �̺�Ʈ
        SystemEvents.OnSaveEnd -= OnSaveFinished;
    }
    void OnSaveFinished()
    {
        afterInteractText.SetActive(true);
    }

}
