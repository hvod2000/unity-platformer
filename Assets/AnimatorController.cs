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
    private HealthState health;
    private Rigidbody2D body2D;
    private Animator animator;

    void Start()
    {
        health = GetComponent<HealthState>();
        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationState = GetNextAnimationState();
    }

    private State GetNextAnimationState()
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
