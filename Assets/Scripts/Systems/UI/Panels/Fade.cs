using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Image image;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Fade;

    //Fade 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //Fade시간
    float fadeTime;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (image == null) image = transform.Find("Image")?.GetComponent<Image>();
    }

    //어디선가 Fade 패널을 열었을 때
    public void FadeOpen(float fadeT, FadeDirection fadeDirection)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        fadeTime = fadeT;
        if(fadeDirection == FadeDirection.FadeOut)
        {
            //페이드 아웃
            StartCoroutine(StartFade(0f, 1f, fadeTime, FadeDirection.FadeOut));
        }
        else
        {
            //페이드 인
            StartCoroutine(StartFade(1f, 0f, fadeTime, FadeDirection.FadeIn));
        }

    }
    public void FadeClose()
    {
        if (currentPanel != null)
        {
            UIPanelController.Close(ref currentPanel, gameObject);
        }
    }
     
    private IEnumerator StartFade(float startAlpha, float endAlpha, float fadeTime, FadeDirection fadeDir)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = startAlpha;
        image.color = color;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            image.color = color;
            yield return null;
        }

        // 마지막 알파 정확히 지정
        color.a = endAlpha;
        image.color = color;

        //페이드인일 경우
        if(fadeDir == FadeDirection.FadeIn)
        {
            FadeClose();
        }
    } 

}
