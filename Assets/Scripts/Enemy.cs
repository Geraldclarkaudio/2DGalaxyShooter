﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    private float _enemySpeed = 4;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private Player _player;

    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

    private AudioSource _enemyAudioSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAudioSource = GetComponent<AudioSource>();
        
        if(_player == null)
        {
            Debug.LogError("Player is Null");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        if(_enemyAudioSource == null)
        {
            Debug.LogError("AudioSource On Enemy is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        EnemyFire();

    }

    public void EnemyFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -4f)
        {
            float randomX = Random.Range(-9.8f, 9.8f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

           Player player = other.transform.GetComponent<Player>();

             if(player != null)
             {
                 player.Damage();
             }

            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _enemyAudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Enemy>());
            Destroy(this.gameObject, 2.5f);
         }

         if (other.tag == "Laser")
         { 
             Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.AddToScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _enemyAudioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Enemy>());
            Destroy(this.gameObject, 2.5f);
         }   
    }
}
