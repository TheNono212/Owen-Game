using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HO
{
    public class PlayerStats : CharacterStats
    {

        
        public HealthBar healthBar;

        public AnimationHandler animatorHandler;
        public Fire fire;
        public DamagePlayer damagePlayer;


        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            fire = GetComponent<Fire>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        void Update()
        {
            healthBar.SetCurrentHealth(currentHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage, string damageType)
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