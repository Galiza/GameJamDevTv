using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player attributes")]
    [SerializeField] int health = 5;

    [Header("Movement attributes")]
    [SerializeField] float rotationSpeed = 1000f;

    // Cached reference
    private Rigidbody2D rb;
    private Movement movementScript;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MousePosition();
    }

    void Move()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * movementScript.moveSpeed * Time.deltaTime;
        float verticalSpeed = Input.GetAxis("Vertical") * movementScript.moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    void MousePosition()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime); ;
    }

    private void Hit()
    {
        health--;
        if (health.Equals(0))
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagType.Enemy.ToString()))
        {
            Hit();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        GameObject.FindObjectOfType<SceneManagement>().LoadStartMenu();
    }

}
