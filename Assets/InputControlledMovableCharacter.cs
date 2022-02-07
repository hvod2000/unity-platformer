using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlledMovableCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = direction.x * speed;
        rigidbody2d.velocity = velocity;
        anim.SetBool("running", direction.x != 0);
    }
}
