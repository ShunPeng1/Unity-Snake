using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGameUISystem : MonoBehaviour
{
    
    [Header("Object")]
    [SerializeField] private SnakeMovement player;
    [SerializeField] private GameObject loseGameMenu;

    void Start(){
        Resuming();
    }
    public void Pausing(){
        Time.timeScale = 0;
        loseGameMenu.SetActive(true);

    }
    public void Resuming(){
        Time.timeScale = 1;
        loseGameMenu.SetActive(false);
    
    }
}
