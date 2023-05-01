using Chronos.Example;
using UnityEngine;

public class ArrowProjectile : TimeBehaviour
{
    private Vector3 lastMoveDir;
    private Enemy targetEnemy;
    private float timeToDie = 2f;

    private void Update()
    {
        Vector3 moveDir;

        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        var moveSpeed = 20f;
        transform.position += moveDir * (time.deltaTime * moveSpeed);

        timeToDie -= time.deltaTime;
        if (timeToDie <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            var damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }

    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        var pfArrowProjectile = Instantiate(GameAssets.Instance.pfArrowProjectile, position, Quaternion.identity);
        var arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        var arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }

    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }
}