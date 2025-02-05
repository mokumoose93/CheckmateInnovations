using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAnimations : MonoBehaviour {
    public Tilemap tilemap;
    private Tile[] tiles;
    private int gridX;
    private int gridY;
    private int gridZ;

    private AnimationGridOverlay animationGrid;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Set tilemap dimensions to start at proper origin in editor
        gridX = tilemap.origin.x;
        gridY = tilemap.origin.y;
        gridZ = tilemap.origin.z;

        // Get the Size and Origin of the Tilemap in cells count for testing purposes
        Debug.Log("Size of Tilemap: " + tilemap.size);
        Debug.Log("Origin of Tilemap: " + tilemap.origin);

        // Start by cycling through all the tiles found in the Tilemap object for debugging purposes
        GetTilemapData(tilemap);

        // Replicate the tilemap as a Grid of GameObjects that can be used for animation
        animationGrid.GenerateGrid(tilemap);

    }


    // Update is called once per frame
    void Update() {

        // Animate tiles by moving them sequentially
        //AnimateTile(new Vector3Int(gridX, gridY, gridZ));

    }

    void GetTilemapData(Tilemap tilemap) {
        for (int x = tilemap.origin.x; x < tilemap.size.x; x++) {
            for (int y = tilemap.origin.y; y < tilemap.size.y; y++) {
                for (int z = tilemap.origin.z; z < tilemap.size.z; z++) {
                    Debug.Log("Tile Coordinate: (" + x + ", " + y + ", " + z + ")");
                    TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y, z));
                    if (tileBase != null) {
                        Debug.Log("Tile Sprite Name: " + tileBase.ToString());
                    }
                    else {
                        Debug.Log("Tile Sprite Name: null");
                    }
                }
            }
        }
    }

    void AnimateTile(Vector3Int tilePos) {
        // Need to make use of the Tile class and not the TileBase class
        // Tile makes use of gameObject whereas TileBase does not, and therefore does not have a transform

        Tile tileBase = (Tile)tilemap.GetTile(tilePos);
        //tileBase
    }


}
