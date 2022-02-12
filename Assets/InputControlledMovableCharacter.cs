using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlledMovableCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private LayerMask impassableObstacle;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider;
    private Animator anim;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GoInDirection(direction.x);
    }

    public void GoInDirection(float movingDirection)
    {
        if (!CanMoveInDirection(new Vector2(movingDirection, 0f)))
        {
            return;
        }
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = movingDirection * speed;
        rigidbody2d.velocity = velocity;
    }

    private bool CanMoveInDirection(Vector2 direction)
    {
        return !Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, direction, 0.01f, impassableObstacle);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Impassable Obstacle"))
        {
            transform.SetParent(collider.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("exit");
        if (collider.gameObject.layer == LayerMask.NameToLayer("Impassable Obstacle"))
        {
            transform.SetParent(null);
        }
    }
}
