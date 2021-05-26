using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    private Animator _anim;
    private SpawnManager spawnManager;
    private GameObject powerUp;

    [SerializeField]
    private GameObject enemyShield;

    private bool isShieldActive = false;

    private RaycastHit2D hit;

    private Animator cameraAnimator;

    [SerializeField]
    private float _enemySpeed = 4;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    private Player _player;

    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

    private float _canFireAtPowerup = -1f;
    private float _powerUpFireRate = 3f;



    private AudioSource _enemyAudioSource;
    void Start()
    {
        isShieldActive = true;
        powerUp = GameObject.FindGameObjectWithTag("Powerup");
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAudioSource = GetComponent<AudioSource>();
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();//
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        hit = Physics2D.Raycast(transform.position + new Vector3(0, -2, 0), Vector2.down * 10);

        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        if (_enemyAudioSource == null)
        {
            Debug.LogError("AudioSource On Enemy is Null");
        }

        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL on Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
       CalculateMovement();
        EnemyFire();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * 10, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -2, 0), Vector2.down * 10);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Powerup")
            {
                if (Time.time > _canFireAtPowerup)
                {
                    _powerUpFireRate = Random.Range(3.0f, 7.0f);
                    _canFireAtPowerup = Time.time + _fireRate;
                    GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
                    Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i].AssignEnemyLaser();
                    }
                }

            }
        }

    }


    public void EnemyFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {

        transform.Translate(new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -4f)
        {
            float randomX = Random.Range(-9.8f, 9.8f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isShieldActive == true)
        {
            Player player = other.transform.GetComponent<Player>();

            enemyShield.SetActive(false);
            isShieldActive = false;

            if(other.tag == "Laser")
            {
                Destroy(other.gameObject);
            }
            if(other.tag == "Player")
            {
                player.Damage();
            }

            if(other.tag == "HeatSeeker")
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (other.tag == "Player")
            {

                Player player = other.transform.GetComponent<Player>();

                if (player != null)
                {
                    player.Damage();
                }
                if(isShieldActive == true )
                {
                    player.Damage();
                }


                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _enemyAudioSource.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Enemy>());
                Destroy(this.gameObject, 2.5f);
            }

            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);

                if (_player != null)
                {
                    _player.AddToScore(10);
                }

                cameraAnimator.SetTrigger("CameraShake");
                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _enemyAudioSource.Play();

                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Enemy>());
                Destroy(this.gameObject, 2.5f);
            }

            if (other.tag == "HeatSeeker")
            {
                Destroy(other.gameObject);

                if (_player != null)
                {
                    _player.AddToScore(75);
                }
                cameraAnimator.SetTrigger("CameraShake");
                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _enemyAudioSource.Play();

                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Enemy>());
                gameObject.tag = "Untagged";
                Destroy(this.gameObject, 2.5f);

            }
        }
    }
       
}

