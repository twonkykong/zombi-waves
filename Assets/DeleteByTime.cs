using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByTime : MonoBehaviour
{
    public float timer, endTimer;
    public bool deleteByCollision;

    private void FixedUpdate()
    {
        timer += 0.1f;
        if (timer >= endTimer) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (deleteByCollision) Destroy(gameObject);
    }
}
