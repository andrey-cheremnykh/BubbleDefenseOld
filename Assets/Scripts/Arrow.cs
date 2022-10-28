using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Arrow : MonoBehaviour
{
    GameObject enemy;
    float damageArrow = 10;
    float timer = 0;
    float speed = 0.5f;
    Vector3 startPos;
    
    

    public void Launch(GameObject en, float damage) 
    {
        enemy = en;
        damageArrow = damage;
        startPos = transform.position;
        transform.parent = null;
    }
    private void Update()
    {
        if (enemy == null) return;
        timer += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, enemy.transform.position, timer);
        transform.LookAt(enemy.transform.position);
        if (timer >= 1)
        {
            enemy.GetComponent<EnemyHealth>().GetDamage(damageArrow);  
            Destroy(gameObject);
        }
    }
}
