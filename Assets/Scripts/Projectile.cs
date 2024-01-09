using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    private ParticleSystem pSys;
    [SerializeField]
    public static int rawDamage = 10;
    

    private void Start()
    {
        pSys = GetComponentInChildren<ParticleSystem>();  
        pSys.Play();
        Destroy(gameObject, 3.0f);

    }

    private void CollisionEnter(Collider col)
    {
        Debug.Log("Collided");

        if (col.gameObject.tag == "Enemy")
        {
            // Apply damage to the enemy
            col.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);

            // Destroy the bullet on trigger
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void OnParticleCollision(GameObject other)
    //{
        //Debug.Log("Particle collided with: " + other.name);

        //if (other.tag == "Enemy")
        //{
            //Debug.Log("Hit enemy");
            //other.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
       // }
    //}
}
