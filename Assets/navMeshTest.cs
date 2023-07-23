using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshTest : MonoBehaviour
{
    public NavMeshAgent agent;

    void Start()
    {
        agent.angularSpeed = 1000;
        agent.stoppingDistance = 0;

        //InvokeRepeating("TargetNewPos", 0, 0.07f);

        //Ray ray = new Ray(transform.position, gameObject.GetComponent<NewPlayerMovement>().desiredVector);//new Vector3(velocity.x, velocity.y, transform.position.z));
        //RaycastHit hit;
        //agent.speed = gameObject.GetComponent<NewPlayerMovement>().currentMoveSpeed;

        //if (Physics.Raycast(ray, out hit))
        //{
        //    agent.SetDestination(hit.point);
        //}
    }

    void FixedUpdate()
    {
        agent.speed = gameObject.GetComponent<NewPlayerMovement>().currentMoveSpeed;
        agent.SetDestination((gameObject.GetComponent<Attack>().currentTarget).transform.position);
    }
}
