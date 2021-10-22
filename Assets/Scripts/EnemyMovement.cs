using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 start;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Distance the enemy will travel
    public float journeyLength;

    void Start()
    {
        start = transform.position;
    }

    // Makes the object oscillate between two points
    void Update()
    {
        transform.position = start + transform.up * Mathf.Sin(Time.time * speed) * journeyLength;
    }
}
