using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class DamageCollider : MonoBehaviour
    {
        public BoxCollider damageCollider;

        public DamagePlayer damagePlayer;

        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<BoxCollider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }
        
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision) 
        {
            if(collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage, damagePlayer.damageType[0]);
                }
            }
            if(collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if(enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }
}