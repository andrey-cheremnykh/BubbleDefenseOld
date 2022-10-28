using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 10;
    public void Hit()
    {
        CastleHealth castle = FindObjectOfType<CastleHealth>();
        castle.GetDamage(damage);
    }
}
