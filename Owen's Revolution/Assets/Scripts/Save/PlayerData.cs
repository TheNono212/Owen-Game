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

        public PlayerData (PlayerStats playerStats, PlayerLocomotion playerLocomotion)
        {
            health = playerStats.currentHealth;

            position = new float[3];
            position[0] = playerLocomotion.transform.position.x;
            position[1] = playerLocomotion.transform.position.y;
            position[2] = playerLocomotion.transform.position.z;
        }
    }
}