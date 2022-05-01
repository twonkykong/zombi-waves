using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hp = 100, shield = 0, kills;
    public bool dead;
    public Text hpText, killsText;
    public GameObject weaponsObj, craftButton, prop, weaponObj, changeWeaponButton, dropWeaponButton;
    public GameObject[] weapons, weaponsButtons;

    public ParticleSystem damage;
    public RectTransform hpImage;

    public Sprite pistolImage, shotgunImage, rifleImage, crossbowImage;

    private void Update()
    {
        killsText.text = "KILLS: " + kills;
        hpText.text = "" + hp;
        hpImage.sizeDelta = new Vector2(hp * 3, 40);
        if (hp <= 0)
        {
            if (!dead)
            {
                hp = 0;
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerMove>().anim.SetBool("walk", false);
                GetComponent<PlayerMove>().walkPS.SetActive(false);
                GameObject.Find("gameManager").GetComponent<Game>().EndGame();
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().AddForce(-transform.forward * 2f, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(transform.up * 2f, ForceMode.Impulse);
                GetComponent<Rigidbody>().angularVelocity = new Vector3(2, 1, 0);

                Camera.main.GetComponent<Animation>().Stop();
                Camera.main.GetComponent<Animation>().Play("dead");
            }
            dead = true;
        }
        else if (hp > 100) hp = 100;
    }

    public void DropWeapon()
    {
        weaponObj.transform.parent = null;
        weaponObj.SetActive(true);
        weaponObj.GetComponent<Rigidbody>().AddForce(transform.forward * 2f, ForceMode.Impulse);
        weaponObj = null;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
            weaponsButtons[i].SetActive(false);
        }

        dropWeaponButton.SetActive(false);
    }

    public void ChangeWeapon()
    {
        if (weaponObj != null)
        {
            weaponObj.transform.parent = null;
            weaponObj.SetActive(true);
            weaponObj.GetComponent<Rigidbody>().AddForce(transform.forward * 2f, ForceMode.Impulse);
        }
        else
        {
            dropWeaponButton.SetActive(true);
        }
        
        weaponObj = prop;
        weaponObj.transform.position = transform.position + transform.up * 0.5f + transform.forward * 0.5f;
        weaponObj.SetActive(false);
        weaponObj.transform.parent = transform;
        weaponObj.transform.rotation = transform.rotation;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
            weaponsButtons[i].SetActive(false);
            if (weapons[i].name.Contains(weaponObj.name.Split('(')[0].Replace(" ", "")))
            {
                weapons[i].SetActive(true);
                weaponsButtons[i].SetActive(true);
            }
        }

        changeWeaponButton.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ammo") 
        {
            if (collision.collider.name.Contains("pistolAmmo")) weaponsObj.GetComponent<Inventory>().pistolAmmo += 5;
            else if (collision.collider.name.Contains("shotgunAmmo")) weaponsObj.GetComponent<Inventory>().shotgunAmmo += 3;
            else if (collision.collider.name.Contains("rifleAmmo")) weaponsObj.GetComponent<Inventory>().rifleAmmo += 20;
            else if (collision.collider.name.Contains("grenadeAmmo")) weaponsObj.GetComponent<Inventory>().grenades += 1;
            else if (collision.collider.name.Contains("knifeAmmo")) weaponsObj.GetComponent<Inventory>().knifes += 1;
            else if (collision.collider.name.Contains("bandageAmmo")) weaponsObj.GetComponent<Inventory>().bandages += 1;
            else if (collision.collider.name.Contains("firstAidAmmo")) weaponsObj.GetComponent<Inventory>().firstAid += 1;
            else if (collision.collider.name.Contains("woodAmmo")) weaponsObj.GetComponent<Inventory>().wood += 1;
            else if (collision.collider.name.Contains("wallAmmo")) weaponsObj.GetComponent<Inventory>().walls += 1;

            Destroy(collision.collider.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "craftingTable")
        {
            craftButton.SetActive(true);
        }
        else if (other.tag == "weapon")
        {
            prop = other.gameObject;
            changeWeaponButton.SetActive(true);

            if (prop.name.Contains("pistol")) changeWeaponButton.GetComponent<Button>().image.sprite = pistolImage;
            else if (prop.name.Contains("shotgun")) changeWeaponButton.GetComponent<Button>().image.sprite = shotgunImage;
            else if (prop.name.Contains("rifle")) changeWeaponButton.GetComponent<Button>().image.sprite = rifleImage;
            else if (prop.name.Contains("crossbow")) changeWeaponButton.GetComponent<Button>().image.sprite = crossbowImage;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "craftingTable")
        {
            craftButton.SetActive(false);
        }
        else if (other.tag == "weapon")
        {
            prop = null;
            changeWeaponButton.SetActive(false);
        }
    }

    public void GetDamage(float damag)
    {
        hp -= damag + Random.Range(-3, 3);

        damage.Stop();
        damage.Play();
    }

    public IEnumerator EnableGodMode()
    {
        StartCoroutine(GodMode());
        yield return new WaitForSeconds(5);
        StopAllCoroutines();
    }

    public IEnumerator GodMode()
    {
        while (true)
        {
            hp = 100;
            yield return new WaitForEndOfFrame();
        }
    }
}
