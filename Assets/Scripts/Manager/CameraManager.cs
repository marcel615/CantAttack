using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static CameraManager Instance;

    //ī�޶� ����ٴ� �÷��̾ü
    Transform target;

    //�⺻ ������
    Vector3 ClampedPosition; //���� ��迡 �ٴٸ��� �ش� ��踦 �������� ī�޶� �����뼱 ����

    //�� ���
    Tilemap tilemap;
    public Vector2 minPosition; //���� ���� �ϴ� ���
    public Vector2 maxPosition; //���� ������ ��� ���

    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        //Ÿ�ϸ� ���� ȹ��
        tilemap = FindObjectOfType<Tilemap>();
        SetCameraMinMaxPosition();

    }

    void LateUpdate()
    {
        if (target == null) return;

        //�� �� ��踦 �����뼱���� �÷��̾� ���� ī�޶� ���� ����
        ClampedPosition = new Vector3(
            Mathf.Clamp(target.transform.position.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(target.transform.position.y, minPosition.y, maxPosition.y),
            gameObject.transform.position.z
            );

        transform.position = ClampedPosition;

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_CameraManager += SetTarget;
    }
    private void OnDisable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_CameraManager -= SetTarget;
    }

    //�÷��̾� ��ġ �޾ƿ��� �޼ҵ�
    public void SetTarget(Transform t)
    {
        target = t;
    }

    //ī�޶��� Min, Max Position�� ȹ���ϱ� ���� �޼ҵ�
    void SetCameraMinMaxPosition()
    {
        minPosition = tilemap.localBounds.min;
        minPosition.y = minPosition.y + 1; //Ÿ�ϸ��� Ÿ�Ͼ�Ŀ�� ���� �ϴ��� �ƴ϶� ���߾��̱� ������ �߻��ϴ� ���� ����
        maxPosition = tilemap.localBounds.max;

        //��谪���ٰ� �ػ󵵿� �´� ����, ���� ���� ���ϰ� ���� �۾����� ������ ī�޶� ��ġ ����
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;

        minPosition.x = minPosition.x + cameraWidthHalf;
        minPosition.y = minPosition.y + cameraHeightHalf;
        maxPosition.x = maxPosition.x - cameraWidthHalf;
        maxPosition.y = maxPosition.y - cameraHeightHalf;

    }
}
