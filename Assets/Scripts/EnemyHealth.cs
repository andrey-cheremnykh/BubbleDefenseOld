using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 5;
    public bool isAlive = true;
    public void GetDamage(float damage)
    {
        if (isAlive == false) return;
        enemyHealth -= damage;
        if(enemyHealth < 0)
        {
            isAlive = false;
            Death();
        }
    }

    void Death()
    {
        GetComponent<Animator>().SetTrigger("die");
        Destroy(gameObject, 2.5f);
    }
}
