using Assets.Scripts.Controllers;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sprites.Bricks
{
    public class BrickBase : MonoBehaviour
    {
        protected GameManager _gameManager;
        protected int Resistance;
        [SerializeField] protected GameObject[] powerUpsPrefabs;
        protected virtual void Awake()
        {
            _gameManager = FindFirstObjectByType<GameManager>();
        }
        protected virtual void Start()
        {
            if (_gameManager == null) return;
            Resistance = 1;
            _gameManager.BricksOnLevel++;
            _gameManager.PowerUpPossibilityPercentage = 100;//(byte)Random.Range(0,100);
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                Resistance--;
            }

            if (Resistance > 0) return;
            if (_gameManager != null)
            {
                _gameManager.BricksOnLevel--;
            }
            if(powerUpsPrefabs.Length!=0)
            {
                if (_gameManager != null && _gameManager.BigPaddle == false && _gameManager.LaserPaddle == false && _gameManager.SuperBall == false)
                {
                    var randomNumber = Random.Range(0,100);
                    if (randomNumber < _gameManager.PowerUpPossibilityPercentage)
                    {
                        var randomPowerUp= Random.Range(0, powerUpsPrefabs.Length);
                        Instantiate(powerUpsPrefabs[randomPowerUp], transform.position, Quaternion.identity);
                    }
                }
            }
            if (Resistance == 0)
            {
                Destroy(gameObject);
            }
            
        }
        
    }
}
