using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public string name = "counter";
    public IntVariable counted;

    private void Start()
    {
        if (!counted)
        {
            counted = ScriptableObject.CreateInstance<IntVariable>();
        }
    }

    public void Count(int delta)
    {
        counted.value += delta;
    }

    public static Counter GetCounter(GameObject gameObject, string name)
    {
        foreach (Counter counter in gameObject.GetComponentsInParent<Counter>())
        {
            if (counter.name == name)
            {
                return counter;
            }
        }
        return null;
    }
}