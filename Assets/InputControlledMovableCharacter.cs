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
    } 

    [SerializeField] private float speed = 4;
    [SerializeField] private LayerMask impassableObstacle;
    
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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!canMoveInDirection(new Vector2(direction.x, 0f)))
        {
            direction.x = 0f;
        }
        
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = direction.x * speed;
        rigidbody2d.velocity = velocity;
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        AnimationState state = AnimationState.idle;

        if (direction.x != 0)
        {
            sprite.flipX = (direction.x < 0);
            state = AnimationState.running;
        }

        if (rigidbody2d.velocity.y < -0.1)
        {
            state = AnimationState.falling;
        }

        anim.SetInteger("desired state", (int)state);
    }

    bool canMoveInDirection(Vector2 direction)
    {
        return !Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, direction, 0.01f, impassableObstacle);
    }
}
