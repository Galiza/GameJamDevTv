using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Status")]
    [SerializeField] int health = 1;

    // Behaviour configuration
    private Transform target;
    private Movement movementScript;

     private void Awake()
     {
        movementScript = GetComponent<Movement>();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        float speed = movementScript.moveSpeed * Time.deltaTime;
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagType.Projectile.ToString()))
        {
            Hit();
            DetectDeath();
        }
    }

    void Hit()
    {
        health--;
    }

    private void DetectDeath()
    {
        if (health.Equals(0))
        {
            Destroy(gameObject);
        }
    }
}
