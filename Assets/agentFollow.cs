using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentFollow : MonoBehaviour
{
    public GameObject player;

    public NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("newPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
