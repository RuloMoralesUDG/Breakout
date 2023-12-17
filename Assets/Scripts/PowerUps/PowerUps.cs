using UnityEngine;

namespace PowerUps
{
    public class PowerUps : MonoBehaviour
    {
        private const float Speed = 5f;
        public enum PowerUpType
        {
            BigPaddle,
            SuperBall,
            LaserPaddle
        }
        public PowerUpType powerUpType;
        private void Start()
        {
            Destroy(gameObject, 10f);
        }
        private void Update( )
        { 
            transform.position += Vector3.down * (Speed * Time.deltaTime);
        }
    }
}
