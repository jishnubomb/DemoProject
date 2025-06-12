using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 10;
    public float extraGravity = 1f;

    private Rigidbody rb;
    private bool onGround = false;
    private float deathTimer = 0;

    [SerializeField] private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        deathTimer = 0;
        onGround = false;
        transform.SetParent(null);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        // Move left and right
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            onGround = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && transform.position.x != 0)
        {
            gameManager.PauseGame();
        }

        if (rb.linearVelocity.y != 0)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Impulse);
        }

        // Reset game if fall too far
        if (deathTimer > 2)
        {
            gameManager.GameOver();
        }

        if (!onGround)
        {
            deathTimer += Time.deltaTime;
        }

        if (onGround)
        {
            deathTimer = 0;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            onGround = true;
            // Stick to moving platforms
            if (hit.gameObject.GetComponent<Platform>())
            {
                transform.SetParent(hit.transform);
            }
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            onGround = false;
            // Unstick from platforms
            transform.SetParent(null);
        }
    }
}