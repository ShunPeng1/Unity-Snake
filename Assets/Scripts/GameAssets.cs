using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameAssets I;
    public GameObject[] foodArray;

    public GameObject RandomFoodSprite()
    {
        var choice = Random.Range(0, foodArray.Length);
        return foodArray[choice];
    }
    public GameObject snakeNode;
    public GameObject SnakeNodeSprite()
    {
        return snakeNode;
    }
    public GameObject snakeTail;
    public GameObject SnakeTailSprite()
    {
        return snakeTail;
    }
    public GameObject snakeTurnLeft;
    public GameObject SnakeTurnLeftSprite()
    {
        return snakeTurnLeft;
    }
    public GameObject snakeTurnRight;
    public GameObject SnakeTurnRightSprite()
    {
        return snakeTurnRight;
    }
    public GameObject grass;
    public GameObject GrassSprite()
    {
        return grass;
    }
    
    public GameObject wall;
    public GameObject WallSprite()
    {
        return wall;
    }
    void Start()
    {
        I = this;
        
    }
}
