using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootInitializer : MonoBehaviour
{
    private void Start()
    {
        // �Ŵ��� �ʱ�ȭ
        //GameManager.Instance.Init();
        UIManager.Instance.Init();
        //SaveManager.Instance.Init();
        //InputManager.Instance.Init();
        //MessageManager.Instance.Init();
        //Player.Instance.Init();

        // ���θ޴� �� �ε�
        SceneManager.LoadScene("MainMenu");
    }
}
