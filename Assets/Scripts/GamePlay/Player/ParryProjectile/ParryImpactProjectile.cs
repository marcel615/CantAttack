using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryImpactProjectile : ProjectileBase
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
        if (sender == null) return;

        Direction = dir.normalized;
        Rigidbody2D senderRB = sender.GetComponent<Rigidbody2D>();

        Vector2 impctDir = -Direction;

        //senderRB.AddForce(impctDir * 15f, ForceMode2D.Impulse);
    }

    public override void SetTarget(GameObject target, GameObject sender)
    {
        if (sender == null) return;

        Direction = (target.transform.position - transform.position).normalized;
        Rigidbody2D senderRB = sender.GetComponent<Rigidbody2D>();

        Vector2 impctDir = -Direction;

        //senderRB.AddForce(impctDir * 15f, ForceMode2D.Impulse);
    }

    protected override void Shoot()
    {
        base.Shoot();
    }
}
