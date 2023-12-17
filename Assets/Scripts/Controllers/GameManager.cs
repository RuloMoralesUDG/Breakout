using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.Sprites;
using Sprites;
using UnityEngine;
namespace Controllers
{
    public class GameManager : MonoBehaviour
    {
        private float _gameTime;
        [SerializeField] private GameObject ballPrefab;
        private UiController _uIController;
        private Ball _ball;
        private bool _isGameStarted;
        internal bool IsGameStarted 
        { 
            get=> _isGameStarted; 
            set 
            {
                _isGameStarted = value;
                if (_isGameStarted)
                {
                    _gameTime=Time.time;
                }
            }
        }
        private bool _isBallOnPLay;
        internal bool IsBallOnPLay 
        {
            get=> _isBallOnPLay;
            set 
            {
                _isBallOnPLay = value;
                if (_isBallOnPLay)
                {
                    _ball.Launch();
                }
            }
        }
        private bool _bigPaddle;
        internal bool BigPaddle
        { 
            get=>_bigPaddle;
            set
            {
                _bigPaddle = value;
            }
        }
        private bool _laserPaddle;
        internal bool LaserPaddle
        {
            get => _laserPaddle;
            set
            {
                _laserPaddle = value;
            }
        }
        private bool _superBall;
        internal bool SuperBall
        { 
            get => _superBall;
            set
            {
                _superBall = value; _ball.ToggleTrail();
            }
        }
        [SerializeField] private byte bricksOnLevel;
        internal byte BricksOnLevel 
        {
            get=> bricksOnLevel;
            set 
            { 
                bricksOnLevel=value;
                if (bricksOnLevel != 0) return;
                Destroy(GameObject.Find("Ball"));
                _gameTime =Time.time-_gameTime;
                _uIController.ShowWinnerPanel(_gameTime);
            }
        }
        private byte _lives;
        internal byte Lives 
        { 
            get=> _lives;
            set 
            {
                _lives = value;
                if (_lives == 0)
                {
                    Destroy(_ball.gameObject);
                    _gameTime = Time.time - _gameTime;
                    _uIController.ShowLosePanel();
                }
                else
                {
                    _ball.Reset();
                }
                _uIController.UpdateUILives(_lives);
            }
        }
        private byte _bigPaddleTime;
        internal byte BigPaddleTime
        {
            get=>_bigPaddleTime;
            set
            {
                _bigPaddleTime = value;
            }
        }
        private byte _superBallTime;
        internal byte SuperBallTime
        {
            get=>_superBallTime;
            set
            {
                _superBallTime = value;
            }
        }
        private byte _laserPaddleTime;
        internal byte LaserPaddleTime 
        {
            get=>_laserPaddleTime;
            set
            {
                _laserPaddleTime = value;
            }
        }
        private float _paddleSpeed;
        internal float PaddleSpeed 
        {
            get=>_paddleSpeed;
            set
            {
                _paddleSpeed = value;
            }
        }
        private float _xLimitPositionPaddle;
        internal float XLimitPositionPaddle 
        {
            get=>_xLimitPositionPaddle;
            set
            {
                _xLimitPositionPaddle = value;
            }
        }
        private float _xLimitWhenBigPaddle;
        internal float XLimitWhenBigPaddle 
        {
            get=>_xLimitWhenBigPaddle;
            set
            {
                _xLimitWhenBigPaddle = value;
            }
        }
        private byte _powerUpPossibilityPercentage;
        internal byte PowerUpPossibilityPercentage 
        {
            get=>_powerUpPossibilityPercentage;
            set
            {
                _powerUpPossibilityPercentage = value;
            }
        }
        private void Awake()
        {
            _uIController = FindFirstObjectByType<UiController>();
            _lives=3;
            Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
        private void Start()
        {
            // _uIController.UpdateUILives(lives);
            _ball = FindFirstObjectByType<Ball>();
            _bigPaddleTime = 10;
            _superBallTime = 10;
            _laserPaddleTime = 2;
            _paddleSpeed = 5f;
            _xLimitPositionPaddle = 7.5f;
            _xLimitWhenBigPaddle = 8.5f;
            
        }
    }
}
