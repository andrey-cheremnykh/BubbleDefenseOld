using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 1;  
    List<Waypoint> enemyPath;
    EnemyHealth health;

    float timer = 0;
    int indexWaypoint = 0;

    Vector3 start, end, offset;
    bool isOnCastle;
    public float GetPassedDist()
    {
        float dist = indexWaypoint + timer;
        return dist;
    }

    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPath == null) return;
        if (health.isAlive == false) return;

        timer += Time.deltaTime * enemySpeed;
        transform.position = Vector3.Lerp(start, end, timer);

        if(timer > 1)
        {
            SwitchWaypoint();
        }

    }
    
    void SwitchWaypoint()
    {
        if (isOnCastle == true) return;
        indexWaypoint++;
        if (indexWaypoint + 1 >= enemyPath.Count)
        {
            start = enemyPath[enemyPath.Count - 1].transform.position + offset;
            StartCoroutine(GoToCastle());
            return;

        }
        
        start = enemyPath[indexWaypoint].transform.position + offset;
        end = enemyPath[indexWaypoint + 1].transform.position + offset;

        LookForward();

        timer = 0;
    }
    IEnumerator  GoToCastle()
    {
        isOnCastle = true;
        CastleHealth castle = FindObjectOfType<CastleHealth>();
        AttackPoint freePoint = castle.GetFreePoint();
        while(castle.GetFreePoint() == null)
        {
            yield return new WaitForSeconds(1);
            GetComponent<Animator>().SetTrigger("attack");
        }
        end = castle.GetFreePoint().transform.position;
        if(castle.GetAttackPoint(gameObject).y == -1)
        {
            end = start; 
            yield return new WaitForSeconds(1);
            StartCoroutine(GoToCastle());
        }
        else
        {
        timer = 0;
        freePoint.enemyOnPoint = gameObject;
        yield return new WaitForSeconds(1/enemySpeed);
        enemyPath =null;

        }
        //start attack anim

    }
    void LookForward()
    {
        Waypoint look = enemyPath[indexWaypoint + 2];
        transform.LookAt(look.transform);
        float yDegree = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yDegree, 0);
    }

    public void Go(List<Waypoint> path)
    { 
        offset = new Vector3(0, 10, 0);
        enemyPath = path;
        transform.position = start;
        start = path[0].transform.position + offset;
        end = path[1].transform.position + offset;
        timer = 0;
        transform.rotation = Quaternion.Euler(180,0,0);
        LookForward();
    }

    public IEnumerator SlowEnemy(float slowness, float duration)
    {
        float percent = 1 - slowness;
        enemySpeed *= percent;
        yield return new WaitForSeconds(duration);
        enemySpeed /= percent;
    }
}
