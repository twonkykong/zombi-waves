using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFire : MonoBehaviour
{
    float timer;

    private void Start()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, 1.5f))
        {
            if (col.GetComponent<MolotovFire>() != null && col.gameObject != gameObject) Destroy(col.gameObject);
        }
    }

    private void FixedUpdate()
    {
        timer += 0.1f;
        if (timer >= 2.5f)
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, 1.5f))
            {
                if (col.tag == "zombie")
                {
                    col.GetComponent<Zombi>().GetDamage(8);
                    timer = 0;
                }
                else if (col.tag == "Player")
                {
                    col.GetComponent<Player>().GetDamage(4);
                    timer = 0;
                }
            }
        }
    }
}
