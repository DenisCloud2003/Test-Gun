using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBehaviour : MonoBehaviour
{
    float randomMovementRange = 10f;

    private Transform player;
    private Animator animator;   
    public EnemyData enemyData;

    private float attackDistance = 10f;
    private float listenDistance = 15f;

    private Vector3 targetPosition;

    private void Start()
    {
        if (!animator)
            animator = GetComponent<Animator>();      
        if (!player)
            player = GameObject.Find("PlayerController").transform;

        targetPosition = transform.position;
    }

    private void Update()
    {
        AIBehaviour();
    }

    private void AIBehaviour()
    {
        if (Vector3.Distance(player.position, transform.position) <= listenDistance) // && playerShoot
            AttackBehaviour();
        else
        {
            if (Vector3.Distance(player.position, transform.position) <= attackDistance)
            {
                AttackBehaviour();
            }
            else Patrol();
        }               
    }



    #region Patrol Behaviour

    public void Patrol()
    {
        animator.SetBool("IsRun", false);
        animator.SetBool("IsWalk", true);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = transform.position + new Vector3(UnityEngine.Random.insideUnitCircle.x, 0, UnityEngine.Random.insideUnitCircle.y) * randomMovementRange;
        }

        Vector3 direction = targetPosition - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            direction = Vector3.Reflect(direction, hit.normal);
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = Quaternion.Euler(new Vector3(0f, lookRotation.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemyData.walkSpeed * 4);

        transform.position = Vector3.Lerp(transform.position, targetPosition, enemyData.walkSpeed * Time.deltaTime);

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, targetPosition);
    }

    #endregion

    #region Attack Behaviour

    public void AttackBehaviour()
    {
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", true);

        
        transform.position = Vector3.Lerp(transform.position, player.position, enemyData.runSpeed * Time.deltaTime);

        Vector3 direction = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = Quaternion.Euler(new Vector3(0f, lookRotation.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemyData.runSpeed * 4);

        if (Vector3.Distance(transform.position, player.position) <= enemyData.attackRange) // && !playerDmg.isDead
        {
            Attack();
        }
    }

    

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    #endregion
}
