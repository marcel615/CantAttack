#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class ForceBootScene
{
    static ForceBootScene()
    {
        // BootScene ��δ� �� ������Ʈ �������� �ٲ���
        string bootScenePath = "Assets/Scenes/BootScene.unity";

        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(bootScenePath);
    }
}

#endif