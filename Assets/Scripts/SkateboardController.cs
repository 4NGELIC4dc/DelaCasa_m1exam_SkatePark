using UnityEngine;

public class SkateboardController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;
    public float jumpForce = 50f;
    public float speedBoost = 20f;
    public float brakeStrength = 0.5f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical") * moveSpeed;

        float turnInput = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        if (rb.velocity.magnitude > 5f) 
        {
            turnInput *= 0.75f; 
        }

        transform.Rotate(0, turnInput, 0);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }

        // Speed up
        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 20f; 
        }

        rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        // Break
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.velocity *= brakeStrength;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (Vector3.Dot(Vector3.up, collision.contacts[0].normal) < 0.9f)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Acceleration);
            }

            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(-horizontalVelocity * 2f, ForceMode.Acceleration); 
        }
    }

}
