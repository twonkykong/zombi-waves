using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crossbow : MonoBehaviour
{
    public GameObject bulletPrefab, player;
    public Transform shotPoint;
    public float magAmmo;
    public float shotTimer, animTimer;
    public Text ammoText;
    public Animator anim;
    public ParticleSystem[] shotParticles;

    public void Shot()
    {
        if (shotTimer == 0)
        {
            if (magAmmo > 0)
            {
                Instantiate(bulletPrefab, shotPoint.position, player.transform.rotation);
                magAmmo -= 1;
                shotTimer = 0.1f;
                animTimer = 0.1f;
                anim.SetBool("shot", true);
                foreach (ParticleSystem ps in shotParticles)
                {
                    ps.Stop();
                    ps.Play();
                }
            }
        }
    }

    private void Update()
    {
        ammoText.text = "AMMO: " + magAmmo + " (" + GetComponentInParent<Inventory>().crossbowAmmo + ")";
    }

    private void FixedUpdate()
    {
        if (shotTimer != 0)
        {
            shotTimer += 0.1f;
            if (shotTimer >= 7)
            {
                shotTimer = 0;
                if (GetComponentInParent<Inventory>().crossbowAmmo > 0)
                {
                    GetComponentInParent<Inventory>().crossbowAmmo -= 1;
                    magAmmo += 1;
                }
            }
        }

        if (animTimer != 0)
        {
            animTimer += 0.1f;
            if (animTimer > 0.2f)
            {
                anim.SetBool("shot", false);
                animTimer = 0;
            }
        }
    }
}
