using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryReflectProjectile : ProjectileBase
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
        base.SetDirection(dir, sender);
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
