using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }

        Instance = this; 
    }
    #endregion

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    private int coins;
    private int points;

    private int totalCoins;
    private int highScore; 
    

    // Start is called before the first frame update
    void Start()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString(); 
        }
        if(scoreText != null)
        {
            scoreText.text = points.ToString(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin()
    {
        coins++;
        coinText.text = coins.ToString(); 
    }

    public void AddPoint()
    {
        points++;
        scoreText.text = points.ToString();
    }


    #region GameOver
    public void GameOver()
    {
        totalCoins += coins;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); 
    }

    #endregion
}
