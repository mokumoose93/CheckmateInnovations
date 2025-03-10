using UnityEngine;
using UnityEngine.Tilemaps;
using Gameboard;
using System.Collections.Generic;

namespace PlayerUnits {
    public abstract class PlayerUnit : MonoBehaviour {
        public TilemapBehavior tilemapBehavior;         // TilemapBehavior class instantiation associated with tilemap behavior

        private SpriteRenderer spriteRenderer;
        public Tilemap tilemap;                         // Tilemap that this unit is positioned on
        public Vector3Int objCellPos;

        //public List<Vector3Int> movePath;

        private bool isSelected = false;
        private bool isHovering = false;

        public int maxBoundX;
        public int maxBoundY;
        public int minBoundX;
        public int minBoundY;

        private float transformScale = 1.2f;

        //*********************************************
        //* START + UPDATE FUNCTIONS
        //*********************************************

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            // Assign Components
            tilemap = GetComponentInParent<Tilemap>();
            tilemapBehavior = GetComponentInParent<TilemapBehavior>();      //! Unit must be a child of the tilemap in the hierarchy for this to work
            spriteRenderer = GetComponent<SpriteRenderer>();

            // Assign values of Unity Class Variables
            objCellPos = tilemap.WorldToCell(gameObject.transform.position);

            // Assign simple variables
            maxBoundX = tilemap.cellBounds.xMax;
            maxBoundY = tilemap.cellBounds.yMax;
            minBoundX = tilemap.cellBounds.xMin;
            minBoundY = tilemap.cellBounds.yMin;
        }

        // Update is called once per frame
        void Update() {

        }

        //*********************************************
        //* STANDARD CLASS FUNCTIONS
        //*********************************************

        public abstract List<Vector3Int> CalculateMovementPath();

        //*********************************************
        //* EVENT FUNCTIONS
        //*********************************************

        // void OnMouseEnter() {
        //     //TODO 1. need visual feedback when the mouse is hovering over a chess piece
        //     //TODO 2. be able to click on the chess piece and "select" it
        //     //TODO 3. move the piece by clicking somewhere else
        //     //TODO 4. proper piece pathing

        //     // Give visual feedback when mouse hovers over object
        //     transform.localScale *= transformScale;
        //     isHovering = true;
        // }

        // void OnMouseExit() {
        //     transform.localScale /= transformScale;
        //     isHovering = false;
        // }

        // void OnMouseDown() {
        //     //TODO probably best place to handle unit selection in movement since it requires a collider (this prevents unneccessary duplicate calls)



        //     // Set the current piece as the currently selected piece on the board
        //     if (isHovering) {   // Check that this is the piece to receive MouseDown action
        //         if (!cursorResponse.hasSelectedPiece) {
        //             cursorResponse.hasSelectedPiece = true; // Let the associated tilemap know that a piece is selected
        //         }

        //         cursorResponse.selectedObject = this.gameObject; // Assign this piece as the selected piece on the tilemap
        //     }

        // }
    }
}