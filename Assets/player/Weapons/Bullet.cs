using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
    }
}
