using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Header("Player and Enemy Objects")]
    private Transform player;
    private NavMeshAgent enemyAgent;
    public GameObject enemyObj;
    public LayerMask isGround, isPlayer;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerinSightRange;
    public bool playerinAttackRange;

    [Header("Jumpscare")]
    public AudioSource jumpscareSound;
    public GameObject scareCamera;
    private bool isJumpscare;



    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Check sight and attack range
        playerinSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        
        if(!playerinSightRange && !playerinAttackRange) Patrolling();
        if(playerinSightRange && !playerinAttackRange) Chase();
        if (playerinSightRange && playerinAttackRange && !isJumpscare)
        {
            isJumpscare = true;
            Jumpscare();
        }

    }

    private void Patrolling()
    {
        enemyObj.GetComponent<Animator>().Play("Walking");
        enemyAgent.speed = 2f;
        
        if (!walkPointSet) SearchWalkPoint();
        else
        {
            enemyAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //If reached walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround)) walkPointSet = true;
    }
    
    private void Chase()
    {
        enemyAgent.SetDestination(player.position);
        enemyAgent.speed = 4f;
        enemyObj.GetComponent<Animator>().Play("Fast Run");
    }
    
    private void Jumpscare()
    {
        //enemy stops when reached
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);
        StartCoroutine(JumpscareCoroutine());
    }

    private IEnumerator JumpscareCoroutine()
    {
        jumpscareSound.Play();
        scareCamera.SetActive(true);
        enemyObj.SetActive(false);
        yield return new WaitForSeconds(1.09f);
        scareCamera.SetActive(false);
        enemyObj.SetActive(true);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
