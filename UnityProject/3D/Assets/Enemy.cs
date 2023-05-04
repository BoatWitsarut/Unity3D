using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string tagObj;

    private Transform targetObj;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private GameLogic gameLogic;

    //private bool dying = false;
    private float deathTime = -0.0f;
    private float secondUntilDestroy = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (targetObj == null) {
            targetObj = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (gameLogic == null) {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            gameLogic = gameController.GetComponent<GameLogic>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetObj.position;

        if (deathTime != -0.0f) {
            float destroyRatio = (Time.time - deathTime) / secondUntilDestroy;
            if (destroyRatio > 1.0f) Destroy(gameObject);
            else gameObject.transform.localScale = Vector3.one * (1 - destroyRatio);
        }

        //animator.SetBool("Idle", navMeshAgent.destination == null);
    }

    void OnTriggerEnter(Collider other){
        //Debug.Log($"OnTriggerEnter {other.gameObject}");
        if (other.gameObject.tag == tagObj && deathTime == -0.0f) {
            //Debug.Log($"other.gameObject == tagObj {other.gameObject.tag == tagObj}");
            // Destroy(other.gameObject);
            deathTime = Time.time;
            //dying = true;
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Death");
            gameLogic.GetDestroyedScore(1);
        }
    }
}
