using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollToTarget : MonoBehaviour
{
    //내 컴포넌트
    [SerializeField] private ScrollRect scrollRect;
    //자식 오브젝트
    [SerializeField] private RectTransform viewPort;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (viewPort == null) viewPort = transform.Find("Viewport")?.GetComponent<RectTransform>();

    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            RectTransform selected = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            if (selected != null)
            {
                ScrollToKeepInView(selected);
            }
        }
    }
    void ScrollToKeepInView(RectTransform selected)
    {
        Vector3[] viewportCorners = new Vector3[4];
        viewPort.GetWorldCorners(viewportCorners);

        Vector3[] itemCorners = new Vector3[4];
        selected.GetWorldCorners(itemCorners);

        // 아래로 벗어남
        if (itemCorners[0].y < viewportCorners[0].y)
        {
            scrollRect.verticalNormalizedPosition -= 0.001f;
        }
        // 위로 벗어남
        else if (itemCorners[1].y > viewportCorners[1].y)
        {
            scrollRect.verticalNormalizedPosition += 0.001f;
        }

        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

}
