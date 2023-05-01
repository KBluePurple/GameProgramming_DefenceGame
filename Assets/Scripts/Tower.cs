using System.Collections;
using System.Collections.Generic;
using Chronos.Example;
using UnityEngine;

public class Tower : TimeBehaviour
{
    [SerializeField]
    private float shootTimerMax = 0.3f;
    private float shootTimer = 0f;

    private Enemy targetEnemy;
    private float lookForTargetTimer = 0f;
    private float lookForTargetTimerMax = 0.2f;

    private Vector3 projectileSpawnPosition;

    private void Awake()
    {
        projectileSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }

    private void HandleTargeting()
    {
        lookForTargetTimer -= time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        shootTimer -= time.deltaTime;
        if(shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            if (targetEnemy != null)
            {
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }

}