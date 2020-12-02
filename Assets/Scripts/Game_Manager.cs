using UnityEngine;

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
                GameObject.Find("UI").GetComponent<UI_Controller>().showDefeatPanel();
                unlockCursor();
                Time.timeScale = 0;
                break;
            case EndScenario.VICTORY:
                // Display the Victory panel
                GameObject.Find("UI").GetComponent<UI_Controller>().showVictoryPanel();
                unlockCursor();
                Time.timeScale = 0;
                break;
        }
    }

    // UPDATE THE ITEMS LIST UI AND CREATE VICTORY END GAME IF ALL ITEMS COLLECTED
    public static void tickOffItem(GameObject item) { 
        // Call the UI Controller to update the parts list
        GameObject.Find("UI").GetComponent<UI_Controller>().updatePartsList(item);
        itemCount++;
        if (itemCount == itemLimit) {
            Debug.Log(itemLimit);
            endGame(EndScenario.VICTORY);
        }
    }
    
    // Unlock the cursor to allow menu use
    private static void unlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
