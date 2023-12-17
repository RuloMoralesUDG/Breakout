using UnityEngine;

namespace Assets.Scripts.Sprites
{
    public class Bullet : MonoBehaviour
    {
        float speed = 5f;
        void Start()
        {
        
        }
        void Update()
        {
            transform.position+=speed*Time.deltaTime*Vector3.up;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}
