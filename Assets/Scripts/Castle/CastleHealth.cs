using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CastleHealth : MonoBehaviour
{
    AttackPoint[] attackPoints;
    float maxHealth = 100;
    float health = 100;
    bool isAlive = true;
    public event Action<float> onDamage;
    public event Action onDestroy;
     // Start is called before the first frame update
    void Start()
    {
        attackPoints = GetComponentsInChildren<AttackPoint>();
        health = maxHealth;
        if (onDamage != null) onDamage(health / 100);
        onDestroy += CastleDestroy;
        SetmaxHP();
    }
    void SetmaxHP()
    {
        int level = PlayerPrefs.GetInt("strength");
        if (level == 1) maxHealth = 200;
        if (level == 2) maxHealth = 400;
        if (level == 3) maxHealth = 800;
        if (level == 4) maxHealth = 1200;
        if (level == 5) maxHealth = 2300;
    }
    public void GetDamage(float damage)
    {
        if (isAlive == false) return;
        health -= damage;
        if (onDamage != null) onDamage(health/100);
        if(health <= Mathf.Epsilon)
        {
            onDestroy();
        }
    }
    public void CastleDestroy()
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
