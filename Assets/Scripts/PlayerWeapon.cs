using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    Transform firingPoint;

    [SerializeField]
    GameObject attack1Prefab;

    [SerializeField]
    GameObject attack2Prefab;

    [SerializeField]
    float firingSpeed;

    private float lastTimeShot = 0;

    private GameObject currentAttackPrefab;

    public static PlayerWeapon Instance;

    private void Awake()
    {
        Instance = GetComponent<PlayerWeapon>();
    }

    void Start()
    {
        SetCurrentAttack(attack1Prefab);
    }

    public void SwitchToAttack1()
    {
        SetCurrentAttack(attack1Prefab);
    }

    public void SwitchToAttack2()
    {
        SetCurrentAttack(attack2Prefab);
    }

    private void SetCurrentAttack(GameObject attackPrefab)
    {
        currentAttackPrefab = attackPrefab;
    }

    public void Shoot()
    {
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;

            Instantiate(currentAttackPrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
