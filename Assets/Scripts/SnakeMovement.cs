using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeMovement : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject loseGameMenu;

    private Vector3 _position;
    private Transform _tf;
    private SnakeNode _snakeNode;
    private bool _isSelected= true;

    private int _horizontal;
    private int _vertical;
    private float _lastRotationZ;

    private float _currentTimeTick;
    public float maxTimeTick;

    private State _state;
    public enum State
    {
        Alive,
        Dead,
        Frozen
    }
    private void Start()
    {
        
        _currentTimeTick = 0f;
        _horizontal = 1;
        _vertical = 0;

        _tf = transform;
        _position = _tf.position;

        loseGameMenu.SetActive(false);
        _state = State.Alive;
        
        _snakeNode = gameObject.GetComponent<SnakeNode>();
        InitializeSnakeNode();

    }

    void InitializeSnakeNode(){
        _snakeNode.AddFront( new Vector3(_tf.position.x-1,_tf.position.y),  _tf.rotation, 0);
        
    }

    private void Warp()
    {
        if (_position.x < LevelGrid.I.xLeft) _position.x = LevelGrid.I.xRight;
        else if (_position.x > LevelGrid.I.xRight) _position.x = LevelGrid.I.xLeft;
        else if (_position.y < LevelGrid.I.yBottom) _position.y = LevelGrid.I.yTop;
        else if (_position.y > LevelGrid.I.yTop) _position.y = LevelGrid.I.yBottom;
    }

    private void Move()
    {
        _currentTimeTick += Time.deltaTime;
        
        if (_currentTimeTick >= maxTimeTick)
        {
            //new Position
            var rotation = new Vector3(0, 0, GetAngelFromVector(_horizontal, _vertical));
            _position = new Vector3(_horizontal+_position.x, _vertical+_position.y,0);
            Warp();
            
            //check if the new position is a wall / food / body 
            string incomingObstacle = LevelGrid.I.CheckObstacle(_position);
            
            if (incomingObstacle == "Null")
            {
                //Nothing
                _snakeNode.AddFront( _tf.position,  _tf.rotation, rotation.z - _lastRotationZ);
                _snakeNode.RemoveBack();
            }
            else if (incomingObstacle == "Food")
            {
                _snakeNode.AddFront( _tf.position,  _tf.rotation, rotation.z - _lastRotationZ);
                LevelGrid.I.EatFood(_position);
            }
            else
            {
                //Got hit in snake body or wall
                _state = State.Dead;
                StartCoroutine(PlayerDead());
                return;
            }
            
            //now move it if it movable
            _tf.position = _position;
            _tf.eulerAngles = rotation;
            _lastRotationZ = rotation.z;
            LevelGrid.I.Grid[(int) _position.x - LevelGrid.I.xLeft, (int) _position.y - LevelGrid.I.yBottom] = gameObject;
            
           
            //recycle loop
            _currentTimeTick -= maxTimeTick;
            _isSelected = true;
        }
    }

    private void TurnChecking()
    {
        //To lock the snake from switching state move Up Down or Left Right 
        if (_vertical != 0 && _isSelected)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _vertical = 0;
                _horizontal = -1;
                _isSelected= false;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _vertical = 0;
                _horizontal = 1;
                _isSelected= false;
            }
        }
        else if (_horizontal != 0 && _isSelected)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _vertical = 1;
                _horizontal = 0;
                _isSelected= false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _vertical = -1;
                _horizontal = 0;
                _isSelected= false;
            }
        }
    }

    public void changeState(State changes){
        if(_state == State.Dead) return;
        _state = changes;
    }

    private IEnumerator PlayerDead(){
        yield return new WaitForSeconds(2);
        loseGameMenu.SetActive(true);
    }
    private void Update()
    {
        switch (_state)
        {
            case State.Alive:
                Move();
                TurnChecking();
                break;
            case State.Frozen:
                break;

            case State.Dead:
                break;
        }
    }

    private float GetAngelFromVector(int x, int y)
    {
        float n = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    
}
