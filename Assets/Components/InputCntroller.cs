using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCntroller : MonoBehaviour
{
    private Movable movable;
    private void Start()
    {
        movable = GetComponent<Movable>();
    }

    private void Update()
    {
        float movingDirection = Input.GetAxisRaw("Horizontal");
        movable.GoInDirection(movingDirection);
    }
}
