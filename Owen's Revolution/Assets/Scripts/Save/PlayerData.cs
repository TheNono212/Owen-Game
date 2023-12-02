using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    [System.Serializable]
    public class PlayerData
    {
        public int health;
        public float[] position;

        public float[] enemyPosition;

        public int enemyHealth;
        public bool enemyIsDead;

        public PlayerData (PlayerStats playerStats, PlayerLocomotion playerLocomotion, EnemyStats enemyStats)
        {
            health = playerStats.currentHealth;

            position = new float[3];
            position[0] = playerLocomotion.transform.position.x;
            position[1] = playerLocomotion.transform.position.y;
            position[2] = playerLocomotion.transform.position.z;

            enemyHealth = enemyStats.currentHealth;
            enemyIsDead = enemyStats.isDead;

            enemyPosition = new float[3];
            enemyPosition[0] = playerLocomotion.transform.position.x;
            enemyPosition[1] = playerLocomotion.transform.position.y;
            enemyPosition[2] = playerLocomotion.transform.position.z;
        }
    }
}