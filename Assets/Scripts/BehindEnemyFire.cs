using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindEnemyFire : MonoBehaviour
{
    private float _canFire = -1f;
    private float _fireRate = 2f;

    [SerializeField]
    private GameObject _behindLaser;

    private Transform _player;

    private float speed = 4f;


    // Start is called before the first frame update
    void Start()
    {
       
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireBehind();
    }

    private void FireBehind()
    {
        if (Time.time > _canFire && transform.position.y < _player.transform.position.y)

        {
            _fireRate = Random.Range(1.5f, 2.0f);
            _canFire = Time.time + _fireRate;
            Instantiate(_behindLaser, transform.position, Quaternion.identity);
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.y <= -10f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = GameObject.Find("Player").GetComponent<Player>();

        if (other.tag == "HeatSeeker")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(50);
            }
           
           
            speed = 0;
            //make explode
            gameObject.tag = "Untagged";
            Destroy(this.gameObject, 2.5f);

        }
    }

}
