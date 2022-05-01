using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    public GameObject explosion, firePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ground")
        {
            Instantiate(firePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, 1.5f))
            {
                if (col.tag == "zombie")
                {
                    col.GetComponent<Zombi>().GetDamage(40 - (Vector3.Distance(transform.position, col.transform.position) / 1.5f * 20));
                    col.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 1.5f);
                }
                else if (col.tag == "Player")
                {
                    col.GetComponent<Player>().GetDamage(20 - (Vector3.Distance(transform.position, col.transform.position) / 1.5f * 5));
                    col.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 1.5f);
                }
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
