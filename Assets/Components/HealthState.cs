using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    public bool Alive
    {
        get => alive;

        set
        {
            if (value == alive)
            {
                return;
            }

            if (value)
            {
                Resurrect();
            }
            else
            {
                Die();
            }
        }
    }

    [SerializeField] private bool alive = true;

    [SerializeField] private Counter deathCounter;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (!deathCounter)
        {
            deathCounter = Counter.GetCounter(gameObject, "deaths");
        }
    }

    public void OnDeadlyRegionStay(GameObject regionGameObject)
    {
        if (!alive)
        {
            return;
        }

        if (rigidbody2D && rigidbody2D.velocity.y < -0.1)
        {
            Alive = false;
        }
    }

    private void Resurrect()
    {
        if (TryGetComponent<Movable>(out var movable))
        {
            movable.enabled = true;
        }
        alive = true;
    }

    private void Die()
    {
        if (TryGetComponent<Movable>(out var movable))
        {
            movable.enabled = false;
        }

        if (deathCounter)
        {
            deathCounter.Count(1);
        }

        alive = false;
    }
}
