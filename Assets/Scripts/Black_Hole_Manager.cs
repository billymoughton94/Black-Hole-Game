using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Black_Hole_Manager : MonoBehaviour
{
    // amount of time (in seconds) needed for Black Hole to reach its maximum threshold
    public int duration;

    // starting size vs maximum size of Black Hole
    private Vector3 startScale;
    private Vector3 targetScale;

    // Start is called before the first frame update
    void Start() {
        startScale = gameObject.transform.localScale;
        targetScale = new Vector3(2000.0f, 2000.0f, 2000.0f);
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(increaseMass());
    }
    
    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if(collision.collider.tag == "Player") {
            Game_Manager.endGame(EndScenario.GAMEOVER);
        }
    }

    IEnumerator increaseMass() {
        float timeElapsed = 0;

        // whilst timeElapsed <= duration, expand the Black Hole size per frame
        while (timeElapsed <= duration)
        {     
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
