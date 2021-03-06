using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    private Animator cameraAnimator;

    [SerializeField]
    private GameObject ExplosionEffect;

    [SerializeField]
    private Transform[] explosionArea;

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

    public SpawnManager _spawnManager;
    public GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 9, 0);

        waypoints[0] = GameObject.Find("BossWaypoint").GetComponent<Transform>();
        waypoints[1] = GameObject.Find("BossWaypoint (1)").GetComponent<Transform>();
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        Color originalColor = GetComponent<Renderer>().material.color;

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
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        GetComponent<Renderer>().material.color = Color.white;
        
    }

    IEnumerator DyingRoutine()
    {
        GameObject explosion01 = Instantiate(ExplosionEffect, explosionArea[0].position, Quaternion.identity);
        Destroy(explosion01, 2f);
        yield return new WaitForSeconds(1);
        GameObject explosion02 = Instantiate(ExplosionEffect, explosionArea[1].position, Quaternion.identity);
        Destroy(explosion02, 2f);
        yield return new WaitForSeconds(1);
        GameObject explosion03 = Instantiate(ExplosionEffect, explosionArea[2].position, Quaternion.identity);
        Destroy(explosion03, 2f);
        yield return new WaitForSeconds(1);
        GameObject explosion04 = Instantiate(ExplosionEffect, explosionArea[3].position, Quaternion.identity);
        Destroy(explosion04, 2f);
        yield return new WaitForSeconds(1);

        
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
            cameraAnimator.SetTrigger("CameraShake");
            Destroy(other.gameObject);
            _lives--;
            StartCoroutine(Hurt());
        }

        if(_lives <= 0 && isDead == false)
        {
            isDead = true;
            _speed = 0;
            _spawnManager.YouWin();
            _gameManager.GameOver();
            StartCoroutine(DyingRoutine());
            Destroy(this.gameObject, 5f);


        }

        if (other.tag == "HeatSeeker")
        {
            Destroy(other.gameObject);

            _lives--;
            StartCoroutine(Hurt());
            cameraAnimator.SetTrigger("CameraShake");
;

        }
    }
}
