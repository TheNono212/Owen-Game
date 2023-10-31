using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HO
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        
        public HealthBar healthBar;

        public AnimationHandler animatorHandler;

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage_01", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead_01", true);
                //HANDLE PLAYER DEATH;
                HandlePlayerDeath();
            }
        }
        public void HandlePlayerDeath()
        {
            StartCoroutine(Death(5.0f));
        }

        public IEnumerator Death(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
        }
    }
}