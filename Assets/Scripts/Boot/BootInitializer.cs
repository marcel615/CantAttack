using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootInitializer : MonoBehaviour
{
    private void Start()
    {
        // 매니저 초기화
        //GameManager.Instance.Init();
        UIManager.Instance.Init();
        //SaveManager.Instance.Init();
        //InputManager.Instance.Init();
        //MessageManager.Instance.Init();
        //Player.Instance.Init();

        // 메인메뉴 씬 로드
        SceneManager.LoadScene("MainMenu");
    }
}
