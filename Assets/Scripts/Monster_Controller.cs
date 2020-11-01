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

    private bool nextToPlayer;

    IEnumerator attack;

    // Start is called before the first frame update
    void Start()
    {
        nextToPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        monsterAnim = GetComponent<Animator>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
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
            Quaternion targetDirection = Quaternion.LookRotation(direction);
            Quaternion deltaRotation = Quaternion.Slerp(transform.rotation, targetDirection, walkSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(new Vector3(rb.rotation.x, deltaRotation.eulerAngles.y, rb.rotation.z)));
        }
    }

    private void moveTowardsPlayer()
    {
        // ANIMATES MONSTER AND MOVES TOWARDS PLAYER IF IN AGGRO ZONE AND ABOVE ATTACK ZONE
        if ((distanceFromPlayer <= aggroDistance) && nextToPlayer == false)
        {
            rb.velocity = transform.forward * walkSpeed;
            if (monsterAnim.GetFloat("InputZ") != 1.0f)
                monsterAnim.SetFloat("InputZ", 1.0f);

        }

        // SWITCHES TO IDLE POSITION IF NEXT TO PLAYER OR OUTSIDE OF AGGRO DISTANCE
        if (distanceFromPlayer > aggroDistance || nextToPlayer == true)
        {
            if (monsterAnim.GetFloat("InputZ") != 0.0f)
                monsterAnim.SetFloat("InputZ", 0.0f);
        }

    }

    // BEGINS TO ATTACK WHEN PLAYER COMES WITHIN RANGE1
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && nextToPlayer == false)
        {
            nextToPlayer = true;
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
            nextToPlayer = false;
            StopCoroutine(attack);
            monsterAnim.SetBool("NextToPlayer", false);
        }
    }

    // ATTACKS PLAYER IF NEXT TO THEM
    IEnumerator attackPlayer()
    {
        while(true)
        { 
            if(monsterAnim.GetBool("NextToPlayer") != true)
            {
                monsterAnim.SetBool("NextToPlayer", true);
                yield return new WaitForSeconds(2);
                monsterAnim.SetBool("NextToPlayer", false);
            }
            yield return new WaitForFixedUpdate();

            // DOUBLE CHECKS THE ATTACK RANGE AFTER FIXED UPDATE BEFORE APPLYING HITPOINT DEDUCTION. IF HIT, GAME OVER
            if (nextToPlayer)
            {
                Debug.Log("THE MONSTER ATTACKS THE PLAYER");
                Game_Manager.endGame(EndScenario.GAMEOVER);
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
