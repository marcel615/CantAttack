using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactionHandler : MonoBehaviour
{
    public void HitWithKnockback(Vector2 hittedPos)
    {
        Debug.Log("HitWithKnockback");
    }
    public void HitWithNoKnockback()
    {
        Debug.Log("HitWithNoKnockback");
    }

}
