﻿using UnityEngine;

public enum EndScenario {
    GAMEOVER,
    VICTORY
}

public class Game_Manager : MonoBehaviour {
    private static int itemCount; //total number of items collected
    private static int itemLimit = 3; // number of items to collect

    // END THE GAME AS A VICTORY OR GAME OVER. WIN IF ALL ITEMS COLLECTED. GAME OVER IF MONSTER/BLACK HOLE KILLS PLAYER
    public static void endGame(EndScenario endScenario) {
        switch (endScenario) {
            case EndScenario.GAMEOVER:
                // Display the Defeat panel
                GameObject.Find("UI").GetComponent<UI_Controller>().togglePanel("Defeat");
                Time.timeScale = 0;
                break;
            case EndScenario.VICTORY:
                // Display the Victory panel
                GameObject.Find("UI").GetComponent<UI_Controller>().togglePanel("Victory");
                Time.timeScale = 0;
                break;
        }
    }

    // UPDATE THE ITEMS LIST UI AND CREATE VICTORY END GAME IF ALL ITEMS COLLECTED
    public static void tickOffItem(string item) { 
        // Call the UI Controller to update the parts list
        GameObject.Find("UI").GetComponent<UI_Controller>().updatePartsList(item);
        itemCount++;
        Debug.Log(itemCount);
        Debug.Log(itemLimit);
        if (itemCount == itemLimit) {
            Debug.Log(itemLimit);
            endGame(EndScenario.VICTORY);
        }
            
    }
}
