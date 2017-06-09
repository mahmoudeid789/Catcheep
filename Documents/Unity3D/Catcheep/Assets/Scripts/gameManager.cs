﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class gameManager : MonoBehaviour
{
    public GameObject[] sheeps;
    public int[] waitTimeForEachSpawn;
    private bool gameOver;
    private Vector3 edgeOfScreen;
    private GameObject scoreText;

    public static int totalSheepsCaught;
    public static int combo;
    public static int score;
    public static bool catchedSomething;


    // Use this for initialization
    void Start()
    {
        catchedSomething = false;
        totalSheepsCaught = 0;
        combo = 0;
        score = 0;

        scoreText = GameObject.Find("score");
        scoreText.GetComponent<Text>().text = "Score: " + score;

        //edge of screen is a vector3 that holds the screens width (can't get it directly cause of Screen/World point difference)
        edgeOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        gameOver = false;
        StartCoroutine(sheepSpawner());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!catchedSomething && combo > 0)
            {
                combo = 0;
            }

            if (catchedSomething)
            {
                catchedSomething = false;
            }
        }
    }

    IEnumerator sheepSpawner()
    {
        yield return new WaitForSeconds(1f);

        while (!gameOver)
        {
            /* int i = Random.Range(1, 100);

             switch (i)
             {
                 case 0:
                     oneSheepyRandom();
                     yield return new WaitForSeconds(2);
                     break;

                 case 1:
                     twoSheepyHorizontalManySet(3);
                     yield return new WaitForSeconds(4);
                     break;

                 case 2:
                     threeSheepyHorizontalpartScreen();
                     yield return new WaitForSeconds(3);
                     break;

                 case 3:
                     oneSheepyRandom();
                     yield return new WaitForSeconds(3);
                     break;

                 case 4:
                     fourSheepyTriangleLookingDownUp(-1);
                     yield return new WaitForSeconds(4);
                     break;

                 case 5:
                     fourSheepyTriangleLookingDownUp(1);
                     yield return new WaitForSeconds(4);
                     break;

                 case 6:
                     threeSheepyHorizontalFullScreen();
                     yield return new WaitForSeconds(4);
                     break;

                 case 7:
                     twoSheepyHorizontalOneSet();
                     yield return new WaitForSeconds(3);
                     break;
             }*/


            //threeSheepySlidingRightUpDown(1);
            //yield return new WaitForSeconds(3);

            vFormeSheepy(2);
            yield return new WaitForSeconds(3);
        }

    }
    //one sheepy formations:
    void oneSheepyRandom()
    {
        float edges = edgeOfScreen.x - (sheeps[0].GetComponent<SpriteRenderer>().sprite.bounds.extents).x;
        float xPosition = Random.Range(-edges, edges);
        Vector3 spawnPosition = new Vector3(xPosition,transform.position.y,transform.position.z);
        Instantiate(sheeps[0], spawnPosition, Quaternion.identity);
    }

    void oneSheepyChosen(float xPosition, float deltaYPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y + deltaYPosition, transform.position.z);
        Instantiate(sheeps[0], spawnPosition, Quaternion.identity);
    }
    //end of one sheepy formations;

    //two sheepy formations:
    void twoSheepyHorizontalOneSet()
    {
            float gap = Random.Range(edgeOfScreen.x * 0.4f, edgeOfScreen.x * 0.7f);
            float edges = edgeOfScreen.x - ((sheeps[0].GetComponent<SpriteRenderer>().sprite.bounds.extents).x * 2);
            // i used edges - gap cause i instantiate from the left and i m making sure the sheep wont go overboard 
            // when we add the gap value to the xPosition to create the spacing
            float xPosition = Random.Range(-edges , edges - gap);
    
            oneSheepyChosen(xPosition , 0f);
            oneSheepyChosen(xPosition +  gap,0f);   
    }

    void twoSheepyHorizontalManySet(int times)
    {
        float gap = Random.Range(edgeOfScreen.x * 0.4f, edgeOfScreen.x * 0.7f);
        float edges = edgeOfScreen.x - (sheeps[0].GetComponent<SpriteRenderer>().sprite.bounds.extents).x;
        // i used edges - gap cause i instantiate from the left and i m making sure the sheep wont go overboard 
        // when we add the gap value to the xPosition to create the spacing
        float xPosition = Random.Range(-edges, edges - gap);
        for (int i = 0; i < times; i++)
        {
            oneSheepyChosen(xPosition, i * gap);
            oneSheepyChosen(xPosition + gap, i * gap);
        }
        
    }
    //ende of two sheepy formations;

    //three sheepy formations:
    void threeSheepyHorizontalFullScreen()
    {
        float xPosition = -edgeOfScreen.x + (sheeps[0].GetComponent<SpriteRenderer>().bounds.extents).x;

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition + (edgeOfScreen.x * 0.8f * i), transform.position.y, transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
        } 
    }

    void threeSheepyHorizontalpartScreen()
    {
        // positioning the first (left) sheepy
        // sheepy width is half the width of the sheep sprite
        float sheepyWidth = (sheeps[0].GetComponent<SpriteRenderer>().bounds.extents).x;
        // edges is the max left position value that a sheepy can have (screen width + concidering the sheepy width)
        float edges = -edgeOfScreen.x + sheepyWidth;
        // multiplied by 2 cause sheepy width is HALF of the sprite width and we dont want a sheepy showing in half on the right side J.I.C
        float xPosition = Random.Range(edges,0f -(2 *sheepyWidth));

        // finding out how much gap should be between the sheepys (randomly)
        float gap = Random.Range(edgeOfScreen.x * 0.4f, edgeOfScreen.x * 0.7f);

        //instantiating the sheepys
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition + (gap * i), transform.position.y, transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
        }
    }

    void threeSheepySlidingRightUpDown(int direction)
    {
        // positioning the first (left = 1 or right = -1 depending on the direction) sheepy
        float sheepyWidth = (sheeps[0].GetComponent<SpriteRenderer>().bounds.size).x;
        // edges is the max left position value that a sheepy can have (screen width + concidering the sheepy width)
        float edges = direction * (-edgeOfScreen.x + sheepyWidth);
        //choosing half of screen where to position first sheepy depending on direction
        float xPosition = Random.Range(edges, 0f - direction * (2 *sheepyWidth));
        
        // finding out how much gap should be between the sheepys (randomly)
        float gap = Random.Range(edgeOfScreen.x * 0.4f, edgeOfScreen.x * 0.7f);

        //instantiating the sheepys
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition + direction * (gap * i), transform.position.y + (gap * i), transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
        }
    }
    //ende of three sheepy formations;

    //four sheepy formations:
    void fourSheepyTriangleLookingDownUp(int direction)
    {

        //some paramtres that we need to instantite correctly
        float sheepyWidth = (sheeps[0].GetComponent<SpriteRenderer>().bounds.extents).x;
        float edges = -edgeOfScreen.x + sheepyWidth;
        float xPosition = Random.Range(edges, 0f - (2 * sheepyWidth));
        float gap = Random.Range(edgeOfScreen.x * 0.4f, edgeOfScreen.x * 0.7f);

        //saving the position of the second sheepy to creat the triangle
        Vector3 secondSheepy = new Vector3();

        //instantiating the sheepys
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition + (gap * i), transform.position.y, transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
            if (i == 1) secondSheepy = spawnPositionVector3;
        }
        
        oneSheepyChosen(secondSheepy.x, direction * gap);
    }
    //ende of two sheepy formations;

    //V forme sheepy
    void vFormeSheepy(int number)
    {
        float gap;
        // finding out how much gap should be between the sheepys
        switch (number)
        {
            case 2:
                gap = edgeOfScreen.x * 0.6f;
                break;
            case 3:
                gap = edgeOfScreen.x * 0.4f;
                break;
            default:
                gap = 0f;
                break;
        }
        
        // choosing half of screen where to position first sheepy depending on direction
        float xPosition = 0f; //Random.Range(edges, -edges);

        //instantiating the sheepys
        for (int i = 0; i < number; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition + (gap * i), transform.position.y + (gap * i), transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
        }

        //instantiating the sheepys
        for (int i = 1; i < number; i++)
        {
            Vector3 spawnPositionVector3 = new Vector3(xPosition - (gap * i), transform.position.y + (gap * i), transform.position.z);
            Instantiate(sheeps[0], spawnPositionVector3, Quaternion.identity);
        }

    }

    //end of V frome sheepy sheepy
}