using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //ī�޶� ����ٴ� �÷��̾ü
    Transform target;
    Tilemap tilemap;

    //�⺻ ������
    Vector3 ClampedPosition; //���� ��迡 �ٴٸ��� �ش� ��踦 �������� ī�޶� �����뼱 ����

    //�� ���
    public Vector2 minPosition; //���� ���� �ϴ� ���
    public Vector2 maxPosition; //���� ������ ��� ���

    private void Awake()
    {

    }
    private void Start()
    {
        //�� �ʸ����� ��踦 Ȯ���ϱ� ���� �۾�
        tilemap = FindObjectOfType<Tilemap>();
        minPosition = tilemap.localBounds.min;
        minPosition.y = minPosition.y + 1; //Ÿ�ϸ��� Ÿ�Ͼ�Ŀ�� ���� �ϴ��� �ƴ϶� ���߾��̱� ������ �߻��ϴ� ���� ����
        maxPosition = tilemap.localBounds.max;
        
        //Debug.Log(minPosition + "" + maxPosition);

        //��谪���ٰ� �ػ󵵿� �´� ����, ���� ���� ���ϰ� ���� �۾����� ������ ī�޶� ��ġ ����
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;
        
        minPosition.x = minPosition.x + cameraWidthHalf;
        minPosition.y = minPosition.y + cameraHeightHalf;
        maxPosition.x = maxPosition.x - cameraWidthHalf;
        maxPosition.y = maxPosition.y - cameraHeightHalf;
        
        //Debug.Log(minPosition + "" + maxPosition);
        

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

    //�÷��̾� ��ġ �޾ƿ��� �޼ҵ�
    public void SetTarget(Transform t)
    {
        target = t;
    }
}
