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
    public int damagePoints;
    private AudioSource audioSource;
    public AudioClip[] audioQueue;

    bool alive;
    // Start is called before the first frame update
    void Start() {
        alive = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && alive)
        {
            Debug.Log("HIT DETECTED");
            player.GetComponent<Player_Controller>().takeDamage(damagePoints);
        }
    }

        

    public void takeDamage(float amount)
    {
        Debug.Log("Player shoots the monster");
        hitPoints -=amount;
        if (hitPoints <= 0)
        {
            
            monsterAnim.SetTrigger("HasDied");
            nav.isStopped = true;
            audioSource.Stop();
            StopAllCoroutines();
            alive = false;
            try
            {
                Destroy(transform.Find("Monster_Icon").gameObject);
            }
            catch(NullReferenceException n)
            {
                Debug.Log("Stop! He's already dead...");
            }
                
        }
        else
        {
            monsterAnim.SetTrigger("HasTakenDamage");
            
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
