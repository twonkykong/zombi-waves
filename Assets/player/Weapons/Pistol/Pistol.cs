using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab, player, magPrefab;
    public Transform shotPoint, magPoint;
    public float magAmmo, fullMagAmmo;
    public float reloadTimer, reloadTimerEnd = 10, shotTimer, animTimer;
    public Text ammoText;
    public Animator anim;
    public ParticleSystem[] shotParticles;

    public void Shot()
    {
        if (reloadTimer == 0 && shotTimer == 0)
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
            else
            {
                Reload();
            }
        }
    }

    public void Reload()
    {
        if (GetComponentInParent<Inventory>().pistolAmmo != 0)
        {
            reloadTimer = 0.1f;
            Instantiate(magPrefab, magPoint.position, transform.rotation);
            anim.SetBool("reload", true);
        }
    }

    private void Update()
    {
        ammoText.text = "AMMO: " + magAmmo + " (" + GetComponentInParent<Inventory>().pistolAmmo + ")";
    }

    private void FixedUpdate()
    {
        if (reloadTimer != 0)
        {
            reloadTimer += 0.1f;
            if (reloadTimer >= reloadTimerEnd)
            {
                if (GetComponentInParent<Inventory>().pistolAmmo > fullMagAmmo) magAmmo = fullMagAmmo;
                else magAmmo = GetComponentInParent<Inventory>().pistolAmmo;

                GetComponentInParent<Inventory>().pistolAmmo -= magAmmo;
                reloadTimer = 0;
                anim.SetBool("reload", false);
            }
        }

        if (shotTimer != 0)
        {
            shotTimer += 0.1f;
            if (shotTimer >= 1.2f)
            {
                shotTimer = 0;
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
