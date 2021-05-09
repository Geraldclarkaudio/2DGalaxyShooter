using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private GameObject explosionPrefab;

    private AudioSource _enemyAudioSource;

    private Animator cameraAnimator;

    private Transform target;
    private Transform target2;

    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target2 = GameObject.FindGameObjectWithTag("Target2").GetComponent<Transform>();

        _enemyAudioSource = GetComponent<AudioSource>();
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        if (_enemyAudioSource == null)
        {
            Debug.LogError("AudioSource On Enemy is Null");
        }

        if(cameraAnimator == null)
        {
            Debug.LogError("Camera Animator is Null on Follower Eenemy");
        }

    }

    private void Update()
    {
        if(transform.position.y >= target.transform.position.y)
        {
            if (target != null)
            {

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                LookAt2D(target.position);
            }
        }
        else if(transform.position.y < target.transform.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
            LookAt2D2(target2.position);
            
        }

        if(transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }


    }// Move Towards Player Until Y position is < Player's

    public void LookAt2D(Vector3 lookTarget)
    {

        Vector3 xDirection = (lookTarget - transform.position).normalized;
        Vector3 yDirection = Quaternion.Euler(0, 0, 0) * xDirection;
        Vector3 zDirection = Vector3.forward;

        transform.rotation = Quaternion.LookRotation(zDirection, -yDirection);
    }// Look At the target

    public void LookAt2D2(Vector3 lookTarget2)
    {

        Vector3 xDirection = (lookTarget2 - transform.position).normalized;
        Vector3 yDirection = Quaternion.Euler(0, 0, 0) * xDirection;
        Vector3 zDirection = Vector3.forward;

        transform.rotation = Quaternion.LookRotation(zDirection, -yDirection);
    }// Look At the target

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            _enemyAudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(50);
            }
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            cameraAnimator.SetTrigger("CameraShake");


            _enemyAudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }

        if (other.tag == "HeatSeeker")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(20);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            cameraAnimator.SetTrigger("CameraShake");
            _enemyAudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            gameObject.tag = "Untagged";
            Destroy(this.gameObject);

        }
    }

}
