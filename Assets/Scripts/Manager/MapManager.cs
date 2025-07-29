using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    Tilemap tilemap;
    Vector3 minPos;
    Vector3 maxPos;

    //�̺�Ʈ ����
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        tilemap = FindObjectOfType<Tilemap>();
        if (tilemap != null)
        {
            SetCameraMinMaxPosition();
            SystemEvents.InvokeGetMapPos(minPos, maxPos);
        }

    }

    //ī�޶��� Min, Max Position�� ȹ���ϱ� ���� �޼ҵ�
    void SetCameraMinMaxPosition()
    {
        minPos = tilemap.localBounds.min;
        minPos.y = minPos.y + 1; //Ÿ�ϸ��� Ÿ�Ͼ�Ŀ�� ���� �ϴ��� �ƴ϶� ���߾��̱� ������ �߻��ϴ� ���� ����
        maxPos = tilemap.localBounds.max;
        maxPos.x = maxPos.x - 1; //���� ����

        //��谪���ٰ� �ػ󵵿� �´� ����, ���� ���� ���ϰ� ���� �۾����� ������ ī�޶� ��ġ ����
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;

        minPos.x = minPos.x + cameraWidthHalf;
        minPos.y = minPos.y + cameraHeightHalf;
        maxPos.x = maxPos.x - cameraWidthHalf;
        maxPos.y = maxPos.y - cameraHeightHalf;

    }
}
