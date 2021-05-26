using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidEnemy : MonoBehaviour
{
    private float speed = 4;

    public Rigidbody2D rb;

    [SerializeField]
    private GameObject _ExplodeAnimPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player _player = GameObject.Find("Player").GetComponent<Player>();

        if(collision.CompareTag("Laser"))
        {
            this.transform.position = transform.position + new Vector3(2, 0, 0);
        }
        
        if (collision.tag == "HeatSeeker")
        {
            Instantiate(_ExplodeAnimPrefab, transform.position, Quaternion.identity);
           
            if (_player != null)
            {
                _player.AddToScore(100);
            }

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }


}
