using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotationSpeed = 0.5f;
    private Rigidbody rb;
    private bool isGrounded;
    private float yaw;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = transform.forward * v + transform.right * h;
        Vector3 velocity = direction.normalized * moveSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground")) isGrounded = true;
    }
}