using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {

        public static PlayerController instance;

        // Outlet
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        SpriteRenderer sprite;
        Animator animator;
        public TMP_Text scoreUI;

        // State tracking
        public int jumpsLeft;
        public int score;
        public bool isPaused;

        // Methods
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            score = PlayerPrefs.GetInt("Score");
        }

        void FixedUpdate()
        {
            // This update event is synced with the physics engine
            animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);

            if(_rigidbody2D.velocity.magnitude > 0)
            {
                animator.speed = _rigidbody2D.velocity.magnitude / 3f;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Update UI
            scoreUI.text = score.ToString();

            if (isPaused)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuController.instance.Show();
            }

            // Move player left
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }

            // Move player right
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }

            // Aim towards mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            // Shoot
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    _rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
            }

            animator.SetInteger("JumpsLeft", jumpsLeft);

        }

        void OnCollisionStay2D(Collision2D other)
        {
            // Check that we collide with ground
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // Check what is directly below the character's feet
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.85f);
                // Debug.DrawRay(transform.position, Vector2.down * 0.85f);

                // We might have multiple things below our character's feet
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];

                    // Check that we collided with ground below our feet
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        // Reset jump count
                        jumpsLeft = 2;
                    }
                }
            }
        }

        public void ResetScore()
        {
            score = 0;
            PlayerPrefs.DeleteKey("Score");
        }
    }
}

