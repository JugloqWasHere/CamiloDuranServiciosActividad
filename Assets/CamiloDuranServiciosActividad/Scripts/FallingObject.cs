using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FallingObject : MonoBehaviour
{
    public float lifeTime = 12f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Clean up
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
         
            GameManager.Instance.ObjectCaught();
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            GameManager.Instance.ObjectMissed();
            Destroy(gameObject);
        }
    }
}
