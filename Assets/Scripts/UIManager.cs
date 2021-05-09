﻿using System.Collections;
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
    private Text _restartText;

    [SerializeField]
    private Text _waveText;

    [SerializeField]
    private int _wave;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Text _ammoText;


    //handle to text
    // Start is called before the first frame update
    void Start()
    {
        _ammoText.text = "Ammo: " + 15;
        _scoreText.text = "Score: " + 0;
        _waveText.text = "Wave: " + 0;
        _waveText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWave(int currentwave)
    {
        _wave = currentwave;
        StartCoroutine(ShowWave());
    }

    public IEnumerator ShowWave()
    {
        if(_wave == 4)
        {
            _waveText.text = "BOSS BATTLE";
        }
        _waveText.text = "Wave: " + _wave;
        _waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        _waveText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateAmmo(int ammoAmount)
    {
        _ammoText.text = "Ammo: " + ammoAmount;

        if(ammoAmount < 1)
        {
            _ammoText.text = "Ammo: " + 0;
        }
    }

   public void UpdateLives(int currentlives)
    {
        //display img sprite
        //give new oner based on current lives
        _livesImg.sprite = _livesSprites[currentlives];

        if(currentlives == 0)
        {
            GameOverSequence(); 
        }
    }

    
    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
