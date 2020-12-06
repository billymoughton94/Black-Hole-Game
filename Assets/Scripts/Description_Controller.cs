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
        itemDescription.Add("Iron", "Used for crafting");
        itemDescription.Add("Magnet", "Used for crafting");
        itemDescription.Add("Antenna", "Useful for navigating the skies. A key item in repairing the ship.");
        itemDescription.Add("Food", "Can be eaten to restore hunger.");
        itemDescription.Add("Monster Meat", "A questionable choice of food. Restores 20 hunger.");
    }
    
    void Update() {
        transform.position = Input.mousePosition + new Vector3(125f, 50f, 0);
        UIText.text = itemDescription[name];
    }

    public void setText(string name) {
        this.name = name;
    }
}
