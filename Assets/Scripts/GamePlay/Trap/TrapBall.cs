using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapBall : ProjectileBase, IParryable
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    public override void SetTarget(GameObject target, GameObject sender)
    {
        base.SetTarget(target, sender);
    }
    protected override void SetDirection()
    {
        base.SetDirection();
    }
    protected override void Shoot()
    {
        base.Shoot();
    }

    //IParryable 인터페이스 구현
    public void OnParried(GameObject parryOrigin)
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerAttack");
        CancelDestroy();
        SetTarget(Sender, parryOrigin);
    }

}
