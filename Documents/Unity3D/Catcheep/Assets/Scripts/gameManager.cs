﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject[] sheeps;
    public int[] waitTimeForEachSpawn;
    private bool gameOver;
    private Vector3 edgeOfScreen;
    private GameObject scoreText;

    public static int sheepsCaught;
    public static int combo;
    public static int score;


    // Use this for initialization
    void Start()
    {
        sheepsCaught = 0;
        combo = 0;
        score = 0;

        scoreText = GameObject.Find("score");
        scoreText.GetComponent<Text>().text = "Score: " + score;

        //edge of screen is a vector3 that holds the screens width (can't get it directly cause of Screen/World point difference)
        edgeOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        gameOver = false;
        StartCoroutine(sheepSpawner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator sheepSpawner()
    {
        yield return new WaitForSeconds(1f);
        while (!gameOver)
        {
            int index = Random.Range(0, sheeps.Length);
            float xPosition = Random.Range(-edgeOfScreen.x, edgeOfScreen.x) / 2;

            Vector3 spawnPositionVector3 = new Vector3(xPosition, transform.position.y, transform.position.z);
            Instantiate(sheeps[index], spawnPositionVector3, Quaternion.identity);
            yield return new WaitForSeconds(waitTimeForEachSpawn[index]);
        }
    }
}