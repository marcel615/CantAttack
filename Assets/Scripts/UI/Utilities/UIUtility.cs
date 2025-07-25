using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UIUtility
{
    //EŰ�� EnterŰ�� �Ȱ��� ����ϴ� ��쿡 ���
    public static void TriggerSelectAction()
    {
        //��Ŀ�̵� ������Ʈ �Ҵ�
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        Button selectedButton = selected?.GetComponent<Button>();

        //��Ŀ�̵� ������Ʈ Ŭ��
        if (selectedButton != null)
        {
            selectedButton?.onClick.Invoke();
        }
    }
}
