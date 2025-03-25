using UnityEngine;
// Это враг, который следует за игроком
public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float chaseRange = 5f;


    private Transform target;
    private bool isChasing = false;
    
   
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime );
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            FlipSprite(target.position.x > transform.position.x);
        }
    }

    private void FlipSprite(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
