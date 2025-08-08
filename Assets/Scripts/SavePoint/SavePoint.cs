using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    //내 오브젝트
    Animator animator;

    //자식 오브젝트
    public GameObject beforeInteractText;
    public GameObject afterInteractText;

    //세이브포인트 데이터
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

        //SavePoint에서 저장하기 직전에 보내는 이벤트
        SystemEvents.InvokeSavePointNotice(transform);

        //세이브 요청 이벤트 발행
        SystemEvents.InvokeSaveRequest();
    }
    //이벤트 구독
    private void OnEnable()
    {
        //세이브 끝났다는 이벤트
        SystemEvents.OnSaveEnd += OnSaveFinished;
    }
    private void OnDisable()
    {
        //세이브 끝났다는 이벤트
        SystemEvents.OnSaveEnd -= OnSaveFinished;
    }
    void OnSaveFinished()
    {
        afterInteractText.SetActive(true);
    }

}
