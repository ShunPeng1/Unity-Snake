using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreUISystem : MonoBehaviour
{
    private int currentScore = 0;
    [SerializeField] private int goalScore = 10;

    [Header("Score UI")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image scoreBar;

    
    [Header("Game Object")] 
    [SerializeField] private GameObject winGameMenu;
    
    public bool AddScore(int value){
        currentScore+= value;
        scoreText.text = "Score : "+ currentScore.ToString();
        scoreBar.fillAmount = (float)currentScore/(float)goalScore;
        
        
        if(goalScore <= currentScore) {
            winGameMenu.SetActive(true);
            return true;
        }
        return false;
    }

    

}
