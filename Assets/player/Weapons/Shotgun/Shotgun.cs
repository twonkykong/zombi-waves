using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    public GameObject player, magPrefab;
    public GameObject[] bullets;
    public Transform shotPoint, magPoint;
    public float magAmmo, fullMagAmmo;
    public float reloadTimer, reloadTimerEnd = 20, shotTimer, animTimer;
    public Text ammoText;
    public Animator anim;
    public ParticleSystem[] shotParticles;

    public void Shot()
    {
        if (reloadTimer == 0 && shotTimer == 0)
        {
            if (magAmmo > 0)
            {
                Instantiate(bullets[Random.Range(0, bullets.Length)], shotPoint.position, Quaternion.Euler(Random.Range(-10, 11), player.transform.eulerAngles.y + Random.Range(-10, 11), player.transform.eulerAngles.z));
                Instantiate(magPrefab, magPoint.position, player.transform.rotation);
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
            if (shotTimer >= 7.5f)
            {
                shotTimer = 0;
            }
        }

        if (animTimer != 0)
        {
            animTimer += 0.1f;
            if (animTimer >= 0.3f)
            {
                anim.SetBool("shot", false);
                animTimer = 0;
            }
        }
    }
}
