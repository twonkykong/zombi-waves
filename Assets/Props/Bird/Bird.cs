using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool fly, walk;
    public Vector3 pos, start;
    public float timer, endTimer;

    public GameObject player;
    public Animator anim;

    Vector3 lookPos;

    private void Start()
    {
        start = transform.position;
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            walk = false;
            if (fly == false)
            {
                pos = transform.position + (Vector3.up * 10) + (player.transform.forward * 25);
                fly = true;
            }
            timer = 0;

            anim.SetBool("fly", true);
            anim.SetBool("walk", false);
        }

        timer += 0.1f;
        if (walk)
        {
            lookPos = new Vector3(pos.x, transform.position.y, pos.z);
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.025f);
            if (timer >= Random.Range(25, 36) || transform.position == new Vector3(pos.x, transform.position.y, pos.z))
            {
                timer = 0;
                endTimer = Random.Range(10, 25);
                walk = false;

                anim.SetBool("walk", false);
            }
        }

        if (fly)
        {
            if (timer == 0.1f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
            lookPos = pos;

            if (transform.position == pos)
            {
                fly = false;
                timer = 0;
                endTimer = 150;
                GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                anim.SetBool("fly", false);
            }
        }

        if (!walk && !fly)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 3)
            {
                lookPos = player.transform.position;
            }

            if (timer >= endTimer)
            {
                if (endTimer > 100)
                {
                    walk = true;
                    transform.position = start;
                    pos = transform.position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));
                    anim.SetBool("walk", true);
                    GetComponent<Rigidbody>().isKinematic = false;

                    GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
                else
                {
                    walk = true;
                    pos = transform.position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));

                    anim.SetBool("walk", true);
                }
            }
        }

        Quaternion rot1 = transform.rotation;
        transform.LookAt(lookPos);
        Quaternion rot2 = transform.rotation;
        transform.rotation = rot1;
        transform.rotation = Quaternion.Slerp(transform.rotation, rot2, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        pos = transform.position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));
        if (!walk && !fly)
        {
            walk = true;
            timer = 0;
        }
    }
}
