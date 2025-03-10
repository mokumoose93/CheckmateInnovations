using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

//! TO BE ATTACHED TO A TILEMAP OBJECT
//TODO animate tiles individually and using interpolation for smooth animations (using lambda functions?)
//TODO Explore GenerateGrid2 using Tilemap.GetInstantiatedObject which may be more suitable for my animation goals
//TODO TileAnchor value (0.5, 0.5, 0) needs to be accounted for when generating animation grid (currently spawns offset from original by value specified)

public class TilemapAnimations : MonoBehaviour {

    public delegate void TestDelegate();
    public delegate void AnimateTileDelegate(Vector3Int tilePos);

    private TestDelegate testDelegateFunction;
    private AnimateTileDelegate AnimateTilesDelegateFunction;

    //* Object Variables
    private Tilemap tilemap;
    private GameObject[,,] tiles;

    //* Position related variables
    private int gridX;
    private int gridY;
    private int gridZ;

    //* Time Related Variables
    public float speed = 0.002f;
    public float animationDuration = 2f;
    public float delayBetweenTiles = 0.05f;
    private AnimationGridOverlay animationGrid;

    //* UNITY START + UPDATE FUNCTIONS
    //********************************

    //* START FUNCTION
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Get the tilemap associated with this object
        tilemap = this.GetComponent<Tilemap>();

        // Get the Size and Origin of the Tilemap in cells count for testing purposes
        Debug.Log("Size of Tilemap: " + tilemap.size);
        Debug.Log("Origin of Tilemap: " + tilemap.origin);

        // Start by cycling through all the tiles found in the Tilemap object for debugging purposes
        GetTilemapData(tilemap);

        // Replicate the tilemap as a Grid of GameObjects that can be used for animation
        tiles = GenerateGrid(tilemap);

        // Hide tilemap so that grid duplicate can visually take its place
        Renderer render = tilemap.GetComponent<Renderer>();
        render.enabled = false;

        // Perform Grid animation using Coroutines
        StartCoroutine(AnimateTilesSequence(tiles));

        // Resume rendering of tilemap
        //render.enabled = true;

        //TODO I don't know how to use delegates so I will avoid using them for now
        // AnimateTilesDelegateFunction = AnimateTile;

    }

    //* UPDATE FUNCTION
    // Update is called once per frame
    void Update() {



    }

    //* FUNCTIONS
    //***********

    // GetTilemapData Function
    void GetTilemapData(Tilemap tilemap) {

        // Initialize to 0
        gridX = gridY = gridZ = 0;

        for (int x = tilemap.origin.x; x < tilemap.size.x; x++) {
            for (int y = tilemap.origin.y; y < tilemap.size.y; y++) {
                for (int z = tilemap.origin.z; z < tilemap.size.z; z++) {
                    Debug.Log("Tile Coordinate: (" + x + ", " + y + ", " + z + ")");
                    TileBase tileBase = tilemap.GetTile(new Vector3Int(x, y, z));
                    if (tilemap.GetSprite(new Vector3Int(x, y, z)) != null) {
                        Debug.Log("Tile Sprite Name: " + tileBase.ToString());
                        // set grid size indicators to latest indices
                        gridX = x;
                        gridY = y;
                        gridZ = z;
                    }
                    else {
                        Debug.Log("Tile Sprite Name: null");
                    }
                }
            }
        }
    }

    // GenerateGrid Function
    public GameObject[,,] GenerateGrid(Tilemap tilemap) {
        // Generate the grid by creating GameObject cell at each exisiting point on the tilemap where there is actually a TileBase

        // Initialize the grid with size based on tilemap dimensions        
        GameObject[,,] cells = new GameObject[tilemap.size.x, tilemap.size.y, tilemap.size.z];

        // GameObject cellPrefab to handle sprite allocation
        GameObject cellPrefab = new GameObject();
        cellPrefab.AddComponent<SpriteRenderer>();

        // Attach Grid component from parent GameObject
        Grid grid = this.GetComponentInParent<Grid>();

        // Counter for rendering order
        float renderOrder = 0.0f;

        // Assign each cell using nested for loops
        // Multiple counters are used to account for possible displacement of tilemap origin from world origin
        for (int x = tilemap.origin.x, i = 0; i < tilemap.size.x; x++, i++) {
            for (int y = tilemap.origin.y, j = 0; j < tilemap.size.y; y++, j++) {
                for (int z = tilemap.origin.z, k = 0; k < tilemap.size.z; z++, k++) {
                    // Check if location contains sprite data (tilemap can be initialized as larger than actual space of sprites used)
                    if (tilemap.GetSprite(new Vector3Int(x, y, z)) != null) {
                        // Attach Sprite to cellPrefab which will be referenced for Instantiate()
                        cellPrefab.GetComponent<SpriteRenderer>().sprite = tilemap.GetSprite(new Vector3Int(x, y, z));
                        Vector3Int cellPosition = new Vector3Int(x, y, z);
                        Vector3 worldPosition = grid.CellToWorld(cellPosition);
                        // Adjust the Z position to reflect position in the render hierarchy
                        Vector3 renderPosition = new Vector3(worldPosition.x, worldPosition.y, renderOrder * 0.01f);
                        Debug.Log("(Cell, World): (" + cellPosition + ", " + worldPosition + ")");
                        cells[i, j, k] = Instantiate(cellPrefab, renderPosition, Quaternion.identity);
                        cells[i, j, k].transform.parent = transform;
                        renderOrder++;
                    }
                }
            }
        }

        // Destroy the referenced prefab
        Destroy(cellPrefab);

        return cells;
    }

    // GenerateGrid2 Function (TODO)
    void GenerateGrid2() {
        //TODO Use this space to experiment achieving the same outcome as as GenerateGrid() but using Tilemap.GetInstantiatedObject()
    }

    // AnimateTile Function
    //TODO is this function even necessary? Learning to use Delegates vs. Lambda functions with coroutines
    void AnimateTilemap(AnimateTileDelegate delegateMethod, GameObject[,,] gameObjectGrid) {

        for (int x = 0; x < gameObjectGrid.GetLength(0); x++) {
            for (int y = 0; y < gameObjectGrid.GetLength(1); y++) {
                for (int z = 0; z < gameObjectGrid.GetLength(2); z++) {

                }
            }
        }
    }

    //* Lambda Functions
    //******************

    IEnumerator AnimateTilesSequence(GameObject[,,] tiles) {
        foreach (var tile in tiles) {
            // Use of lambda function to define the animation behavior
            System.Action<GameObject> animateTile = (b) => {
                StartCoroutine(AnimateTile(b));
            };

            // Start the animation for current tile
            animateTile(tile);

            // Wait for the specified delay before animiating the next block
            yield return new WaitForSeconds(delayBetweenTiles);
        }
    }

    IEnumerator AnimateTile(GameObject tile) {
        Vector3 originalPosition = tile.transform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * 2f;

        float elapsedTime = 0f;

        // Move tile up
        while (elapsedTime < animationDuration / 2) {
            tile.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / (animationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move tile back down
        elapsedTime = 0f;
        while (elapsedTime < animationDuration / 2) {
            tile.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / (animationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure tile returns to its original position
        tile.transform.position = originalPosition;
    }

}
