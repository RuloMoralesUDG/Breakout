using System.Collections;
using Controllers;
using UnityEngine;
namespace Sprites
{
    public class Paddle : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Vector3 shootingOffset;
        private bool _isGameManagerNotNull;
        private enum ModeBehavior
        {
            Mouse,
            Keyboard,
            Axis
        }
        [SerializeField]private ModeBehavior currentMode;
        private void Awake()
        {
            _gameManager = FindFirstObjectByType<GameManager>();
        }
        private void Start( )
        {
            transform.position = new Vector3(0,-4.2f,0);
            _isGameManagerNotNull = _gameManager != null;
        }
        private void Update( )
        {
            PaddleBehavior(currentMode);
            StatusGameAndBall();
        }
        private void MoveLeft( )
        {
            transform.position += _gameManager.PaddleSpeed * Time.deltaTime * Vector3.left;
        }
        private void MoveRight( )
        {
            transform.position += _gameManager.PaddleSpeed * Time.deltaTime * Vector3.right;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PowerUp"))
            {
                if (collision.GetComponent<PowerUps.PowerUps>().powerUpType == PowerUps.PowerUps.PowerUpType.BigPaddle)
                {
                    _gameManager.BigPaddle = true;
                    if (_gameManager.BigPaddle)
                    {
                        StartCoroutine(BigPaddlePower());
                    }
                
                }else if (collision.GetComponent<PowerUps.PowerUps>().powerUpType == PowerUps.PowerUps.PowerUpType.SuperBall)
                {
                    _gameManager.SuperBall = true;
                    if (_gameManager.SuperBall)
                    {
                        StartCoroutine(StopSuperBall());
                    }
                }
                else if (collision.GetComponent<PowerUps.PowerUps>().powerUpType == PowerUps.PowerUps.PowerUpType.LaserPaddle)
                {
                    _gameManager.LaserPaddle = true;
                    if (_gameManager.LaserPaddle)
                    {
                        StartCoroutine(ContinuousShooting());
                    }
                }
                Destroy(collision.gameObject);
            }
        }
        private void StatusGameAndBall()
        {
            if (!Input.GetKey(KeyCode.Space)) return;
            if (!_isGameManagerNotNull) return;
            if (!_gameManager.IsGameStarted)
            {
                _gameManager.IsGameStarted = true;
            }
            if (!_gameManager.IsBallOnPLay)
            {
                _gameManager.IsBallOnPLay = true;
            }
        }
        private void PaddleBehavior(ModeBehavior controlPaddleMode)
        {
            switch (controlPaddleMode)
            {
                case ModeBehavior.Mouse:
                    Debug.Log("mouse");
                    var mousePositions2D = Input.mousePosition;
                    mousePositions2D.z = -Camera.main.transform.position.z;
                    var mousePositions3D = Camera.main.ScreenToWorldPoint(mousePositions2D);
                    var position = transform.position;
                    position.x = mousePositions3D.x;
                    if (position.x < -_gameManager.XLimitPositionPaddle)
                    {
                        position.x = -_gameManager.XLimitPositionPaddle;
                    }
                    else if (position.x > _gameManager.XLimitPositionPaddle)
                    {
                        position.x = _gameManager.XLimitPositionPaddle;
                    }
                    transform.position = position;
                    break;
                case ModeBehavior.Keyboard when Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -_gameManager.XLimitPositionPaddle:
                    MoveLeft();
                    break;
                case ModeBehavior.Keyboard:
                {
                    if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < _gameManager.XLimitPositionPaddle)
                    {
                        MoveRight();
                    }
                    break;
                }case ModeBehavior.Axis:
                    Debug.Log("Axis");
                    transform.Translate(Vector3.right * (Input.GetAxis("Horizontal") * _gameManager.PaddleSpeed * Time.deltaTime));
                    break;
                default:
                    Debug.Log("No control");
                    break;
            }
        }
        private IEnumerator ContinuousShooting( )
        {
            var stopShootingTime = Time.time * _gameManager.LaserPaddleTime;
            while (Time.time < stopShootingTime)
            {
                Instantiate(bulletPrefab, transform.position + shootingOffset, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                _gameManager.LaserPaddle = false;
            }
        }
        private IEnumerator StopSuperBall( )
        {
            yield return new WaitForSeconds(_gameManager.SuperBallTime);
            _gameManager.SuperBall = false;
        }
        private IEnumerator BigPaddlePower( ) 
        {
            var originalXLimit = _gameManager.XLimitPositionPaddle;
            if (_gameManager.BigPaddle)
            {
                _gameManager.XLimitPositionPaddle = _gameManager.XLimitWhenBigPaddle;
            }
            var newSize = transform.localScale;
            while (transform.localScale.x<=10f) 
            {
                newSize.x += 0.1f;
                transform.localScale = newSize;
            }
            yield return new WaitForSeconds(_gameManager.BigPaddleTime);
            while (transform.localScale.x >= 6f)
            {
                newSize.x -= 0.1f;
                transform.localScale = newSize;
            }
            _gameManager.BigPaddle = false;
            _gameManager.XLimitPositionPaddle = originalXLimit;
        }
    }
    
}
