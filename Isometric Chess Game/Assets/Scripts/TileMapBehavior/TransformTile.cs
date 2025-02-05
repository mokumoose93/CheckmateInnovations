using UnityEngine;
using UnityEngine.Tilemaps;

public class TransformTile : TileBase {

    // Goal of this class:
    //////////////////////
    // Apply modifications to the selected tile in order to animate it

    public enum ActionType {
        Translate,
        Rotate,
        Scale
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData, ActionType action, Vector3 transformVal) {

        // tileData.

        switch (action) {
            case ActionType.Translate:
                break;
        }

    }

}
