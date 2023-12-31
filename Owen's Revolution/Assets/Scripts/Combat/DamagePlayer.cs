using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        public GameObject obj;

        public string[] damageType = {"SwordDamage", "FireDamage", "TreeDamage"};

        private void Start() {
            if(obj != null)
            {
                Debug.Log("HELLO158");
            }
            obj = gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            Fire fire = other.GetComponent<Fire>();

            if(playerStats != null)
            {
                if(obj.tag == "Enemy")
                {
                    playerStats.TakeDamage(damage, damageType[0]);
                    Debug.Log(damageType[0]);
                }
                if(obj.tag == "Fire")
                {
                    playerStats.TakeDamage(damage, damageType[1]);
                    Debug.Log(damageType[1]);
                    fire.FireDamage(true);
                }
                if(obj.tag == "Tree")
                {
                    playerStats.TakeDamage(damage, damageType[2]);
                    Debug.Log(damageType[2]);
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            Fire fire = other.GetComponent<Fire>();
            if(obj.tag == "Fire")
            {
                fire.FireDamage(false);
            }
        }
    }
}