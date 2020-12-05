using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Controller : MonoBehaviour {

    private string name;
    private int amount;
    private bool useable;

    private TextMeshProUGUI UIText;
    private GameObject UIButton;

    void Start() {
        UIText = transform. GetComponentInChildren<TextMeshProUGUI>();
        UIButton = GameObject.Find("Button");
    }

    public void setValues(string name, int amount, bool useable) {
        this.name = name;
        this.amount = amount;
        this.useable = useable;
        updateItem();
    }

    private void updateItem() {
        UIText.text = name + ": " + amount;
        UIButton.SetActive(useable);
    }
}
