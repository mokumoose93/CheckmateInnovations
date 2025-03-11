using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Client.Differences;
using log4net.DateFormatter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using PlayerUnits;
//using System.Numerics;

//TODO write code that makes individual tiles in a tilemap respond when hovered over by cursor (move tile up or have sprite appear, etc.)
//TODO write code that allows players to make action happen when clicking on a tile with a unit on it
//TODO write code that makes selected unit move to another tile

namespace Gameboard {
    public class TilemapBehavior : MonoBehaviour {

        //*********************************************
        //* VARIABLES
        //*********************************************
        public PlayerUnit selectedUnit;

        private Tilemap tilemap;
        public GameObject selectedObject;               //? Redundant to have selectedObject AND selectedUnit
        public GameObject hoveringObject;
        public GameObject pathTile;

        public List<Vector3Int> movePath;
        public List<Vector3Int> lastMovePath;
        public List<GameObject> movePathTiles;

        private Vector3Int currentMouseCellPos;
        private Vector3Int lastMouseCellPos;

        public float transformScale = 1.5f;
        public float translateAmount;
        public bool hasSelectedPiece;                   //? May be obsolete
        public bool isHovering;
        public bool pathIsDrawn;

        //*********************************************
        //* START + UPDATE FUNCTIONS
        //*********************************************

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            // Get Tilemap that is associated with GameObject that this script is attached as a component to
            tilemap = this.GetComponent<Tilemap>();
            selectedObject = null;
            selectedUnit = null;
            hasSelectedPiece = false;
            pathIsDrawn = false;

            // // Iterate through all the children of the tilemap
            // int counter = 0; // include a counter to differentiate between objects of identical names
            // foreach (Transform child in tilemap.transform) {
            //     // Append the counter to the child's name
            //     child.name = $"{child.name}_{counter}";
            //     Debug.Log("Found painted GameObject: " + child.name);

            //     GameObject gameObject = child.gameObject; // Example of getting the GameObject from the Transform

            //     // Move the child in a direction that is within the bounds of the tilemap
            //     Vector3Int cellPosition = tilemap.WorldToCell(child.position); // Convert world position of child to cell position in tilemap
            //     bool moveCheck = tilemap.HasTile(cellPosition + Vector3Int.right); // Check if the child can move to the right on the tilemap
            //     if (moveCheck) {
            //         //child.transform.Translate(Vector3Int.right);
            //         Debug.Log("Child translated Cell Position: " + (cellPosition + Vector3Int.right));
            //         child.transform.position = tilemap.GetCellCenterWorld(cellPosition + Vector3Int.right); // Use GetCellCenterWorld and NOT CellToWorld
            //         //TODO Lerp the movement
            //         //tile.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / (animationDuration / 2));
            //     }

            //     counter++; // increment the counter
            // }

        }

        // Update is called once per frame
        void Update() {
            // Get mouse position from screen to world to tilemap cell coordinates
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);   //? probably obsolete

            // Update the Render Order for all the GameObjects on this tilemap
            UpdateRenderOrder();

            // Perform Actions for Left Mouse Click (GetMouseButtonDown(0))
            if (Input.GetMouseButtonDown(0)) {
                MouseClickLeft(mouseWorldPos);
            }

            // // Actions to perform when an object is selected 
            // //TODO movement path should be calculated by the unit selected based on its movement rules
            // if (selectedObject != null) {
            //     // Calculate the movement path
            //     CalculateMovementPathGeneric();

            //     // If the movement path has changed, delete the old one before drawing a new one
            //     if (PathChanged()) {
            //         ClearMovementPath();
            //     }

            //     // If a movepath exists and the mouse is within the tilemap bounds, draw the path
            //     if (movePath != null && MouseInBounds()) {
            //         DrawMovementPath();
            //     }

            // } else {    // if no GameObject is selected, clear last path
            //     ClearMovementPath();
            // }

            //TODO Logic needs to be cleaned up, shouldn't have to call CalculateMovementPath every frame. Maybe only on mouse click?
            if (selectedObject != null && selectedObject.tag == "Unit") {
                selectedUnit = selectedObject.GetComponent<PlayerUnit>();
                movePath = selectedUnit.CalculateMovementPath();

                if (PathChanged()) {
                    ClearMovementPath();
                }

                if (movePath != null) {
                    DrawMovementPath();
                }

            } else {
                ClearMovementPath();
            }

            // Variable Post-Condition Updates
            lastMouseCellPos = cellPosition;
            if (movePath != null) {
                lastMovePath = movePath;
            }
        }

        //*********************************************
        //* STANDARD CLASS FUNCTIONS
        //*********************************************

        // Get position of all GameObjects on Tilemap in Tilemap Cell Coordinates and calculate render order based on position
        void UpdateRenderOrder() {
            // Vector3Int cellPosition;
            int spriteOrder = 0;    // Ordering starts at zero and counts upwards (anything < 0 will render behind the tilemap)

            // Retrieve all children into a list
            List<Transform> childList = new List<Transform>();

            foreach (Transform child in tilemap.transform) {
                childList.Add(child);
            }

            // Sort the list first by Y, then by X
            childList.Sort((t1, t2) => {
                Vector3Int t1Cell = tilemap.WorldToCell(t1.position);
                Vector3Int t2Cell = tilemap.WorldToCell(t2.position);
                int yComparison = t1Cell.y.CompareTo(t2Cell.y);
                if (yComparison != 0) {
                    return yComparison;
                } else {
                    return t1Cell.x.CompareTo(t2Cell.x);
                }
            });

            childList.Reverse();    // Lowest values should be rendered last as they "appear closest"; Reverse the order

            // Assign Render Order in order of the sorted list
            foreach (Transform child in childList) {
                SpriteRenderer spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();    // Get the Associated Sprite Renderer
                if (spriteRenderer != null) {   // Assign sprite order value and increment
                    spriteRenderer.sortingOrder = spriteOrder;
                    spriteOrder++;
                }
            }
        }

        //TODO transforming the localScale seems to cause issues with collider detection, how to resolve this?
        void SelectObject(GameObject obj) {
            if (selectedObject != null) {
                DeselectObject();
            }
            selectedObject = obj;
            // selectedObject.transform.localScale *= transformScale;
            Debug.Log("Selected: " + selectedObject.name);
        }

        void DeselectObject() {
            if (selectedObject != null) {
                Debug.Log("Deselected: " + selectedObject.name);
                // selectedObject.transform.localScale /= transformScale;
                selectedObject = null;
            }
        }

        // Calculate the Path of Cells between the Mouse and the selected GameObject
        void CalculateMovementPathGeneric() {
            // Get cell positions from both the selected GameObject and the mouse
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 objWorldPos = selectedObject.transform.position;
            Vector3Int mouseCellPos = tilemap.WorldToCell(mouseWorldPos);
            Vector3Int objCellPos = tilemap.WorldToCell(objWorldPos);

            // Calculate the distance in (x, y) coordinates from GameObject cell to mouse cell
            int xDiff = mouseCellPos.x - objCellPos.x;
            int yDiff = mouseCellPos.y - objCellPos.y;

            // Calculate pathing direction, starting position, and number of steps to destination
            Vector3Int xDir = xDiff >= 0 ? Vector3Int.right : Vector3Int.left;
            Vector3Int yDir = yDiff >= 0 ? Vector3Int.up : Vector3Int.down;
            int xSteps = Mathf.Abs(xDiff);
            int ySteps = Mathf.Abs(yDiff);

            // movePathTiles = new Vector3Int[xSteps + ySteps];
            movePath = new List<Vector3Int>();

            //TODO have decision to draw either x or y first depending on which is longer (or other condition)
            // Collect the Cell Positions for the path on the x-axis starting from the GameObject
            for (int x = 1; x <= xSteps; x++) {
                Vector3Int cellCoords = objCellPos + (xDir * x);
                //? movePathTiles.Append(cellCoords);
                movePath.Add(objCellPos + (xDir * x));
            }
            // Collect the Cell Positions for the path on the y-axis starting from the end of the x-path
            for (int y = 1; y <= ySteps; y++) {
                //? movePathTiles.Append(objCellPos + (xDir * xSteps) + (yDir * y));
                movePath.Add(objCellPos + (xDir * xSteps) + (yDir * y));
            }
        }

        // Draw the path formed from CalculateMovementPath()
        void DrawMovementPath() {
            // if movePath exists and there is no path drawn yet, draw path
            if (movePath != null && !pathIsDrawn) {
                movePathTiles = new List<GameObject>();
                // Generate the movement path tiles
                foreach (Vector3Int tilePos in movePath) {
                    movePathTiles.Add(Instantiate(pathTile, tilemap.GetCellCenterWorld(tilePos), Quaternion.identity));
                }
                pathIsDrawn = true;
            }
        }

        void ClearMovementPath() {
            if (movePathTiles != null && pathIsDrawn) {
                foreach (GameObject obj in movePathTiles) {
                    Destroy(obj);
                }
                pathIsDrawn = false;
                movePath = null;
                movePathTiles = null;
            }
        }

        bool PathChanged() {
            // Check if either list is null
            if (lastMovePath == null || movePath == null) {
                return lastMovePath != movePath;
            }

            // Check for equal size
            if (lastMovePath.Count != movePath.Count) {
                return true;
            }

            // Check for equal elements
            for (int i = 0; i < lastMovePath.Count; i++) {
                if (lastMovePath[i] != movePath[i]) {
                    return true;
                }
            }

            // if all elements are equal, return false
            return false;
        }

        bool MouseInBounds() {
            int xBound = tilemap.cellBounds.xMax;
            int yBound = tilemap.cellBounds.yMax;
            Vector3Int mouseCellPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (mouseCellPos.x >= tilemap.origin.x && mouseCellPos.x < xBound && mouseCellPos.y >= tilemap.origin.y && mouseCellPos.y < yBound) {
                return true;
            } else {
                return false;
            }
        }

        // Check if the passed in cell position "cellPos" has a unit positioned on it
        public bool HasUnit(Vector3Int cellPos) {
            // Check for object using OverlapPoint
            Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPos));

            // Confirm that hit is actually positioned on that cell and not just overlapping it with a collider
            if (hit != null && hit.tag == "Unit") { // Check that something was actually hit and that it is tagged as a "Unit"
                Vector3Int actualPos = tilemap.WorldToCell(hit.gameObject.transform.position);
                Debug.Log("(Hit, Loc): (" + hit.gameObject + ", " + actualPos);
                if (actualPos == cellPos) { // Check that the position of that collider is actually in the cell being checked
                    return true;
                }
            }
            return false;
        }

        //*********************************************
        //* EVENT FUNCTIONS
        //*********************************************



        void MouseClickLeft(Vector3 mousePos) {
            Vector3Int cellPosition = tilemap.WorldToCell(mousePos);

            // Check if there's a unit at the cell position
            Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPosition));
            if (hit != null) {
                switch (hit.tag) {
                    case "Unit":
                        SelectObject(hit.gameObject);
                        break;
                    case "Path":
                        if (selectedObject != null && selectedUnit != null) {
                            selectedUnit.Move(cellPosition);
                            DeselectObject();
                        }
                        break;
                    default:
                        break;
                }
            } else {
                DeselectObject();
            }


            // if (hit != null && hit.tag == "Unit") {
            //     SelectObject(hit.gameObject);
            // } else {
            //     DeselectObject();
            // }

            //TODO if a unit is selected and the mouse clicks on the path, move that unit to that position
            if (hit != null && hit.tag == "Path") {
                if (selectedObject != null && selectedUnit != null) {
                    selectedUnit.Move(cellPosition);
                    DeselectObject();
                }
            }

            //TODO use a switch case to separate different cases

        }

        //! Currently avoiding OnMouse events because they require interaction with a collider associated with the tilemap which is currently an issue
        void OnMouseOver() {
            // // Convert the mouse position to world coordinates
            // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mouseWorldPos.z = 0; // Ensure the z-coordinate is in front of all possible tilemap target z-coordinates

            // // Output some Debug.Logs() for better understanding cell-space, world-space, and local-space
            // Debug.Log("mouseScreenPos: " + Input.mousePosition); // get the position of the mouse in screen coordinates
            // Debug.Log("mouseWorldPos: " + mouseWorldPos); // get the position of the mouse in world coordinates

            // // Get the cell position in the Tilemap
            // Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            // // Check if there is a tile at hovered over tile
            // if (tilemap.HasTile(cellPosition)) {
            //     Debug.Log("cellPosition: " + cellPosition); // get the position of the cell as an object in the grid

            //     // Get the GameObject that's instantiated at the cell position where mouse is hovering
            //     GameObject selectedTile = tilemap.GetInstantiatedObject(cellPosition);

            //     // Make the selected tile hover slightly
            //     if (selectedTile != null) {
            //         selectedTile.transform.Translate(Vector3.up * translateAmount);
            //     }
            //     else {
            //         Debug.Log("No GameObject Here");
            //     }
            // }

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            // Check if there's a unit at the cell position
            Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPosition));
            if (hit != null && hit.tag == "Unit") {
                //AddHighlightObject(hit.gameObject);
            } else {
                //RemoveHighlightObject();
            }
        }

        void OnMouseDown() {
            //! THIS FUNCTION ONLY CALLS WHILE MOUSE IS OVER THE COLLIDER OF ASSOCIATED GAMEOBJECT
            //TODO Get mouse position when mouse is clicked and this function is called
            //TODO Check on what is actually being clicked (tile, gameObject, what kind of gameObject?)
            //TODO if a gameobject is currently selected and a tile is clicked on, move to that tile
            //TODO if a gameobject is NOT currently selected and a tile is clicked on, do nothing
            //TODO if a gameobject is clicked on, set it to currently selected

            // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            // // Check if there's a unit at the cell position
            // Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPosition));
            // if (hit != null && hit.tag == "Unit") {
            //     SelectObject(hit.gameObject);
            // }
            // else {
            //     DeselectObject();
            // }
        }
    }
}
