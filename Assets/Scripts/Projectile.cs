using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    private ParticleSystem pSys;
    public int rawDamage = 10;
    

    private void Start()
    {
        pSys = GetComponentInChildren<ParticleSystem>();  
        pSys.Play();
        Destroy(gameObject, 3.0f);

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            other.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
