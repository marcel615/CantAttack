using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //자식 오브젝트 (UI 오브젝트)
    //Canvas
    public GameObject UICanvas;
    //HPPanel
    public GameObject HPPanel;
    public GameObject Portrait;
    public GameObject HPContainer;
    //??Panel



    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (UICanvas == null) UICanvas = transform.Find("UICanvas")?.gameObject;
        if (HPPanel == null) HPPanel = transform.Find("UICanvas/HPPanel")?.gameObject;
        if (Portrait == null) Portrait = transform.Find("UICanvas/HPPanel/Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("UICanvas/HPPanel/HPContainer")?.gameObject;


    }

}
