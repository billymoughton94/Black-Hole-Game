using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Controller : MonoBehaviour {
    Rigidbody rb;
    Animator monsterAnim;
    NavMeshAgent nav;

    private GameObject player;
    
    private float distanceFromPlayer;
    public float aggroDistance;

    //private bool nextToPlayer;

    IEnumerator attack;

    // Start is called before the first frame update
    void Start() {
        nav = GetComponent<NavMeshAgent>();
        //nextToPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        monsterAnim = GetComponent<Animator>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Update()
    {
        chasePlayer();
    }

    private void chasePlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        // IF PLAYER IS WITHIN AGRRO RANGE OF MONSTER AND NOT NEXT TO THE MONSTER, MONSTER STARTS TO CHASE PLAYER
        if (distanceFromPlayer <= aggroDistance)
        {
            nav.SetDestination(player.transform.position);
            if (monsterAnim.GetFloat("InputZ") != 1.0f)
                monsterAnim.SetFloat("InputZ", 1.0f);
        }


        // IF MONSTER IS TOO FAR FROM PLAYER OR RIGHT NEXT TO PLAYER, STOP CHASING
        if (distanceFromPlayer > aggroDistance || nav.isStopped)
        {
            //nav.isStopped = true;
            if (monsterAnim.GetFloat("InputZ") != 0.0f)
                monsterAnim.SetFloat("InputZ", 0.0f);
        }
    }

    // BEGINS TO ATTACK WHEN PLAYER COMES WITHIN RANGE
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            //nextToPlayer = true;
            attack = attackPlayer();
            StartCoroutine(attack);
        }
    }

    // STOPS ATTACKING AND CONTINUES TO PURSUE PLAYER WHEN OUTSIDE RANGE
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("LEAVING MONSTER ATTACK RANGE");
            //nextToPlayer = false;
            StopCoroutine(attack);
            monsterAnim.SetBool("NextToPlayer", false);
        }
    }

    // ATTACKS PLAYER IF NEXT TO THEM
    IEnumerator attackPlayer()
    {
        while (true)
        {
            if (monsterAnim.GetBool("NextToPlayer") != true)
            {
                monsterAnim.SetBool("NextToPlayer", true);
                yield return new WaitForSeconds(2);
                monsterAnim.SetBool("NextToPlayer", false);
            }
            yield return new WaitForFixedUpdate();

            // DOUBLE CHECKS THE ATTACK RANGE AFTER FIXED UPDATE BEFORE APPLYING HITPOINT DEDUCTION. IF HIT, GAME OVER
            //if (nextToPlayer)
            //{
               // Debug.Log("THE MONSTER ATTACKS THE PLAYER");
                Game_Manager.endGame(EndScenario.GAMEOVER);
           // }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
