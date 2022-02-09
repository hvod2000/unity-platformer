using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSoftController : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(target.position.x, target.position.y, pos.z);
        transform.position = pos;
    }
}
