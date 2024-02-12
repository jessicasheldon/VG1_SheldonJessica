using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q2
{
    public class RotatingShipController : MonoBehaviour
    {
        // outlets
        Rigidbody2D _rb;

        // Configuration
        public float speed;
        public float rotationSpeed;

        // Methods
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update() {
            // turn left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb.AddTorque(rotationSpeed * Time.deltaTime);
            }

            // turn right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rb.AddTorque(-rotationSpeed * Time.deltaTime);
            }

            // Thrust forward
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
            }
        }
    }
}
