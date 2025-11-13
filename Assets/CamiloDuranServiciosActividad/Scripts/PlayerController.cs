using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f; // side speed
    private Rigidbody rb;
    private Vector3 moveVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input movement
        float h = Input.GetAxis("Horizontal");
        moveVelocity = new Vector3(h * moveSpeed, rb.linearVelocity.y, 0f);
    }

    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // On crash, get game manager
        if (collision.collider.CompareTag("Deadly"))
        {
            GameManager.Instance.PlayerDied();
        }
    }
}
