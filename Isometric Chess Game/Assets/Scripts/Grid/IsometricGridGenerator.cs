using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsometricGridGenerator : MonoBehaviour {
    public Tilemap tilemap; // Reference to the Tilemap component
    public Tile[] tiles; // Array of Tile assets (e.g., grass, stone, water)
    public int gridWidth = 8; // Width of the grid
    public int gridHeight = 8; // Height of the grid

    void Start() {
        GenerateGrid();

    }

    void Update() {

    }

    void GenerateGrid() {
        // Loop through each cell in the grid
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                // Calculate the position of the current cell
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                // Choose a tile based on position or other logic
                Tile tile = ChooseTile(x, y);

                // Place the tile on the tilemap
                tilemap.SetTile(cellPosition, tile);

            }
        }
    }

    Tile ChooseTile(int x, int y) {
        // Example logic: Alternate between tiles for a checkerboard pattern
        if ((x + y) % 2 == 0) {
            return tiles[0]; // Grass tile
        }
        else {
            return tiles[1]; // Stone tile
        }
    }
}