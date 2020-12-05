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
    public float hitPoints = 50f;

    private AudioSource audioSource;
    public AudioClip[] audioQueue;

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
        monsterInteractions();
      //  if (Input.GetKeyDown("h"))
        //    takeDamage();
    }

    private void monsterInteractions()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool nextToPlayer = distanceFromPlayer <= attackDistance;
        bool inAggroRange = distanceFromPlayer <= aggroDistance;
        AnimatorStateInfo state = monsterAnim.GetCurrentAnimatorStateInfo(0);

        // IF PLAYER IS WITHIN AGRRO RANGE OF MONSTER AND NOT NEXT TO THE MONSTER, MONSTER STARTS TO CHASE PLAYER
        if (inAggroRange && !nextToPlayer)
        {
            nav.SetDestination(player.transform.position);
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
    }
    
    // WHEN A HIT BY THE MONSTER IS DETECTED
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT DETECTED");
            //TODO: DEPLETE PLAYER'S HEALTH BY 1 HITPOINT (0 HP = Game_Manager.endGame(EndScenario.GAMEOVER))
        }
    }

    public void takeDamage(float amount)
    {
        Debug.Log("I'm here");
        hitPoints -=amount;
        if (hitPoints > 0)
        {
            monsterAnim.SetTrigger("HasTakenDamage");
        }


        if (hitPoints <= 0)
        {
            //TODO: DEAD ANIMATION & DELETE GAME OBJECT AFTER FEW SECONDS
            monsterAnim.SetTrigger("HasDied");
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
