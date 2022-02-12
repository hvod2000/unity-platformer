using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    public bool alive = true;

    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnDeadlyRegionStay(GameObject regionGameObject)
    {
        if (!alive)
        {
            return;
        }

        if (rigidbody2D && rigidbody2D.velocity.y < -0.1)
        {
            alive = false;
        }
    }
}
