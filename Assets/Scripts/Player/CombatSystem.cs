using System.Collections;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Changeble")]
    [SerializeField] int damage = 20;
    [SerializeField] float attackRange = 7f;
    [SerializeField] float attackCooldown = 2f;
    [Header("Tech")]
    [SerializeField] ParticleSystem attackParticles;

    private bool canAttack = true;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(IEAttack());
        }
    }

    IEnumerator IEAttack()
    {
        canAttack = false;
        Instantiate(attackParticles,transform.position,transform.rotation);
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, attackRange, transform.rotation.eulerAngles);

        foreach (RaycastHit2D obj in hit) 
        {
            if (obj.collider.TryGetComponent<Enemy>(out Enemy _enemy))
            {
                _enemy.TakeDamage(damage);
                Debug.Log(damage);
            } 
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }



}
