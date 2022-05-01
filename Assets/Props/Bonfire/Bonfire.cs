using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public GameObject player;
    float timer;

    private void FixedUpdate()
    {
        timer += 0.1f;
        if (timer >= 2.5f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
            {
                if (player.GetComponent<Player>().hp < 100)
                {
                    player.GetComponent<Player>().hp += Random.Range(2, 8);
                }
                timer = 0;
            }
        }
    }
}
