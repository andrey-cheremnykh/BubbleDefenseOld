using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    AttackPoint[] attackPoints;
    float health = 100;
    bool isAlive = true;
     // Start is called before the first frame update
    void Start()
    {
        attackPoints = GetComponentsInChildren<AttackPoint>();
    }
    public void GetDamage(float damage)
    {
        health -= damage;
        if(health <= Mathf.Epsilon)
        {
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
