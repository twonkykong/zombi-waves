using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrossBow : MonoBehaviour
{
    float timer;
    public GameObject bulletPrefab;
    public Transform shotPoint;

    private void FixedUpdate()
    {
        timer += 0.1f;
        if (timer >= 5)
        {
            timer = 0;
            Instantiate(bulletPrefab, shotPoint.position, transform.rotation);
        }
    }
}
