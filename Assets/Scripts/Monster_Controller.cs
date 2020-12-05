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
        chasePlayer();
        
    }

    private void chasePlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        // IF PLAYER IS WITHIN AGRRO RANGE OF MONSTER AND NOT NEXT TO THE MONSTER, MONSTER STARTS TO CHASE PLAYER
        if (distanceFromPlayer <= aggroDistance && distanceFromPlayer > attackDistance)
        {
            nav.SetDestination(player.transform.position);
            if (monsterAnim.GetFloat("InputZ") != 0.25f)
                monsterAnim.SetFloat("InputZ", 0.25f);
        }

        // IF MONSTER IS TOO FAR FROM PLAYER OR RIGHT NEXT TO PLAYER, STOP CHASING
        if (distanceFromPlayer > aggroDistance || distanceFromPlayer <= attackDistance)
        {
            if (monsterAnim.GetFloat("InputZ") != 0.0f)
                monsterAnim.SetFloat("InputZ", 0.0f);
        }
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

    // STOPS ATTACKING AND CONTINUES TO PURSUE PLAYER WHEN OUTSIDE RANGE
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("LEAVING MONSTER ATTACK RANGE");
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
            yield return new WaitForSeconds(2.0f);
            // DOUBLE CHECKS THE ATTACK RANGE AFTER FIXED UPDATE BEFORE APPLYING HITPOINT DEDUCTION. IF HIT, GAME OVER
            Debug.Log("THE MONSTER ATTACKS THE PLAYER");
            Game_Manager.endGame(EndScenario.GAMEOVER);
            yield return new WaitForSeconds(5.0f);
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
