using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public enum State
    {
        idle = 103,
        running = 236,
        falling = 7569,
        dead = 1598,
    }

    public State AnimationState
    {
        get => currentAnimationState;

        set
        {
            if (value != currentAnimationState)
            {
                animator.SetInteger("desired state", (int)value);
            }

            currentAnimationState = value;
        }
    }

    private State currentAnimationState;
    private SpriteRenderer sprite;
    private HealthState health;
    private Rigidbody2D body2D;
    private Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<HealthState>();
        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (body2D && Math.Abs(body2D.velocity.x) > 0.1)
        {
            sprite.flipX = (body2D.velocity.x < 0);
        }
        AnimationState = NextAnimationState();
    }

    private State NextAnimationState()
    {
        if (health && !health.Alive)
        {
            return State.dead;
        }

        if (body2D)
        {
            Vector2 velocity = body2D.velocity;

            if (velocity.y < -0.1)
            {
                return State.falling;
            }

            if (Math.Abs(velocity.x) > 0.1)
            {
                return State.running;
            }
        }

        return State.idle;
    }
}
