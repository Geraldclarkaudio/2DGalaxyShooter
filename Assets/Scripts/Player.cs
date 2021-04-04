using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       // transform.Translate(Vector3.right * horizontalInput  * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //if player position on y is greagter than 0, set y position to 0. 
        if(transform.position.y >= 0)
        {
            //uses current position on x axis so it doesnt snap to the middle
            transform.position = new Vector3(transform.position.x,0,0);
        }
        else if(transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if(transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if(transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }
}
