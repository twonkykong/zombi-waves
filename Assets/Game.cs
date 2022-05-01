using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject zombie, fastZombie, deadMenu, game;
    float timer, level, coins, score;
    public Text coinsText;
    public bool infinity;
    public float zombiesAmount;
    public TextMeshProUGUI stats;
    DateTime start;

    Coroutine zombiespawn;

    private void Start()
    {
        Camera.main.GetComponent<Animation>().Play("start");
        PlayerPrefs.SetFloat("emeralds", 999);
        start = DateTime.Now;
        zombiespawn = StartCoroutine(spawnZombies());
    }

    IEnumerator spawnZombies()
    {
        float zombies = 0;
        while (true)
        {
            yield return new WaitForSeconds(3);
            GameObject prefab = zombie;
            zombies += 1;
            if (Mathf.RoundToInt(UnityEngine.Random.Range(0, 10 / level)) == 0) prefab = fastZombie;
            Instantiate(prefab, spawners[UnityEngine.Random.Range(0, spawners.Length)].position + new Vector3(UnityEngine.Random.Range(-3, 3), 0, UnityEngine.Random.Range(-3, 3)), Quaternion.identity);
            if (!infinity && zombies >= zombiesAmount) yield break;
        }
    }

    public void AddCoins(int _coins)
    {
        coins += _coins;
        coinsText.text = "+" + _coins;
        coinsText.GetComponent<Animation>().Stop();
        coinsText.GetComponent<Animation>().Play();
    }

    public void Pause(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void GoToRoom(string name)
    {
        Application.LoadLevel(name);
    }

    public void EndGame()
    {
        StopCoroutine(zombiespawn);
        game.SetActive(false);
        deadMenu.SetActive(true);
        PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") + coins);
        if (PlayerPrefs.GetFloat("score") < score) PlayerPrefs.SetFloat("score", score);

        stats.text = "time: " + (DateTime.Now - start).Hours + "h " + (DateTime.Now - start).Minutes + "m " + (DateTime.Now - start).Seconds + "." + (DateTime.Now - start).Milliseconds + "s\nscore: " + score + "\n+" + coins + " coins";
    }

    public void Respawn()
    {
        if (PlayerPrefs.GetFloat("emeralds") >= 3)
        {
            PlayerPrefs.SetFloat("emeralds", PlayerPrefs.GetFloat("emeralds") - 3);
            GameObject.FindWithTag("Player").GetComponent<Player>().hp = 100;
            GameObject.FindWithTag("Player").transform.position = Vector3.zero;
            GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(Vector3.zero);
            game.SetActive(true);
            deadMenu.SetActive(false);
            PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - coins);
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<Player>().StartCoroutine("EnableGodMode");
            GameObject.FindWithTag("Player").GetComponent<Player>().dead = false;
            GameObject.FindWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            zombiespawn = StartCoroutine(spawnZombies());

            Camera.main.GetComponent<Animation>().Stop();
            Camera.main.GetComponent<Animation>().Play("start");
        }
    }
}
