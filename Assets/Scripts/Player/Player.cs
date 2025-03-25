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
    public int maxHealth = 100;
    public int currentHealth;
    public int damageTaken = 20; //Урон, который будет получать игрок

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

    //Обработка столкновений ( земля + получение урона)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGrounded = true;
            wasDoubleJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damageTaken);
        }

    }

    //Логика получение урона
    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
            Die();
        Debug.Log(currentHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
