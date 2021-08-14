using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected float hp = 300f;
    protected int positionNum = 10;
    protected Vector3[] movePositions;
    protected float moveDistance = 50;
    protected int currPositionPivot = 0;
    protected float speed = 0;
    protected Animator animator;
    protected GameObject[] player;
    protected float attackTime = 0;
    protected bool isDead = false;


    //Animator Parameter
    protected string paraDie = "Die";
    protected string paraIdle = "Idle";
    protected string paraAttack = "Attack";
    protected string paraMove = "Move";


    protected virtual void Awake()
    {
        speed = 5;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        movePositions = new Vector3[positionNum];
        animator = GetComponent<Animator>();
        makePath();
        player = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (hp > 0)
        {
            MoveToPosition();
        }
        else if (hp <= 0 && isDead == false)
        {
            isDead = true;
            agent.speed = 0;
            animator.SetTrigger(paraDie);
            Destroy(gameObject, 5.0f);
        }
    }

    protected Vector3 GetRandomPosOnNavmesh(Vector3 center, float distance)
    {
        Vector3 randomPos;

        NavMeshHit navMeshHit;
        while (true)
        {
            randomPos = Random.insideUnitSphere * distance + center;
            if (NavMesh.SamplePosition(randomPos, out navMeshHit, distance, NavMesh.AllAreas))
            {
                break;
            }
        }
        return navMeshHit.position;
    }

    protected void makePath()
    {
        for (int i = 0; i < positionNum; ++i)
        {
            movePositions[i] = GetRandomPosOnNavmesh(transform.position, moveDistance);
        }
    }

    protected void MoveToPosition()
    {

        for (int i = 0; i < player.Length; ++i)
        {

            if (Vector3.Distance(player[i].GetComponent<Transform>().position, transform.position) < 3
                 && Time.time - attackTime > 3f)
            {
                animator.SetTrigger(paraAttack);
                attackTime = Time.time;
            }
            else if (Vector3.Distance(player[i].GetComponent<Transform>().position, transform.position) < 30
                )
            {
                agent.SetDestination(player[i].GetComponent<Transform>().position);

            }
            else
            {
                agent.SetDestination(movePositions[currPositionPivot]);
            }
        }

       

        if (Vector3.Distance(movePositions[currPositionPivot], transform.position) < 0.5)
        {
            currPositionPivot = ++currPositionPivot % positionNum;
            if(currPositionPivot % 3 == 0)
            {
                animator.SetTrigger(paraIdle);
            }
        }

        AnimatorStateInfo currAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(currAnimatorStateInfo.IsName(paraMove))
        {
            agent.speed = speed;
        }
        else
        {
            agent.speed = 0;
            Debug.Log("Speed Is 0");
        }
    }



}
