using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace PlayerUnits {
    public class Rook : PlayerUnit {

        //*********************************************
        //* VARIABLES
        //*********************************************

        //*********************************************
        //* START + UPDATE FUNCTIONS
        //*********************************************



        //*********************************************
        //* STANDARD CLASS FUNCTIONS
        //*********************************************

        public override List<Vector3Int> CalculateMovementPath() {
            //TODO pathing currently does not function properly, it produces a path to the left only.

            // Initialize relevant values
            Vector3Int nextCellPos;
            List<Vector3Int> movePath = new List<Vector3Int>();

            // Get this GameObject's current position in cell coordinates
            Vector3Int objCellPos = tilemap.WorldToCell(gameObject.transform.position);

            // Check forward moves until either a unit or tilemap bounds is reached
            nextCellPos = objCellPos + Vector3Int.up;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.up;
            }

            // Check back moves until either a unit or tilemap bounds is reached        
            nextCellPos = objCellPos + Vector3Int.down;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.down;
            }

            // Check left moves until either a unit or tilemap bounds is reached
            nextCellPos = objCellPos + Vector3Int.left;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.left;
            }

            // Check left moves until either a unit or tilemap bounds is reached
            nextCellPos = objCellPos + Vector3Int.right;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.right;
            }

            return movePath;
        }

    }
}