using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _livesSprites;
    //handle to text
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        //assign text component to handle 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

   public void UpdateLives(int currentlives)
    {
        //display img sprite
        //give new oner based on current lives
        _livesImg.sprite = _livesSprites[currentlives];

        if(currentlives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
