using System.Collections;
using Controllers;
using UnityEngine;

namespace Sprites
{
    public class Ball : MonoBehaviour
    {
        private Vector2 _moveDirection;
        private Vector2 _currentVelocity;
        private Rigidbody2D _ballRigidBody2D;
        private GameManager _gameManager;
        private const float YMinVelocity = 5f;
        [SerializeField] private TrailRenderer trailRenderer;

        private void Start()
        {
            _ballRigidBody2D =GetComponent<Rigidbody2D>();    
            _gameManager = FindFirstObjectByType<GameManager>();
            InitializedBall(gameObject);
        }

        private void FixedUpdate( )
        {
            _currentVelocity = _ballRigidBody2D.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"Collision with {collision.transform.name}");
            if (collision.transform.CompareTag("Brick") && _gameManager.SuperBall) 
            {
                _ballRigidBody2D.velocity = _currentVelocity;
                return;
            }
            _moveDirection = Vector2.Reflect(_currentVelocity,collision.contacts[0].normal);
            if (_gameManager.IsBallOnPLay)
            {
                if (Mathf.Abs(_moveDirection.y)<YMinVelocity)
                {
                    _moveDirection.y = YMinVelocity * Mathf.Sign(_moveDirection.y);
                }
            }
            _ballRigidBody2D.velocity = _moveDirection;
            if (!collision.transform.CompareTag("DeathLimit")) return;
            if (_gameManager != null)
            {
                _gameManager.Lives--;
            }
        }
        internal void Launch()
        {
            transform.SetParent(null);
            _ballRigidBody2D.velocity = YMinVelocity*Vector2.up;
        }
        internal void Reset()
        {
            var paddle = GameObject.Find("Paddle").transform;
            paddle.localScale = new Vector3(6f, 2f, 1f);
            _ballRigidBody2D.velocity = Vector2.zero;
            transform.SetParent(paddle);
            Vector2 ballPosition= paddle.position;
            ballPosition.y += 0.5f;
            transform.position = ballPosition;
            _gameManager.IsBallOnPLay = false;
        }
        internal void ToggleTrail( )
        {
            trailRenderer.enabled = !trailRenderer.enabled;
        }
        
        private void InitializedBall(GameObject ball)
        {
            if (GameObject.Find("Paddle") == null) return;
            var ballPosition = GameObject.Find("Paddle").transform.position;
            ballPosition.y += 0.5f;
            ball.transform.position = ballPosition;
            ball.transform.SetParent(GameObject.Find("Paddle").transform);
        }


    }
}
