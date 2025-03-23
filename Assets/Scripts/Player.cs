using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem jumpParticle;

    //TECH
    private Rigidbody2D _rb;
    private bool isGrounded = true;
    bool wasDoubleJump = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        MovmentLogic();
    }

    private void MovmentLogic()
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rb.velocity.y);
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            if (wasDoubleJump)
                return;
            else
                wasDoubleJump = true;
        }

        _rb.velocity = new Vector2(_rb.velocity.x, 6);
        isGrounded = false;
        Instantiate(jumpParticle,transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGrounded = true;
            wasDoubleJump = false;
        }
    }
}
