using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum tileTypes
{
    DEFAULT,
    MINIMAL,
    MEDIUM,
    MAXIMUM
}

public class Tile : MonoBehaviour
{
    public FillGameBoard gameBoard;
    public Image image;
    public tileTypes currentTileType;

    public bool tileHarvested = false;
    public bool tileScanned = false;

    // Start is called before the first frame update
    void Start()
    {
        gameBoard = GetComponentInParent<FillGameBoard>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateTileColor()
    {
        switch (currentTileType)
        {
            case tileTypes.MINIMAL:
                image.color = Color.yellow;
                break;

            case tileTypes.MEDIUM:
                image.color = Color.red;
                break;

            case tileTypes.MAXIMUM:
                image.color = Color.magenta;
                break;

            default:
                image.color = Color.gray;
                break;
        }
    }
    public void clickTile(GameObject clickedTile)
    {
        if (gameBoard.currentTool == toolSelect.SCAN)
        {
            if (gameBoard.scanNumbers > 0)
            {
                gameBoard.scanNumbers--;
                for (int r = 0; r < 32; r++)
                {
                    for (int c = 0; c < 32; c++)
                    {
                        if (gameBoard.tiles[r, c] == clickedTile)
                        {
                            // Clicked Tile
                            gameBoard.tiles[r, c].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r, c].GetComponent<Tile>().updateTileColor();
                            // Middle Right Tile
                            gameBoard.tiles[r, c + 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r, c + 1].GetComponent<Tile>().updateTileColor();

                            // Middle Left Tile
                            gameBoard.tiles[r, c - 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r, c - 1].GetComponent<Tile>().updateTileColor();

                            // Top Middle Tile
                            gameBoard.tiles[r + 1, c].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r + 1, c].GetComponent<Tile>().updateTileColor();

                            // Top Right Tile
                            gameBoard.tiles[r + 1, c + 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r + 1, c + 1].GetComponent<Tile>().updateTileColor();

                            // Top Left Tile
                            gameBoard.tiles[r + 1, c - 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r + 1, c - 1].GetComponent<Tile>().updateTileColor();

                            // Bottom Middle Tile
                            gameBoard.tiles[r - 1, c].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r - 1, c].GetComponent<Tile>().updateTileColor();

                            // Bottom Right Tile
                            gameBoard.tiles[r - 1, c + 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r - 1, c + 1].GetComponent<Tile>().updateTileColor();

                            // Bottom Left Tile
                            gameBoard.tiles[r - 1, c - 1].GetComponent<Tile>().tileScanned = true;
                            gameBoard.tiles[r - 1, c - 1].GetComponent<Tile>().updateTileColor();
                        }
                    }
                }
            }
        }
        else
        {
            gameBoard.extractNumbers--;
            if (!tileHarvested)
            {
                switch (currentTileType)
                {
                    case tileTypes.MINIMAL:
                        gameBoard.increaseScore(1000);
                        break;

                    case tileTypes.MEDIUM:
                        gameBoard.increaseScore(2000);
                        break;

                    case tileTypes.MAXIMUM:
                        gameBoard.increaseScore(4000);
                        break;

                    default:
                        gameBoard.increaseScore(0);
                        break;
                }
                currentTileType = tileTypes.MINIMAL;

                if (tileScanned)
                    image.color = Color.yellow;
            }

            if (gameBoard.extractNumbers <= 0)
            {
                gameBoard.finishGame();
            }
        }
        gameBoard.updateText();
    }
}
