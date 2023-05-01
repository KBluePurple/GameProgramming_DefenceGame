using Chronos.Example;
using UnityEngine;

namespace DefaultNamespace
{
    public class Unit : TimeBehaviour
    {
        [SerializeField] private UnitData unitData;
        private Rigidbody2D _rigidbody2d;
        private Transform _target;
        private float _timer;

        private void Awake()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _timer = Random.Range(0f, 1f);
        }

        private void Update()
        {
            if (_target != null)
            {
                var moveDir = (_target.position - transform.position).normalized;
                const float moveSpeed = 6f;
                _rigidbody2d.velocity = moveDir * (moveSpeed * time.timeScale);
            }

            if (_timer >= 1)
            {
                _timer -= 1;
                FindTarget();
                AttackTarget();
            }

            _timer += Time.deltaTime;
        }

        private void FindTarget()
        {
            var targets = FindObjectsOfType<Enemy>();
            if (targets.Length == 0) return;
            var closestTarget = targets[0];
            var closestDistance = Vector3.Distance(transform.position, closestTarget.transform.position);
            foreach (var target in targets)
            {
                var distance = Vector3.Distance(transform.position, target.transform.position);
                if (!(distance < closestDistance)) continue;
                closestTarget = target;
                closestDistance = distance;
            }

            _target = closestTarget.transform;
        }

        private void AttackTarget()
        {
            if (_target != null)
                if (Vector2.Distance(transform.position, _target.position) < 3f)
                    _target.GetComponent<HealthSystem>().Damage(unitData.damage);
        }
    }
}