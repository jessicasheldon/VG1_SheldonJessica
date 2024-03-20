using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        // Outlets
        public GameObject projectilePrefab;

        // State tracking
        public float firingDelay = 1f;

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
            float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
            transform.position = new Vector2(0, yPosition);
        }

        void FireProjectile()
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}

