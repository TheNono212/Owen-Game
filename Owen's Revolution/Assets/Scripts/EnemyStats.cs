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

        public Collider solidCollider;
        public Collider triggerCollider;

        public GameObject enemyHealthBar;
        public GameObject FloatingTextPrefab;

        public int publicDamage;

        public bool isDead;

        void Start()
        {
            solidCollider = GetComponent<Collider>();
            //triggerCollider = GetComponentInChildren<Collider>();
            // The triggerCollider doesn't work this way, it's assigned as the same collider as solidCollider
            // SO IT DOESN'T WORK HAHAHA (putting it in the inspector works so going to let it as it is)
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

            if(!isDead)
            {
                GetComponent<Collider>().enabled = true;
                triggerCollider.enabled = true;
                enemyHealthBar.SetActive(true);
            }
        }

        public void TakeDamage(int damage)
        {
            publicDamage = damage;
            currentHealth = currentHealth - damage;

            if(FloatingTextPrefab != null && currentHealth > 0)
            {
                ShowFloatingText();
            }

            healthBar.SetCurrentHealth(currentHealth);
            if(currentHealth > 0)
            {
            animator.Play("Damage_01");
            }
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                isDead=true;
                animator.Play("Dead_01");
                GetComponent<Collider>().enabled = false;
                triggerCollider.enabled = false;
                StartCoroutine(WaitBeforeHealthDisappear(4.0f));
            }
        }

        public void ShowFloatingText()
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = publicDamage.ToString();
        }

        IEnumerator WaitBeforeHealthDisappear(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if(enemyHealthBar != null)
            {
                enemyHealthBar.SetActive(false);
            }
        }
    }
}