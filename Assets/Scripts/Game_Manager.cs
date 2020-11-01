using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public enum EndScenario
{
    GAMEOVER,
    VICTORY
}

public class Game_Manager : MonoBehaviour

{
    private static int itemCount; //total number of items collected
    private static int itemLimit; // number of items to collect

    public static TextMeshProUGUI partList; // reference for the Parts checklist

    private static string antennaText = "Antenna: 0/1";
    private static string shipBodyText = "Ship Body: 0/1";
    private static string fuelContainersText = "Fuel Containers: 0/1";

    // Start is called before the first frame update
    void Start()
    {
        itemLimit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // END THE GAME AS A VICTORY OR GAME OVER. WIN IF ALL ITEMS COLLECTED. GAME OVER IF MONSTER/BLACK HOLE KILLS PLAYER
    public static void endGame(EndScenario endScenario)
    {
        switch (endScenario)
        {
            case EndScenario.GAMEOVER:
                ////////////////////////////////// TODO: DISPLAY GAME OVER UI MESSAGE //////////////////////////////////
                Debug.Log("GAME OVER...");
                Time.timeScale = 0;
                break;
            case EndScenario.VICTORY:
                ////////////////////////////////// TODO: DISPLAY VICTORY UI MESSAGE //////////////////////////////////
                Debug.Log("YOU WIN!");
                Time.timeScale = 0;
                break;
        }
    }

    // UPDATE THE ITEMS LIST UI AND CREATE VICTORY END GAME IF ALL ITEMS COLLECTED
    public static void tickOffItem(GameObject item)
    {
        switch (item.name)
        {
            case "Antenna":
                antennaText = "Antenna: 1/1";
                break;
            case "Ship Body":
                shipBodyText = "Ship Body: 1/1";
                break;
            case "Fuel Containers":
                fuelContainersText = "Fuel Containers: 1/1";
                break;
        }
        string newPartsText = "Parts Collected:\n" + antennaText + "\n" + shipBodyText + "\n" + fuelContainersText;
        partList.text = newPartsText;
        itemCount++;

        if (itemCount == itemLimit)
            endGame(EndScenario.VICTORY);
    }
}
