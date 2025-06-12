using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class Platform : MonoBehaviour
{
    public float startY = -2;
    public float endY = 3f;
    public float speed = 5f;
    private int moveDir = 1;

    private void Start()
    {
        GameManager.OnGameReset += ResetGame;
    }

    private void ResetGame()
    {
        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Check boundaries BEFORE moving
        if (transform.position.y >= endY)
        {
            moveDir = -1;
        }
        else if (transform.position.y <= startY)
        {
            moveDir = 1;
        }

        // Then move
        transform.position += Vector3.up * moveDir * speed * Time.deltaTime;
    }
}
