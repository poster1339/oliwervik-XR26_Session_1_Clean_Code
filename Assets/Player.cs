using UnityEngine;
using TMPro; // Assuming TextMeshPro is used for UI
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float rotationSpeed = 0.5f; // For mouse rotation

    private Rigidbody rb;
    private bool isGrounded;
    private int score = 0;
    private float health = 30f;
    private bool isJumping = false;
    private float yaw; // For mouse rotation

    // Tightly coupled dependency to GameManager
    [SerializeField]
    private GameManager gameManager;

    // Direct UI references - bad practice for player script
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Slider healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Player needs a Rigidbody component!");
        }
        // Freeze rotation to prevent physics flipping the player
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to screen center
        
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("Player cannot find GameManager!");
            }
        }
        UpdateScoreUI();
        UpdateHealthUI();
        
        // Initialize health bar max value
        if (healthBar != null)
        {
            healthBar.maxValue = 30f; // Set to match max health value
            healthBar.value = health; // Ensure current value matches
        }
    }

    void Update()
    {
        // Handle rotation with mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Jumping Logic (Monolithic, handles animation state and physics)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true; // Simple animation state
            Debug.Log("Player jumped!");
        }


        // Game Over condition tightly coupled here
        if (health <= 0)
        {
            Debug.Log("Player defeated!");
            if (gameManager != null)
            {
                gameManager.GameOver(); // Direct call to GameManager
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }

        // Collecting collectibles (Monolithic, handles score and interaction)
        if (collision.gameObject.CompareTag("Collectible"))
        {
            score += 10;
            UpdateScoreUI();
            Destroy(collision.gameObject);
            Debug.Log("Collected! Score: " + score);
        }

        // Enemy collision (damages player)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
            Debug.Log("Player hit by enemy! Health: " + health);
        }
    }

    // Health management (Monolithic, includes UI logic)
    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0); // Health won't go below zero
        UpdateHealthUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    public int GetScore()
    {
        return score;
    }

    void FixedUpdate()
    {
        // Handle movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * v + transform.right * h;
        Vector3 velocity = direction.normalized * moveSpeed;

        Vector3 newPos = rb.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}