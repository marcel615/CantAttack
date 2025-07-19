using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //�ڽ� ������Ʈ (UI ������Ʈ)
    //Canvas
    public GameObject UICanvas;
    //HPPanel
    public GameObject HPPanel;
    public GameObject Portrait;
    public GameObject HPContainer;
    //??Panel



    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (UICanvas == null) UICanvas = transform.Find("UICanvas")?.gameObject;
        if (HPPanel == null) HPPanel = transform.Find("UICanvas/HPPanel")?.gameObject;
        if (Portrait == null) Portrait = transform.Find("UICanvas/HPPanel/Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("UICanvas/HPPanel/HPContainer")?.gameObject;


    }

}
