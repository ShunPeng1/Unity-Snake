using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnakeNode : MonoBehaviour
{
    private List<GameObject> _nodeStorage;
    
    public void AddFront(Vector3 snakeHeadPosition, Quaternion snakeHeadRotation, float turnDegree)
    {
        //Choose node between turning and straight
        GameObject nodeGameObject;
        if (_nodeStorage.Count<1 )
        {
            nodeGameObject = Instantiate(GameAssets.I.SnakeTailSprite(), snakeHeadPosition, snakeHeadRotation);
        }
        else if (turnDegree == -90.0f || turnDegree == 270.0f)
        {
            nodeGameObject = Instantiate(GameAssets.I.SnakeTurnRightSprite(), snakeHeadPosition, snakeHeadRotation);
        }
        else if (turnDegree == 90.0f || turnDegree == -270.0f)
        {
            nodeGameObject = Instantiate(GameAssets.I.SnakeTurnLeftSprite(), snakeHeadPosition, snakeHeadRotation);
        }
        else
        {
            nodeGameObject = Instantiate(GameAssets.I.SnakeNodeSprite(), snakeHeadPosition, snakeHeadRotation);
        }
        
        _nodeStorage.Insert(0,nodeGameObject);
        LevelGrid.I.Grid[(int) snakeHeadPosition.x - LevelGrid.I.xLeft, (int) snakeHeadPosition.y - LevelGrid.I.yBottom] = nodeGameObject;
        LevelGrid.I.DecreaseGridSize();
    }
    public void RemoveBack()
    {
        if (_nodeStorage.Count <= 0) return;
        var lastGameObject = _nodeStorage[_nodeStorage.Count - 1];
        Destroy(lastGameObject);
        _nodeStorage.Remove(lastGameObject);
        
        var position = lastGameObject.transform.position;
        LevelGrid.I.Grid[(int) position.x - LevelGrid.I.xLeft, (int) position.y - LevelGrid.I.yBottom] = null;
        LevelGrid.I.IncreaseGridSize();
        
        //Setting SnakeTail
        if (_nodeStorage.Count <= 0) return;
        GameObject  secondLastGameObject = _nodeStorage[_nodeStorage.Count - 1];
        GameObject tailGameObject = Instantiate(GameAssets.I.SnakeTailSprite(), secondLastGameObject.transform.position, TailTurn(secondLastGameObject));
        Vector3 position2 = tailGameObject.transform.position;
        LevelGrid.I.Grid[(int) position2.x - LevelGrid.I.xLeft, (int) position2.y - LevelGrid.I.yBottom] = tailGameObject;
        _nodeStorage[_nodeStorage.Count - 1] = tailGameObject;
        Destroy(secondLastGameObject);
    }

    private Quaternion TailTurn(GameObject theBeReplaced)
    {
        
        if (theBeReplaced.CompareTag("SnakeTurnLeft"))
        {
            return Quaternion.Euler(0, 0, 90.0f) *theBeReplaced.transform.rotation ;
        }

        if (theBeReplaced.CompareTag("SnakeTurnRight"))
        {
            return Quaternion.Euler(0, 0, -90.0f)*theBeReplaced.transform.rotation ;
        }

        return theBeReplaced.transform.rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        _nodeStorage = new List<GameObject>();
    }

}
