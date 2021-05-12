using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamEnemy : MonoBehaviour
{
    private SpawnManager spawnManager;

    [SerializeField]
    private float _enemySpeed = 2;

    [SerializeField]
    private GameObject _laserBeamPrefab;

    private bool isLaserBeamActive = false;
   

    [SerializeField]
    private int hitPoints;

    [SerializeField]
    private GameObject rightEngineSprite;

    [SerializeField]
    private GameObject leftEngineSprite;

    [SerializeField]
    private GameObject _ExplodeAnimPrefab;

    private Player _player;

    private AudioSource _enemyAudioSource;

    [SerializeField]
    Transform[] waypoints;

    int waypointIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 3;
        _laserBeamPrefab.SetActive(false);

        waypoints[0] = GameObject.Find("Waypoint").GetComponent<Transform>();
        waypoints[1] = GameObject.Find("Waypoint (1)").GetComponent<Transform>();


        _player = GameObject.Find("Player").GetComponent<Player>();
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _enemyAudioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is Null on laser enemy");
        }
        if (_enemyAudioSource == null)
        {
            Debug.LogError("AudioSource On laser Enemy is Null");
        }

        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL on laser Enemy");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if(isLaserBeamActive == false)
        {
            StartCoroutine(LaserBeamRoutine());
        }
    }

    public void CalculateMovement()
    {

        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < 3)
        {
          
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3.0f, 7f), 0);
            //move back and forth on x axis
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, _enemySpeed * Time.deltaTime);

            if(transform.position == waypoints [waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            if(waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }

            
        }
    }


    IEnumerator LaserBeamRoutine()
    {
     

        isLaserBeamActive = true;

        yield return new WaitForSeconds(2f);

        _laserBeamPrefab.SetActive(true);

        yield return new WaitForSeconds(5f);

        _laserBeamPrefab.SetActive(false);

        isLaserBeamActive = false;
        
    }

    IEnumerator Hurt()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        GetComponent<Renderer>().material.color = Color.white;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            hitPoints--;
            Destroy(other.gameObject);

            StartCoroutine(Hurt());

            if (_player != null)
            {
                _player.AddToScore(100);
            }

            if(hitPoints == 2)
            {
               
                rightEngineSprite.SetActive(true);
               
            }
            if(hitPoints == 1)
            {
                
                leftEngineSprite.SetActive(true);
            }

            if(hitPoints < 1)
            {
                _enemySpeed = 0;
                Instantiate(_ExplodeAnimPrefab, transform.position, Quaternion.identity);
                Destroy(GetComponent<LaserBeamEnemy>());
                Destroy(this.gameObject);
            }

            
        }
        //if hitpoints = 2 instantiate the "hurt" animation for left if 1 instantiate right. if 0 explode destroy
        //audio shit 

        if(other.tag == "HeatSeeker")
        {
            hitPoints--;
            //same seudo code 

        }
    }
}
