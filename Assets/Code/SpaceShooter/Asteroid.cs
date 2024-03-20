using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {

        // Outlet
        Rigidbody2D _rb;

        // State tracking
        float randomSpeed;

        // Methods
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(0.5f, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            _rb.velocity = Vector2.left * randomSpeed;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}

