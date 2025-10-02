using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UIUtility
{
    //E키를 Enter키와 똑같이 사용하는 경우에 사용
    public static void TriggerSelectAction()
    {
        //포커싱된 오브젝트 할당
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        Button selectedButton = selected?.GetComponent<Button>();

        //포커싱된 오브젝트 클릭
        if (selectedButton != null)
        {
            selectedButton?.onClick.Invoke();
        }
    }
}
