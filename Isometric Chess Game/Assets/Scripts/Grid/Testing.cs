using UnityEngine;

public class Testing : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        Grid grid = new Grid(4, 2, 10f);
        Debug.Log("Grid Creation Successful");
    }

    // Update is called once per frame
    void Update() {

    }
}
