using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        // Outlets
        public GameObject projectilePrefab;
        public Image imageHealthBar;

        // State tracking
        public float firingDelay = 1f;
        public float healthMax = 100f;
        public float health = 100f;

        void Start()
        {
            StartCoroutine("FiringTimer");
        }

        IEnumerator FiringTimer()
        {
            // Wait
            yield return new WaitForSeconds(firingDelay);

            // Spawn
            FireProjectile();

            // Repeat
            StartCoroutine("FiringTimer");
        }
    
        // Methods
        void Update()
        {
            if (health > 0)
            {
                float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
                transform.position = new Vector2(0, yPosition);
            }
            
        }

        void FireProjectile()
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Asteroid>())
            {
                TakeDamage(10f);
            }
        }

        void TakeDamage(float damageAmount)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Die();
            }

            imageHealthBar.fillAmount = health / healthMax;
        }

        void Die()
        {
            StopCoroutine("FiringTimer");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

