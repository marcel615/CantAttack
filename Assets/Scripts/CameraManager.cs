using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //CameraManager 오브젝트 중복체크를 위한 private 인스턴스 생성
    private static CameraManager instance;

    //카메라가 따라다닐 플레이어객체
    Transform target;

    //기본 변수들
    Vector3 ClampedPosition; //맵의 경계에 다다르면 해당 경계를 기준으로 카메라 마지노선 설정

    //맵 경계
    Tilemap tilemap;
    public Vector2 minPosition; //맵의 왼쪽 하단 경계
    public Vector2 maxPosition; //맵의 오른쪽 상단 경계

    private void Awake()
    {
        // CameraManager 인스턴스 처음 할당할 때
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //기존 CameraManager 인스턴스가 존재할 때
        {
            Destroy(gameObject);    //파괴
        }

    }
    private void Start()
    {
        //타일맵 정보 획득
        tilemap = FindObjectOfType<Tilemap>();
        SetCameraMinMaxPosition();

    }

    void LateUpdate()
    {
        if (target == null) return;

        //맵 끝 경계를 마지노선으로 플레이어 기준 카메라 설정 과정
        ClampedPosition = new Vector3(
            Mathf.Clamp(target.transform.position.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(target.transform.position.y, minPosition.y, maxPosition.y),
            gameObject.transform.position.z
            );

        transform.position = ClampedPosition;

    }

    //이벤트 구독
    private void OnEnable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned += SetTarget;
    }
    private void OnDisable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned -= SetTarget;
    }

    //플레이어 위치 받아오는 메소드
    public void SetTarget(Transform t)
    {
        target = t;
    }

    //카메라의 Min, Max Position을 획득하기 위한 메소드
    void SetCameraMinMaxPosition()
    {
        minPosition = tilemap.localBounds.min;
        minPosition.y = minPosition.y + 1; //타일맵의 타일앵커가 왼쪽 하단이 아니라 정중앙이기 때문에 발생하는 오차 정정
        maxPosition = tilemap.localBounds.max;

        //경계값에다가 해상도에 맞는 가로, 세로 폭을 더하고 빼는 작업으로 적절한 카메라 위치 조정
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;

        minPosition.x = minPosition.x + cameraWidthHalf;
        minPosition.y = minPosition.y + cameraHeightHalf;
        maxPosition.x = maxPosition.x - cameraWidthHalf;
        maxPosition.y = maxPosition.y - cameraHeightHalf;

    }
}
