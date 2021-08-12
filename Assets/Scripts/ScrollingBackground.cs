using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.05f;

    [SerializeField]
    private GameObject backgroundPrefab;

    [SerializeField]
    private Vector3 originalPos;

    [SerializeField]
    private bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed);

        if(transform.position.y <= -5.5f && hasSpawned == false)
        {
            Instantiate(backgroundPrefab, originalPos, Quaternion.identity);
            hasSpawned = true;
        }
        if(transform.position.y <= -16.3f)
        {
            Destroy(this.gameObject);
        }
    }
}
