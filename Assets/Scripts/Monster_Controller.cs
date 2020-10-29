using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Jobs;

public class Monster_Controller : MonoBehaviour
{
    Rigidbody rb;
    Animator monsterAnim;

    private GameObject player;
    
    public float walkSpeed;
    private float distanceFromPlayer;
    public float aggroDistance;
    public float attackDistance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        monsterAnim = GetComponent<Animator>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        StartCoroutine(attackPlayer());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        rotateTowardsPlayer();
        moveTowardsPlayer();
    }

    private void Update()
    {
    }

    // ROTATE TOWARDS PLAYER IF WITHIN AGGRO DISTANCE
    private void rotateTowardsPlayer()
    {
        if(distanceFromPlayer <= aggroDistance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion targetDirection = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion deltaRotation = Quaternion.Slerp(transform.rotation, targetDirection, walkSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(deltaRotation);
        }
    }


    private void moveTowardsPlayer()
    {
        // ANIMATES MONSTER AND MOVES TOWARDS PLAYER IF IN AGGRO ZONE AND ABOVE ATTACK ZONE
        if (distanceFromPlayer <= aggroDistance && distanceFromPlayer > attackDistance)
        {
            monsterAnim.SetFloat("InputZ", 1.0f);
            rb.velocity = transform.forward * walkSpeed;
        }

        // ATTACK PLAYER IF WITHIN ATTACK ZONE OR STOPS MOVING IF OUTSIDE AGGRO DISTANCE
        if(distanceFromPlayer <= attackDistance || distanceFromPlayer > aggroDistance)
        {
            monsterAnim.SetFloat("InputZ", 0.0f);
        }    
    }


    // ATTACKS PLAYER IF NEXT TO THEM
    IEnumerator attackPlayer()
    {
        while(true)
        { 
            if (distanceFromPlayer < attackDistance)
            {
                if(monsterAnim.GetBool("NextToPlayer") != true)
                {
                    monsterAnim.SetBool("NextToPlayer", true);
                    yield return new WaitForSeconds(2);
                    monsterAnim.SetBool("NextToPlayer", false);
                }

                Debug.Log("THE MONSTER ATTACKS THE PLAYER");
            }

            else
            {
                if (monsterAnim.GetBool("NextToPlayer") != false)
                    monsterAnim.SetBool("NextToPlayer", false);
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
