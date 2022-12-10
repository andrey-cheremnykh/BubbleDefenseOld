using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    [SerializeField] int rewardCoin = 5;
    private void Start()
    {
        EnemyHealth enemH = GetComponent<EnemyHealth>();
        enemH.onDeath += RewardForEnemy;
    }
    void RewardForEnemy()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        mm.AddMoney(rewardCoin);
    }
}
