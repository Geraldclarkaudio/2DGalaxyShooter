using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;


    [SerializeField]
    private GameObject teethLasers;

    float timeToNextShot;
    
    public int _lives = 25;

    bool isDead = false;

    UIManager _uiManager;

    private float _canFire = -1;
    private float _fireRate = 1.5f;


    [SerializeField]
    Transform[] waypoints;

    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 9, 0);

        waypoints[0] = GameObject.Find("BossWaypoint").GetComponent<Transform>();
        waypoints[1] = GameObject.Find("BossWaypoint (1)").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        BossMovement();
        ShootTeeth();
    }

    private void BossMovement()

    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < 3.75)
        {

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3.75f, 7f), 0);
            //move back and forth on x axis
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, _speed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }


        }
    }


    private void ShootTeeth()
    {
        //instantite the prefab make it go downward
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(1.5f, 3.0f);
            _canFire = Time.time + _fireRate;
            Instantiate(teethLasers, transform.position, Quaternion.identity);
            
        }
    }


    IEnumerator Hurt()
    {
        Color originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        GetComponent<Renderer>().material.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isDead == false)
        {
            if(other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().Damage();
            }
        }

        if(other.tag == "Laser" && isDead == false)
        {
            Destroy(other.gameObject);
            _lives--;
            StartCoroutine(Hurt());
        }

        if(_lives <= 0 && isDead == false)
        {
            isDead = true;
            //dyingroutine


        }
    }
}
