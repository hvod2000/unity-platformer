using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlledMovableCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    private Rigidbody2D rigidbody2d;
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody2d.velocity += direction * speed * 0.2f;
    }
}
