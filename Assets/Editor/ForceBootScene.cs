#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class ForceBootScene
{
    static ForceBootScene()
    {
        // BootScene 경로는 네 프로젝트 기준으로 바꿔줘
        string bootScenePath = "Assets/Scenes/BootScene.unity";

        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(bootScenePath);
    }
}

#endif