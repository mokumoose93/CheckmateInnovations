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

            // Vector3Int currCellPos = objCellPos;
            Vector3Int nextCellPos;
            List<Vector3Int> movePath = new List<Vector3Int>();

            // Check forward moves until either a unit or tilemap bounds is reached
            nextCellPos = objCellPos + Vector3Int.forward;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.forward;
            }

            // Check back moves until either a unit or tilemap bounds is reached        
            nextCellPos = objCellPos + Vector3Int.back;
            while (tilemap.HasTile(nextCellPos) && !tilemapBehavior.HasUnit(nextCellPos)) {
                movePath.Add(nextCellPos);
                nextCellPos += Vector3Int.back;
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