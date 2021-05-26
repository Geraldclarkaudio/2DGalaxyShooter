using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Transform target;

   

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        
        StartCoroutine(MissileActive());
    }
       private void Update()
       {
          if(target != null)
           {
               transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
               LookAt2D(target.position);  
           }

           else
           {
            Destroy(this.gameObject);
               return;
           }

          
       }

       public void LookAt2D(Vector3 lookTarget)
       { 
           Vector3 yDirection = (lookTarget - transform.position).normalized;
           Vector3 xDirection = Quaternion.Euler(0, 0, 90) * yDirection;
           Vector3 zDirection = Vector3.forward;
           transform.rotation = Quaternion.LookRotation(zDirection, yDirection);
       }

    IEnumerator MissileActive()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
        }
    }
}