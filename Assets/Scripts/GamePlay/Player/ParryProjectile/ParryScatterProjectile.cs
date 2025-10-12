using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScatterProjectile : ProjectileBase
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void SetDirection(Vector2 dir, GameObject sender)
    {
        Sender = sender;
        Direction = dir.normalized;

        // 회전 적용
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Shoot();
    }
    public override void SetTarget(GameObject target, GameObject sender)
    {
        base.SetTarget(target, sender);
    }
    protected override void Shoot()
    {
        base.Shoot();
    }
}
