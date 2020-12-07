using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Description_Controller : MonoBehaviour {
    
    [SerializeField]
    private TextMeshProUGUI UIText;
    // Dictionary holding the descriptions of all items
    private Dictionary<string, string> itemDescription;
    private string name;

    void Start() {
        itemDescription = new Dictionary<string, string>();
        itemDescription.Add("Iron", "A common ore use for crafting");
        itemDescription.Add("Obsidian", "A rare ore used for crafting");
        itemDescription.Add("Magnet", "Used for crafting the antenna.");
        itemDescription.Add("Antenna", "Useful for navigating the skies. A key item in repairing the ship");
        itemDescription.Add("Berry", "Restores 20 Hunger when consumed");
        itemDescription.Add("Health Pack", "Restores 10 Health when consumed. Use wisely...");
        itemDescription.Add("Ship Body", "A repaired body for your ship");
        itemDescription.Add("Plate Sheet", "Welded metal that can be used to create a ship body");
        itemDescription.Add("Fuel Containers", "You'll need fuel to escape this hellhole...");
    }
    
    void Update() {
        transform.position = Input.mousePosition + new Vector3(125f, 50f, 0);
        UIText.text = itemDescription[name];
    }

    public void setText(string name) {
        this.name = name;
    }
}
