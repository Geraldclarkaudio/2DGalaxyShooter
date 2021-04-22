﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager spawnManager;

    [SerializeField]
    private bool isTripleShotActive = false;
    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject playerShieldPrefab;

    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject rightEngineSprite;

    [SerializeField]
    private GameObject leftEngineSprite; 

    private UIManager _uiManager;

    [SerializeField]
    private GameObject explosionAnimationPrefab;

    //variable to store audio clip 
    [SerializeField]
    private AudioClip _laserFireSound;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL!");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULLLLL!");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is Null!");
        }
        else
        {
            _audioSource.clip = _laserFireSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        //local variables
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //Input moves with this
        transform.Translate(direction * _speed * Time.deltaTime);
       

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {

        _canFire = Time.time + _fireRate;
        // Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity); 

        if (isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        //play laser audio clip 
        _audioSource.Play();
    }

    public void Damage()
    {
        if(isShieldActive == true)
        {
            isShieldActive = false;
            playerShieldPrefab.SetActive(false);
            return;
        }
        else
        {
            _lives--;

            if(_lives == 2)
            {
                rightEngineSprite.SetActive(true);
            }
            else if(_lives == 1)
            {
                leftEngineSprite.SetActive(true);
            }

            _uiManager.UpdateLives(_lives);
        }
       

        //check if dead//if so destroy 

        if (_lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Instantiate(explosionAnimationPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        while (isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5);
            isTripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        while (isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5);
            isSpeedBoostActive = false;
            _speed /= _speedMultiplier;
        }
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        playerShieldPrefab.SetActive(true);
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
