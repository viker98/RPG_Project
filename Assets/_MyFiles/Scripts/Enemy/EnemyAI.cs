using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoints;
    [SerializeField] private Transform targetPos;

    private NavMeshAgent enemy_NavMeshAgent;
    private Animator enemy_Animator;
    private int waypointIndex;
    private Vector3 prevPosition;
    [SerializeField] private float curSpeed;
    [SerializeField] private float FollowRange = 10f;

    void Start()
    {
        /*    GameObject playerHolder = GameManager.m_Instance.GetPlayer();
            if (playerHolder != null)
            {
                targetPos = playerHolder.transform;
            }
            else
            {
                Debug.LogError("Plyaer not Found!");
            }
        */
        StartCoroutine(FindPlayer());
        if (Waypoints.Length <= 0)
        {
            Debug.LogError("NO waypoints found");
        }
        prevPosition = transform.position;
        waypointIndex = 0;

        enemy_NavMeshAgent = GetComponent<NavMeshAgent>();
        enemy_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curMove = transform.position - prevPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        prevPosition = transform.position;
        enemy_Animator.SetFloat("Speed", curSpeed);

        float targetDistance = Vector3.Distance(transform.position, targetPos.position);
        if(targetDistance <= FollowRange)
        {
            enemy_NavMeshAgent.destination = targetPos.position;
        }
        else
        {
            if(Waypoints.Length > 0)
            {
                float waypointdistance = Vector3.Distance(transform.position, Waypoints[waypointIndex].transform.position);
                enemy_NavMeshAgent.destination = Waypoints[waypointIndex].transform.position;
                if(waypointdistance < 1f)
                {
                    waypointIndex++;
                    if(waypointIndex >= Waypoints.Length)
                    {
                        waypointIndex = 0;
                    }
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, FollowRange);
    }
    private IEnumerator FindPlayer()
    {
        yield return new WaitForEndOfFrame();
        GameObject playerHolder = GameManager.m_Instance.GetPlayer();
        if (playerHolder != null)
        {
            targetPos = playerHolder.transform;
        }
        else
        {
            Debug.LogError("Plyaer not Found!");
        }
    }
}
