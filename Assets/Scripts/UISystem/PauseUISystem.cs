using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseUISystem : MonoBehaviour
{
    
    [Header("Object")]
    [SerializeField] private SnakeMovement player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;

    public void Pausing(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.interactable = false;

        player.changeState(SnakeMovement.State.Frozen);
    }
    public void Resuming(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.interactable= true;
        
        player.changeState(SnakeMovement.State.Alive);
    }
}
