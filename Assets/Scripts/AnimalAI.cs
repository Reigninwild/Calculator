using UnityEngine;
using System.Collections;

public class AnimalAI : MonoBehaviour
{
    private const float RANGE_X = 20;
    private const float RANGE_Z = 20;

    [Header("Дистанция обнаружения игрока")]
    public float agrDistance = 20;

    [Header("Скорость передвижения")]
    public float runSpeed = 10;
    public float walkSpeed = 3.5f;

    private NavMeshAgent navAgent;
    //Origin position, anchor
    private Vector3 originPosition;
    //Position rate
    [Header("Время остановок")]
    public float positionRate = 5;
    private float nextPosition = 0.0F;
    //Animation
    private Animator animator;
    
    private bool agr = false;
    private Transform player;

    void Start()
    {
        originPosition = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (!agr)
        {
            if (!navAgent.hasPath)
            {
                if (Time.time > nextPosition)
                {
                    GeneratePosition();
                }
                animator.SetBool("walk", false);
            }
            else
            {
                nextPosition = Time.time + positionRate;
                animator.SetBool("walk", true);
            }
            animator.SetBool("run", false);
            navAgent.speed = walkSpeed;
        }
        else
        {
            GenerateAgrPosition();
            //avAgent.SetDestination(player.position);
            navAgent.speed = runSpeed;
            animator.SetBool("run", true);
            animator.SetBool("walk", false);
        }

        if(Vector3.Distance(transform.position, player.position) > agrDistance)
        {
            agr = false;
        }
        else
        {
            agr = true;
        }
    }

    public void GeneratePosition()
    {
        navAgent.SetDestination(originPosition + new Vector3(Random.Range(0, RANGE_X), 0, Random.Range(0, RANGE_Z)));
    }

    public void GenerateAgrPosition()
    {
        navAgent.SetDestination(transform.position + new Vector3(RANGE_X, 0, RANGE_Z));
    }
}
