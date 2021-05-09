using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethLaser : MonoBehaviour
{
    private float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
