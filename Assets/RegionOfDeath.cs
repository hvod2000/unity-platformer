using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionOfDeath : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<InputControlledMovableCharacter>(out var player))
        {
            player.OnDeadlyRegionStay(gameObject);
        }
    }
}
