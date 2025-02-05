using UnityEngine;
using UnityEngine.Tilemaps;

// Create a class that replicates the layout of a tilemap in order to perform animations
// Class will use a Grid of gameobjects to perform individual animations on cells
// Once Animations are finished, the tilemap will return as main rendering artifice
public class AnimationGridOverlay : MonoBehaviour {

    public UnityEngine.Grid grid;
    public GameObject[,,] cells;
    public GameObject cellPrefab;

    public void Start() {
        // Add SpriteRenderer to cellPrefab GameObject to handle sprites passed into it from tilemap
        cellPrefab.AddComponent<SpriteRenderer>();
    }

    public void Update() {

    }

    public void GenerateGrid(Tilemap tilemap) {
        // Generate the grid by creating GameObject cell at each exisiting point on the tilemap where there is actually a TileBase

        // Initialize the grid with size based on tilemap dimensions        
        cells = new GameObject[tilemap.size.x, tilemap.size.y, tilemap.size.z];

        // Assign each cell using nested for loops
        // Multiple counters are used to account for possible displacement of tilemap origin from world origin
        for (int x = tilemap.origin.x, i = 0; i < tilemap.size.x; x++, i++) {
            for (int y = tilemap.origin.y, j = 0; j < tilemap.size.y; y++, j++) {
                for (int z = tilemap.origin.z, k = 0; k < tilemap.size.z; z++, k++) {
                    //cells[x, y, z] = (Tile)tilemap.GetTile(new Vector3Int(x, y, z));
                    // Assign corresponding Sprite, Position, and ...
                    //cells[i, j, k].GetComponent(sprite).sprite = tilemap.GetSprite(new Vector3Int(x, y, z));
                    //cells[i, j, k].transform = new Vector3Int(x, y, z);
                    cellPrefab.GetComponent<SpriteRenderer>().sprite = tilemap.GetSprite(new Vector3Int(x, y, z));

                    Vector3Int cellPosition = new Vector3Int(x, y, z);

                    Vector3 worldPosition = grid.CellToWorld(cellPosition);

                    GameObject cell = Instantiate(cellPrefab, worldPosition, Quaternion.identity);

                    cell.transform.parent = transform;
                }
            }
        }
    }

    public void Animate(Tilemap tilemap) {

    }

}


// Update Notes:
// =============
// Getting familiar with GameObjects and remembering that you can add + get Component objects
// This allows the GameObject object to be incredibly flexible in the things it can handle
// Next step is to make sure that the sprite translates to the new GameObjects appropriately
// and that the GameObjects are properly distributed in the Grid in order to appropriately replicate the tilemap that it is copying from