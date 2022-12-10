using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CastleHealth : MonoBehaviour
{
    AttackPoint[] attackPoints;
    float health = 100;
    bool isAlive = true;
    public event Action<float> onDamage;
     // Start is called before the first frame update
    void Start()
    {
        attackPoints = GetComponentsInChildren<AttackPoint>();
        if (onDamage != null) onDamage(health / 100);
    }
    public void GetDamage(float damage)
    {
        if (isAlive == false) return;
        health -= damage;
        if (onDamage != null) onDamage(health/100);
        if(health <= Mathf.Epsilon)
        {
            isAlive = false;
            CasleDestroy();
        }
    }
    public void CasleDestroy()
    {
        isAlive = false;
    }

    public AttackPoint GetFreePoint()
    {
        for (int i = 0; i < attackPoints.Length; i++)
        {
            if (attackPoints[i].enemyOnPoint == null) return attackPoints[i];
        }
        return null;
    }
    public Vector3 GetAttackPoint(GameObject enemy)
    {
        Vector3 point = new Vector3(-1, -1, -1);
        for (int i = 0; i < attackPoints.Length; i++)
        {
            if(attackPoints[i].enemyOnPoint == null)
            {
                point = attackPoints[i].transform.position;
                attackPoints[i].enemyOnPoint = enemy;
                break;
            }
        }
        return point;
    }
}
