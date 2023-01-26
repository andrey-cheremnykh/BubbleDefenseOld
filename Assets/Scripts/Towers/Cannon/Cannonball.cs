using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] float damage = 20;
    [SerializeField] float damageRadius = 10;
    Vector3 dest;
    Vector3 startPos;
    bool isLaunched;
    float timer = 0;
    public void LaunchCannonball(Vector3 destination)
    {
        timer = 0;
        startPos = transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 30, 0);
        dest = destination;
        isLaunched = true;
    }
    private void Update()
    {
        if (isLaunched) return;
        timer += Time.deltaTime;
        Vector3 newPos = Vector3.Lerp(startPos,dest,timer );
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        if(timer>= 1)
        {
            Explode();
        }
    }
    void Explode()
    {
        EnemyHealth[] enems = FindObjectsOfType<EnemyHealth>();
        for (int i = 0; i < enems.Length; i++)
        {
            float distance = Vector3.Distance(transform.position,enems[i].transform.position);
            if (distance > damageRadius ) continue;
            if (enems[i].isAlive == false) continue;
            enems[i].GetDamage(damage);
        }
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        explosion.transform.parent = null;
        explosion.Play();   
        Destroy(gameObject);
    }
}
