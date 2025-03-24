using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Это враг, который следует за игроком
public class enemy : MonoBehaviour
{
    float speed =3;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position,speed*Time.fixedDeltaTime);
    }
}