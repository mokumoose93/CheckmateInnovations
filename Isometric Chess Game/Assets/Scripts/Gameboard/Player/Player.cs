using System.Collections.Generic;
using PlayerUnits;
using UnityEngine;

public class Player : MonoBehaviour {

    //*********************************************
    //* VARIABLES
    //*********************************************

    List<PlayerUnit> units;     // The List of units that this player has

    bool isTurn;                // Flag for "is it player's turn?"

    string playerName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerName = gameObject.name;
    }

    // Update is called once per frame
    void Update() {

    }

    //*********************************************
    //* FUNCTIONS
    //*********************************************

    void PassTurn() {

    }


}
