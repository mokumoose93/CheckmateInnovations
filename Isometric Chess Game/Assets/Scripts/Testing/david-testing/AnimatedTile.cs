using UnityEngine;

public class AnimatedTile : MonoBehaviour {

    public GameObject tile;
    public Sprite sprite;

    private float moveSpeed = 0.02f;

    void Start() {
        // Get the GameObject this script is attached to and assign to tile
        tile = gameObject;

        // Access the associated sprite via GetComponent
        sprite = tile.GetComponent<SpriteRenderer>().sprite;
        // sprite = this.GetComponent<SpriteRenderer>().sprite;

        // Debug.Log() the sprite for testing purposes
        Debug.Log("Sprite of the GameObject: " + sprite);

        //
    }

    void Update() {
        tile.transform.Translate(new Vector3(moveSpeed, 0, 0));
    }

}
