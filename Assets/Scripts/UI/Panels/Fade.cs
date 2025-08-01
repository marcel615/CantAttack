using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Image image;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Fade;

    //Fade ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //Fade�ð�
    float fadeTime;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (image == null) image = transform.Find("Image")?.GetComponent<Image>();
    }

    //��𼱰� Fade �г��� ������ ��
    public void FadeOpen(float fadeT, FadeDirection fadeDirection)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        fadeTime = fadeT;
        if(fadeDirection == FadeDirection.FadeOut)
        {
            //���̵� �ƿ�
            StartCoroutine(StartFade(0f, 1f, fadeTime, FadeDirection.FadeOut));
        }
        else
        {
            //���̵� ��
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

        // ������ ���� ��Ȯ�� ����
        color.a = endAlpha;
        image.color = color;

        //���̵����� ���
        if(fadeDir == FadeDirection.FadeIn)
        {
            FadeClose();
        }
    } 

}
