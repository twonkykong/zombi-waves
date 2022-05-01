using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timer;
    public GameObject explosion;

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5, 6), Random.Range(-5, 6));
    }
    private void FixedUpdate()
    {
        timer += 0.1f;
        if (timer >= 15)
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, 1.5f))
            {
                if (col.tag == "zombie")
                {
                    col.GetComponent<Zombi>().GetDamage(80 - (Vector3.Distance(transform.position, col.transform.position) / 1.5f * 30));
                    col.GetComponent<Rigidbody>().AddExplosionForce(300, transform.position, 1.5f);
                }
                else if (col.tag == "Player")
                {
                    col.GetComponent<Player>().GetDamage(50 - (Vector3.Distance(transform.position, col.transform.position) / 1.5f * 20));
                    col.GetComponent<Rigidbody>().AddExplosionForce(250, transform.position, 1.5f);
                }
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
