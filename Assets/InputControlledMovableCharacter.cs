using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlledMovableCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer sprite;
    private Animator anim;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = direction.x * speed;
        rigidbody2d.velocity = velocity;
        anim.SetBool("running", direction.x != 0);
        if (direction.x > 0) sprite.flipX = false;
        if (direction.x < 0) sprite.flipX = true;
    }
}
