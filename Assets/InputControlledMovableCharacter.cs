using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlledMovableCharacter : MonoBehaviour
{
    private enum AnimationState
    {
        idle = 103,
        running = 236,
        falling = 7569,
        dead = 1598,
    }

    [SerializeField] private float speed = 4;
    [SerializeField] private LayerMask impassableObstacle;
    private AnimationState state = AnimationState.idle;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    private Animator anim;

    private Vector2 direction = Vector2.zero;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (state != AnimationState.dead)
        {
            ProcessInput();
        }
        UpdateAnimationState();
    }

    void ProcessInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GoInDirection(direction.x);
    }

    public void GoInDirection(float movingDirection)
    {
        if (!canMoveInDirection(new Vector2(movingDirection, 0f)))
        {
            return;
        }
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = movingDirection * speed;
        rigidbody2d.velocity = velocity;
    }

    void UpdateAnimationState()
    {
        if (state != AnimationState.dead)
        {
            state = AnimationState.idle;

            if (direction.x != 0)
            {
                sprite.flipX = (direction.x < 0);
                state = AnimationState.running;
            }

            if (rigidbody2d.velocity.y < -0.1)
            {
                state = AnimationState.falling;
            }
        }

        anim.SetInteger("desired state", (int)state);
    }

    bool canMoveInDirection(Vector2 direction)
    {
        return !Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, direction, 0.01f, impassableObstacle);
    }

    public void OnDeadlyRegionStay(GameObject regionGameObject)
    {
        if (rigidbody2d.velocity.y < -0.1)
        {
            state = AnimationState.dead;
        }
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
