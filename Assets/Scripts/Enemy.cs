﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime);

        if(transform.position.y < -4f)
        {
            float randomX = Random.Range(-9.8f, 9.8f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }
}
