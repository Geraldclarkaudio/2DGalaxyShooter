using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}


