using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrapCrusherBlock : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject damageCollider;
    [SerializeField] private GameObject groundCollider;
    [SerializeField] private GameObject dropPosition;

    //�⺻ ����
    public int damage = 1;

    //�����̴� ���� ���� ����
    float waitTime = 1f;        //�� �ൿ�� ��ġ�� ��ٸ��� �ð�
    Vector3 originPos;          //�������� �� ���� ��ġ
    Vector2 dropPos;            //������ �� ��ġ
    Vector2 targetPos;          //�� ���¿� ���߾� ������ ��ġ
    float ResetSpeed = 3f;      //�ٽ� ���� ��ġ�� ������ ���� ���ǵ�
    float dropSpeed = 15f;      //�������� ���ǵ�
    float targetSpeed;          //�� ���¿� ���߾� ������ ���ǵ�

    //�÷���
    bool isResetAndWait;        //���µǰ� �������⸦ ��ٸ��� �ð����� �÷���
    bool isDropedAndWait;       //�������� ���µǱ⸦ ��ٸ��� �ð����� �÷���
    bool isStartMove;           //�� ��ٸ��� �����̴� �ð����� �÷���


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (damageCollider == null) damageCollider = transform.Find("DamageArea")?.gameObject;
        if (dropPosition == null) dropPosition = transform.Find("DropPosition")?.gameObject;

        //������ Position �ʱ�ȭ
        originPos = transform.position;
        dropPos = dropPosition.transform.position;
    }
    private void FixedUpdate()
    {
        //��ٷȴٰ� �������� ����
        if(!isResetAndWait && Vector3.Distance(transform.position, originPos) < 0.01f)
        {
            StartCoroutine(WaitAndDrop(waitTime));
        }
        //��ٷȴٰ� ���� ��ġ�� ����
        if(!isDropedAndWait && Vector3.Distance(transform.position, dropPos) < 0.01f)
        {
            StartCoroutine(WaitAndReset(waitTime));
        }
        if (isStartMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, targetSpeed * Time.fixedDeltaTime);
        }
    }
    private IEnumerator WaitAndDrop(float waitTime)
    {
        //�÷��� ����
        isResetAndWait = true;
        isDropedAndWait = false;
        //�����̱� �÷��� ����
        isStartMove = false;

        //������ �ݶ��̴� ��� ����
        damageCollider.GetComponent<BoxCollider2D>().enabled = false;

        //waitTime��ŭ ��ٷȴٰ�
        yield return new WaitForSeconds(waitTime);

        //������ �ݶ��̴� �ٽ� Ű��
        damageCollider.GetComponent<BoxCollider2D>().enabled = true;

        //��ǥ����, ���ǵ� ���� �� �����̱� �÷��� ����
        targetPos = dropPos;
        targetSpeed = dropSpeed;
        isStartMove = true;
    }
    private IEnumerator WaitAndReset(float waitTime)
    {
        //�÷��� ����
        isDropedAndWait = true;
        isResetAndWait = false;
        //�����̱� �÷��� ����
        isStartMove = false;

        //������ �ݶ��̴� ��� ����
        damageCollider.GetComponent<BoxCollider2D>().enabled = false;

        //waitTime��ŭ ��ٷȴٰ�
        yield return new WaitForSeconds(waitTime);

        //��ǥ����, ���ǵ� ���� �� �����̱� �÷��� ����
        targetPos = originPos;
        targetSpeed = ResetSpeed;
        isStartMove = true;
    }
}
