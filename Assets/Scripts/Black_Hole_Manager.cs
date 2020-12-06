using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Black_Hole_Manager : MonoBehaviour {
    // amount of time (in seconds) needed for Black Hole to reach its maximum threshold
    public int duration;
    public float pullForce;

    // starting size vs maximum size of Black Hole
    private Vector3 startScale;
    private Vector3 targetScale;

    bool maximumMass;

    GameObject player;

     IEnumerator BH_GROW;
     IEnumerator BH_PULL;



    // Start is called before the first frame update
    void Start() {
        maximumMass = false;
        startScale = gameObject.transform.localScale;
        const float MAX_SIZE = 1500f;
        targetScale = new Vector3(MAX_SIZE, MAX_SIZE, MAX_SIZE);
        player = GameObject.Find("Player");
        BH_GROW = increaseMass();
        BH_PULL = pullPlayer();
        StartCoroutine(BH_GROW);
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if(collision.collider.tag == "Player") {
            Game_Manager.endGame(EndScenario.GAMEOVER);
        }
    }

    IEnumerator increaseMass()
    {
        float timeElapsed = 0;

        // whilst magnitude is less than desired magnitude, expand the Black Hole size per frame
        while (gameObject.transform.localScale.magnitude < targetScale.magnitude)
        {
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        maximumMass = true;
        Debug.Log("Maxmimum mass reached!!!!");
        StartCoroutine(BH_PULL);
        StopCoroutine(BH_GROW);
    }


    IEnumerator pullPlayer()
    {
        Debug.Log("NOW PULLING PLAYER");

        //LOCK THE PLAYER MOVEMENT (CAMERA CAN STILL MOVE)
        player.GetComponent<Player_Controller>().movementLock = true;

        while (maximumMass)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position, pullForce * Time.deltaTime);
            yield return null;
        }
    }
}
