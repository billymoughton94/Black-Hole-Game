using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival_Controller : MonoBehaviour {

    private int health;
    private int hunger;

    void Start() {
        health = 100;
        hunger = 100;
        InvokeRepeating("hungerTick", 30.0f, 30.0f);
    }

    private void hungerTick() {
        hunger -= 1;
    }

    public void changeHealth(int amount) {
        health += amount;
    }

    public void restoreHunger(int amount) {
        hunger += amount;
    }

    public int getHealth() {
        return health;
    }
    public int getHunger() {
        return hunger;
    }
}
