using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private Transform _playerTrans;

    
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _powerupClip;
    // Start is called before the first frame update
    void Start()
    {
        _playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.C))
        {
            _speed = 0;
            transform.position = Vector2.MoveTowards(transform.position, _playerTrans.position, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerupClip, transform.position);

            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoost();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.AmmoPowerUp();
                        break;
                    case 4:
                        player.HealthUp();
                      
                        break;
                    case 5:
                        player.HeatSeek();
                        break;
                    case 6:
                        player.Damage();
                        break;

                }
            } 

            Destroy(this.gameObject);
        }

        if(other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }
}
