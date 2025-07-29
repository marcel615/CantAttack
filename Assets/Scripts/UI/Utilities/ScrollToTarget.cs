using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollToTarget : MonoBehaviour
{
    //�� ������Ʈ
    [SerializeField] private ScrollRect scrollRect;
    //�ڽ� ������Ʈ
    [SerializeField] private RectTransform viewPort;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
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

        // �Ʒ��� ���
        if (itemCorners[0].y < viewportCorners[0].y)
        {
            scrollRect.verticalNormalizedPosition -= 0.001f;
        }
        // ���� ���
        else if (itemCorners[1].y > viewportCorners[1].y)
        {
            scrollRect.verticalNormalizedPosition += 0.001f;
        }

        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

}
