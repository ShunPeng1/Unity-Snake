using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentScore = 0;
    [SerializeField] private int goalScore = 10;

    [Header("Score UI")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image scoreBar;
    
    public void AddScore(int value){
        currentScore+= value;
        scoreText.text = "Score : "+ currentScore.ToString();
        scoreBar.fillAmount = (float)currentScore/(float)goalScore;
        
        CheckCompletement();
    }

    private void CheckCompletement(){
        if(goalScore == currentScore) SceneSystem.instance.GetNextScene();
    }

}
