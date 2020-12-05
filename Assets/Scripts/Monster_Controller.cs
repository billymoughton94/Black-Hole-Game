using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Controller : MonoBehaviour {
    Animator monsterAnim;
    NavMeshAgent nav;
    private GameObject player;

    private float distanceFromPlayer;
    public float aggroDistance;
    public float attackDistance;

    private AudioSource audioSource;
    public AudioClip[] audioQueue;
    IEnumerator attack;

    // Start is called before the first frame update
    void Start() {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        monsterAnim = GetComponent<Animator>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playMonsterAudio());
    }

    private void Update()
    {
<<<<<<< HEAD
        chasePlayer();
        
=======
        monsterInteractions(); 
>>>>>>> parent of 275267b... Monster Animation and Black Hole Edits
    }

    private void chasePlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
<<<<<<< HEAD
=======
        bool nextToPlayer = distanceFromPlayer <= attackDistance;
        bool inAggroRange = distanceFromPlayer <= aggroDistance;
        AnimatorStateInfo state = monsterAnim.GetCurrentAnimatorStateInfo(0);
>>>>>>> parent of 275267b... Monster Animation and Black Hole Edits

        // IF PLAYER IS WITHIN AGRRO RANGE OF MONSTER AND NOT NEXT TO THE MONSTER, MONSTER STARTS TO CHASE PLAYER
        if (distanceFromPlayer <= aggroDistance && distanceFromPlayer > attackDistance)
        {
            nav.SetDestination(player.transform.position);
<<<<<<< HEAD
            if (monsterAnim.GetFloat("InputZ") != 0.25f)
                monsterAnim.SetFloat("InputZ", 0.25f);
        }

        // IF MONSTER IS TOO FAR FROM PLAYER OR RIGHT NEXT TO PLAYER, STOP CHASING
        if (distanceFromPlayer > aggroDistance || distanceFromPlayer <= attackDistance)
        {
            if (monsterAnim.GetFloat("InputZ") != 0.0f)
                monsterAnim.SetFloat("InputZ", 0.0f);
        }
=======
            if(state.IsName("Idle"))
                monsterAnim.SetBool("IsChasingPlayer", true); // START RUN ANIMATION
        }

        // IF MONSTER IS TOO FAR FROM PLAYER OR RIGHT NEXT TO PLAYER, STOP CHASING
        if ((!inAggroRange || nextToPlayer) && (state.IsName("Run")))
            monsterAnim.SetBool("IsChasingPlayer", false); // START IDLE ANIMATION

        if (nextToPlayer && !monsterAnim.GetBool("IsNextToPlayer")) // START ATTACK ANIMATION
            monsterAnim.SetBool("IsNextToPlayer", true);

        if (!nextToPlayer && monsterAnim.GetBool("IsNextToPlayer")) // END ATTACK ANIMATION
            monsterAnim.SetBool("IsNextToPlayer", false);
>>>>>>> parent of 275267b... Monster Animation and Black Hole Edits
    }

    // BEGINS TO ATTACK WHEN PLAYER COMES WITHIN RANGE
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("MONSTER IN ATTACK RANGE");

            attack = attackPlayer();
            StartCoroutine(attack);
        }
    }

<<<<<<< HEAD
    // STOPS ATTACKING AND CONTINUES TO PURSUE PLAYER WHEN OUTSIDE RANGE
    private void OnTriggerExit(Collider collision)
=======
    public void takeDamage()
>>>>>>> parent of 275267b... Monster Animation and Black Hole Edits
    {
        if (collision.tag == "Player")
        {
            Debug.Log("LEAVING MONSTER ATTACK RANGE");
            StopCoroutine(attack);
            monsterAnim.SetBool("NextToPlayer", false);
        }
    }



<<<<<<< HEAD
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
            yield return new WaitForSeconds(2.0f);
            // DOUBLE CHECKS THE ATTACK RANGE AFTER FIXED UPDATE BEFORE APPLYING HITPOINT DEDUCTION. IF HIT, GAME OVER
            Debug.Log("THE MONSTER ATTACKS THE PLAYER");
            Game_Manager.endGame(EndScenario.GAMEOVER);
            yield return new WaitForSeconds(5.0f);
=======

        if (hitPoints <= 0)
        {
            //TODO: DEAD ANIMATION & DELETE GAME OBJECT AFTER FEW SECONDS
            monsterAnim.SetTrigger("HasDied");
>>>>>>> parent of 275267b... Monster Animation and Black Hole Edits
        }
    }



    IEnumerator playMonsterAudio()
    {

        while (true)
        {
            for(int i = 0; i < audioQueue.Length; i++)
            {
                audioSource.clip = audioQueue[i];
                audioSource.Play();

                while (audioSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }
}
