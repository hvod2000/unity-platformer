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
    } 

    [SerializeField]
    private float speed = 4;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer sprite;
    private Animator anim;
    
    private Vector2 direction = Vector2.zero;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        
        anim.SetInteger("desired state", (int)state);
    }
}
