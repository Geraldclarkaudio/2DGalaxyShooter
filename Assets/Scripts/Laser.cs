using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 8.0f;

    private bool _isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }

        else
        {
            MoveDown();
        }
       
    }
    void MoveUp()
    {
        if (_isEnemyLaser == false)
        {
            transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);

            if (transform.position.y >= 8.0f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
    void MoveDown()
    {
        if (_isEnemyLaser == true)
        {
            transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);

            if(transform.position.y <= -6.0f)
            {
                if(transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);
            }
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
