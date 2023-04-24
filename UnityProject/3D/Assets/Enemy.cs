using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string tagObj;

    Transform targetObj;
    Animator animator;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(targetObj == null) {
            targetObj = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetObj.position;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject == targetObj){
            Destroy(other.gameObject);
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Death");
        }
    }
}
