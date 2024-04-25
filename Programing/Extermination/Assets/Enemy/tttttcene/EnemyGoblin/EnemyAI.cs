using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float interval = 0.0f; // Интервал между выстрелами

    private float timer = 0f; // Таймер для отслеживания времени


    public List<Transform> patrolPoints;
    public PlayerMovement player;
    public float viewAngle;
    public float damage = 20;
    public float attackDistance = 1;
    public Animator animator;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
    private PlayerHealth _playerHealth;
    // Start is called before the first frame update
    private void Start()
    {
       

        InitComponentLinks();

        PickNewPatrolPoint();
    }

    // Update is called once per frame
    private void Update()
    {
        // Увеличиваем таймер
        timer += Time.deltaTime;
        // Если прошло достаточно времени для следующего выстрела
        if (timer >= interval)
        {
            interval = 3f;
            // Воспроизводим звук выстрела
            source.PlayOneShot(clip);

            // Сбрасываем таймер
            timer = 0f;
        }

        NoticePlayerUpdate();
        ChaseUpdate();
        PatrolUpdate();
        AttackUpdate();
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }

    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void PatrolUpdate()
    {
        if (!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                PickNewPatrolPoint();
            }
        }
       
    }

    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    
                    _isPlayerNoticed = true;
                }

            }

        }
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                animator.SetTrigger("attack");
                //_playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }
    }

    public void AttackDamage()
    {
        if (!_isPlayerNoticed) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > (_navMeshAgent.stoppingDistance + attackDistance))
        {
            Debug.Log("Нет урона!");
            return;
        }

        Debug.Log("Есть урон!");
        _playerHealth.DealDamage(damage);
    }
}

