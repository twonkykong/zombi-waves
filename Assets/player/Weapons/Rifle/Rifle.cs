using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : MonoBehaviour
{
    public GameObject bulletPrefab, player, magPrefab;
    public Transform shotPoint, magPoint;
    public float magAmmo, fullMagAmmo;
    public float reloadTimer, reloadTimerEnd = 10, shotTimer, animTimer;
    public Text ammoText;
    public Animator anim;
    public ParticleSystem[] shotParticles;
    public bool shooting;

    public void Shot(bool value)
    {
        if (value)
        {
            if (magAmmo > 0)
            {
                shooting = true;
                anim.SetBool("shot", true);
            }
            else Reload();
        }
        else
        {
            shooting = false;
            anim.SetBool("shot", false);
        }
    }

    public void Reload()
    {
        if (GetComponentInParent<Inventory>().rifleAmmo != 0)
        {
            reloadTimer = 0.1f;
            Instantiate(magPrefab, magPoint.position, transform.rotation);
            anim.SetBool("reload", true);
        }
    }

    private void Update()
    {
        ammoText.text = "AMMO: " + magAmmo + " (" + GetComponentInParent<Inventory>().rifleAmmo + ")";

        if (shooting)
        {
            if (reloadTimer == 0 && shotTimer == 0)
            {
                if (magAmmo > 0)
                {
                    Instantiate(bulletPrefab, shotPoint.position, player.transform.rotation);
                    magAmmo -= 1;
                    shotTimer = 0.1f;
                    animTimer = 0.1f;
                    foreach (ParticleSystem ps in shotParticles)
                    {
                        ps.Stop();
                        ps.Play();
                    }
                }
                else
                {
                    shooting = false;
                    anim.SetBool("shot", false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (reloadTimer != 0)
        {
            reloadTimer += 0.1f;
            if (reloadTimer >= reloadTimerEnd)
            {
                if (GetComponentInParent<Inventory>().rifleAmmo > fullMagAmmo) magAmmo = fullMagAmmo;
                else magAmmo = GetComponentInParent<Inventory>().rifleAmmo;

                GetComponentInParent<Inventory>().rifleAmmo -= magAmmo;
                reloadTimer = 0;
                anim.SetBool("reload", false);
            }
        }

        if (shotTimer != 0)
        {
            shotTimer += 0.1f;
            if (shotTimer >= 1)
            {
                shotTimer = 0;
            }
        }

        if (animTimer != 0)
        {
            animTimer += 0.1f;
            if (animTimer > 0.2f)
            {
                animTimer = 0;
            }
        }
    }
}
