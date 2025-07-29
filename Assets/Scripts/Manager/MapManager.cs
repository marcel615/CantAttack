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

    //이벤트 구독
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

    //카메라의 Min, Max Position을 획득하기 위한 메소드
    void SetCameraMinMaxPosition()
    {
        minPos = tilemap.localBounds.min;
        minPos.y = minPos.y + 1; //타일맵의 타일앵커가 왼쪽 하단이 아니라 정중앙이기 때문에 발생하는 오차 정정
        maxPos = tilemap.localBounds.max;
        maxPos.x = maxPos.x - 1; //오차 정정

        //경계값에다가 해상도에 맞는 가로, 세로 폭을 더하고 빼는 작업으로 적절한 카메라 위치 조정
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;

        minPos.x = minPos.x + cameraWidthHalf;
        minPos.y = minPos.y + cameraHeightHalf;
        maxPos.x = maxPos.x - cameraWidthHalf;
        maxPos.y = maxPos.y - cameraHeightHalf;

    }
}
