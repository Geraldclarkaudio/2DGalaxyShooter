using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethLaser : MonoBehaviour
{
    private Player _player;
    private float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player.Damage();
        }
    }
}
