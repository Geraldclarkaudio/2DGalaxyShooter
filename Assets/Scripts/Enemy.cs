using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

           Player player = other.transform.GetComponent<Player>();

             if(player != null)
             {
                 player.Damage();
             }

             Destroy(this.gameObject);
         }

         if (other.tag == "Laser")
         { 
             Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.AddToScore(10);
            }

             Destroy(this.gameObject);
         }   
    }
}
