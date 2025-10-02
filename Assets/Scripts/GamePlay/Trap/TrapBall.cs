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
    public override void SetDirection(Vector2 dir, GameObject sender)
    {
        base.SetDirection(dir, sender);
    }
    protected override void Shoot()
    {
        base.Shoot();
    }

    //IParryable 인터페이스 구현
    public void OnParried(GameObject parryOrigin)
    {
        Destroy(gameObject);
    }

}
