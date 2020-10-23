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
    private Boolean nextToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        monsterAnim = GetComponent<Animator>();
        StartCoroutine(attackPlayer());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTowardsPlayer();
    }

    private void Update()
    {
    }



    // moves towards the player's position, stops when he gets within 10 units to player and then attacks
    private void moveTowardsPlayer()
    {
        Quaternion targetDirection = Quaternion.LookRotation(player.transform.position - transform.position);
        Quaternion deltaRotation = Quaternion.Slerp(transform.rotation, targetDirection, walkSpeed * Time.deltaTime);


        //////////////////////////// TODO: FIND A BETTER WAY OF ROTATING SO THAT MONSTER DOES NOT ROTATE ON X AXIS (AKA TAKE OFF) ///////////////////////////
        //Quaternion deltaRotation = Quaternion.LookRotation(-Vector3.forward * targetDirection.z, -Vector3.up * targetDirection.y);


        rb.MoveRotation(deltaRotation);

        if (Vector3.Distance(transform.position, player.transform.position) > 10)
          {
            nextToPlayer = false;
            rb.velocity = transform.forward * walkSpeed;
            monsterAnim.SetBool("NextToPlayer", false);
            monsterAnim.SetFloat("InputZ", 1.0f);
          }

          else
          {
              nextToPlayer = true;
              monsterAnim.SetFloat("InputZ", 0.0f);
          } 
    }

    // if the monster is next to the player, attack them
    IEnumerator attackPlayer()
    {
        while(true)
        { 
            if (nextToPlayer)
            {
                Debug.Log("THE MONSTER ATTACKS THE PLAYER");
                monsterAnim.Play("Attack(1)");
                // TODO: REDUCE THE PLAYER'S HEALTH UNTIL THEY DIE
            }

           yield return new WaitForSeconds(5.0f);

        }


    }
}
