using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Walls
{
    public int xLeft;
    public int xRight;
    public int yBottom;
    public int yTop;
}



public class LevelGrid : MonoBehaviour
{
    public static LevelGrid I;

    //Map position
    public int xLeft;
    public int xRight;
    public int yBottom;
    public int yTop;

    private Vector3 _foodPosition;

    //Coordinate
    public GameObject[,] Grid;
    private int _gridSize;
    public Walls[] wallsArray;
    
    public int maxNumFood;
    private bool _initialStart;

    public void IncreaseGridSize()
    {
        _gridSize++;
    }
    public void DecreaseGridSize()
    {
        _gridSize--;
    }
    private void SpawnFood(Vector3 notThisPosition)
    {
        while (_gridSize-1>0)
        {
            _foodPosition = new Vector3(Random.Range(xLeft, xRight+1), Random.Range(yBottom, yTop+1), 0);
            if (_foodPosition == notThisPosition) continue;
            if (CheckObstacle(_foodPosition) != "Null") continue;
            
            GameObject foodGameObject = Instantiate(GameAssets.I.RandomFoodSprite(), _foodPosition, Quaternion.identity);
            Grid[(int) _foodPosition.x - xLeft, (int) _foodPosition.y - yBottom] = foodGameObject;
            
            break;
        }
    }

    public void EatFood(Vector3 theEatingPosition)
    {
        Destroy(Grid[(int) theEatingPosition.x - xLeft, (int) theEatingPosition.y - yBottom]);
        Grid[(int) theEatingPosition.x - xLeft, (int) theEatingPosition.y - yBottom] = null;
        SpawnFood(theEatingPosition);
    }
    
    /*public bool CheckEating(Vector3 snakeHeadPosition)
    {
        foreach (var t in _foodStorage)
        {
            if (t.transform.position != snakeHeadPosition) continue;
            Destroy(t);
            _foodStorage.Remove(t);
            Grid[(int) snakeHeadPosition.x - xLeft, (int) snakeHeadPosition.y - yBottom] = null;
            
            SpawnFood();
            return true;
        }
        return false;
    }*/
    
    public string CheckObstacle(Vector3 thePosition)
    {
        if (Grid[(int) thePosition.x - xLeft, (int) thePosition.y - yBottom] == null)
        {
            return "Null";
        }
        return Grid[(int) thePosition.x - xLeft, (int) thePosition.y - yBottom].tag;
    }


    private void SpawnGrass()
    {
        //Spawn Grass Prefabs
        for (int i = 0; i <= xRight - xLeft; i++)
        {
            for (int j = 0; j <= yTop - yBottom; j++)
            {
                var grass = Instantiate(GameAssets.I.GrassSprite(), new Vector3(i + xLeft, j + yBottom, 0), new Quaternion());
            }
        }
    }

    private void NullifyGrid()
    {
        for (int i = 0; i <= xRight - xLeft; i++)
        {
            for (int j = 0; j <= yTop - yBottom; j++)
            {
                Grid[i , j ] = null;
            }
        }
    }
    private void SpawnInteractable(){
        
        //Spawn Wall Prefabs
        foreach (var wall in wallsArray)
        {
            for (int j = 0; j <= wall.xRight - wall.xLeft; j++)
            {
                for (int k = 0; k <= wall.yTop-wall.yBottom; k++)
                {
                    GameObject wallGo = Instantiate(GameAssets.I.WallSprite(), new Vector3(j+wall.xLeft, k+wall.yBottom, 0), new Quaternion());
                    Grid[j+wall.xLeft-xLeft,k+wall.yBottom-yBottom] = wallGo;
                    DecreaseGridSize();
                }
            }
        }

        //Spawn Food Prefabs
        for (var i = 0; i < maxNumFood; i++)
        {
            SpawnFood(new Vector3(0,0, 0));
            DecreaseGridSize();
        }
        
    }
    
    void Start()
    {
        I = this;
        _initialStart = true;
        
        Grid = new GameObject[xRight-xLeft+1,yTop-yBottom+1];
        _gridSize = (xRight - xLeft + 1) * (yTop - yBottom + 1);
    }
    
    void Update()
    {
        if (!_initialStart) return;
        
        SpawnGrass();
        NullifyGrid();
        SpawnInteractable();
        
        _initialStart = false;
    }
    

}
