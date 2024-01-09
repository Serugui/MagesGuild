using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 25;
    int rawDamage = Projectile.rawDamage;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collided with: " + other.name);

        if (other.tag == "Projectile")
        {
            Debug.Log("Hit enemy");
            Hit(rawDamage);
        }
    }
    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        if (hitPoints <= 0)
        {
            Invoke("SelfTerminate", 0f);
        }
    }

    void SelfTerminate()
    {
        Destroy(gameObject);
    }
}
