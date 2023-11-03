using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HO
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        
        public HealthBar healthBar;

        public Animator animator;

        public Collider collider;

        public GameObject enemyHealthBar;

        void Start()
        {
            collider = GetComponent<Collider>();
            enemyHealthBar = GameObject.Find("Enemy/EnemyModel/EnemyHealth");
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void Update()
        {
            healthBar.slider.value = currentHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);
            if(currentHealth > 0)
            {
            animator.Play("Damage_01");
            }
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead_01");
                collider.enabled = false;
                StartCoroutine(WaitBeforeHealthDisappear(4.0f));
            }
        }
        IEnumerator WaitBeforeHealthDisappear(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if(enemyHealthBar != null)
            {
                Destroy(enemyHealthBar);
            }
        }
    }
}