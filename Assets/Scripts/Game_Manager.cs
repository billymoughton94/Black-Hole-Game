using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EndScenario
{
    GAMEOVER,
    VICTORY
}

public class Game_Manager : MonoBehaviour



{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void endGame(EndScenario endScenario)
    {
        switch (endScenario)
        {
            case EndScenario.GAMEOVER:
                // TODO: DISPLAY GAME OVER UI MESSAGE
                Debug.Log("GAME OVER...");
                Time.timeScale = 0;
                break;
            case EndScenario.VICTORY:
                // TODO: DISPLAY VICTORY UI MESSAGE
                Debug.Log("YOU WIN!");
                Time.timeScale = 0;
                break;
        }
    }

    public static void tickOffItem(GameObject item)
    {

    }
}
