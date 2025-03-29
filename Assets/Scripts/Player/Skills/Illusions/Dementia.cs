using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Dementia : MonoBehaviour
{
    [Header("Changeble")]
    [SerializeField] float skillCooldown;
    [Header("Tech")]
    [SerializeField] ParticleSystem skillParticles;

    private bool canAttack = true;
    private Vector3 lastPosition;
    private float lastPositionTimer = 2;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            StartCoroutine(IESkill());
        }

        lastPositionTimer -= Time.deltaTime;
        if(lastPositionTimer < 0)
        {
            lastPosition = transform.position;
            lastPositionTimer = 3;
        }
        
    }

    IEnumerator IESkill()
    {
        canAttack = false;

        transform.position = lastPosition;
        Instantiate(skillParticles, transform.position, transform.rotation);

        yield return new WaitForSeconds(skillCooldown);
        canAttack = true;
    }
}