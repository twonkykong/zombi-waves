using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float speed = 1, timer, endTimer = 10;
    public ParticleSystem damage;
    public GameObject damageText;

    public float hp;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Player>().hp <= 0)
        {
            anim.SetBool("walk", false);
            return;
        }

        if (timer == 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 0.65f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
                anim.SetBool("walk", true);
            }
            else
            {
                timer = 0.1f;
                anim.SetBool("walk", false);
                GetComponent<Rigidbody>().AddForce(-transform.forward * 2f, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(transform.up * 2f, ForceMode.Impulse);
                player.GetComponent<Player>().GetDamage(25);
            }

            Quaternion rot1 = transform.rotation;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            Quaternion rot2 = transform.rotation;
            transform.rotation = rot1;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot2, 0.25f);
        }

        if (timer != 0)
        {
            timer += 0.1f;
            if (timer >= endTimer)
            {
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player.GetComponent<Player>().hp <= 0) return;
        if (hp <= 0) return;
        if (collision.collider.tag == "bullet")
        {
            GetDamage(collision.collider.GetComponent<Bullet>().damage);
        }
    }

    public void GetDamage(float damag)
    {
        damage.Stop();
        damage.Play();

        int randint = Random.Range(-3, 4);
        hp -= Mathf.RoundToInt(damag + randint);
        damageText.GetComponent<TextMeshPro>().text = "-" + Mathf.RoundToInt(damag + randint);
        damageText.GetComponent<Animation>().Stop();
        damageText.GetComponent<Animation>().Play();

        if (hp <= 0)
        {
            player.GetComponent<Player>().kills += 1;
            this.gameObject.AddComponent<DeleteByTime>().endTimer = 15;
            anim.SetBool("walk", false);
            tag = "Untagged";
            GameObject.Find("gameManager").GetComponent<Game>().AddCoins(Random.Range(1, 7));
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(-transform.forward * 2f, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.up * 2f, ForceMode.Impulse);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(2, 1, 0);
            GetComponent<BoxCollider>().enabled = true;
            Destroy(GetComponent<Zombi>());
        }
    }
}
