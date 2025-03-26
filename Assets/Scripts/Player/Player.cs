using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private ParticleSystem jumpParticle;

    //TECH
    private Rigidbody2D _rb;
    private bool isGrounded = true;
    bool wasDoubleJump = false;

    //Health
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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

    //Jump
    private void Jump()
    {
        if (!isGrounded)
        {
            if (wasDoubleJump)
                return;
            else
            {
                wasDoubleJump = true;
                Instantiate(jumpParticle, transform.position, Quaternion.identity);
            }  
        }

        _rb.velocity = new Vector2(_rb.velocity.x, 12);
        isGrounded = false;
        
    }

    //Collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGrounded = true;
            wasDoubleJump = false;
        }
    }

    //Take damage logic
    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
