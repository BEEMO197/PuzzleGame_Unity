using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum toolSelect
{
    SCAN,
    EXTRACT
}
public class FillGameBoard : MonoBehaviour
{
    public GameObject Tileprefab;
    public GameObject[,] tiles = new GameObject[32, 32];

    public int numDeposits = 5;
    public toolSelect currentTool = toolSelect.SCAN;
    public int score;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI scoreText2;
    public TMPro.TextMeshProUGUI extractsNumText;
    public TMPro.TextMeshProUGUI scanNumText;

    public int scanNumbers = 6;
    public int extractNumbers = 3;

    public GameObject GameFinishedObject;
    // Start is called before the first frame update
    void Start()
    {
        GameFinishedObject.SetActive(false);
        for(int r = 0; r < 32; r++)
        {
            for(int c = 0; c < 32; c++)
            {
                tiles[r, c] = Instantiate(Tileprefab, transform);
            }
        }

        // Generate Mineral Tiles
        for (int i = 0; i < numDeposits; i++)
        {
            int rowRand = 0;
            int colRand = 0;
            // Selected Tile
            bool goodTile = false;
            while (!goodTile)
            {
                rowRand = Random.Range(0, 32);
                colRand = Random.Range(0, 32);
                tileTypes tileType = tiles[rowRand, colRand].GetComponent<Tile>().currentTileType;
                if (tileType == tileTypes.MAXIMUM || tileType == tileTypes.MEDIUM || tileType == tileTypes.MINIMAL) ;
                else
                {
                    tiles[rowRand, colRand].GetComponent<Tile>().currentTileType = tileTypes.MAXIMUM;
                    goodTile = true;
                }
            }

            Debug.Log("Row Number: " + rowRand);
            Debug.Log("Col Number: " + colRand);
            if (rowRand != 31)
            {
                // Below Selected Tile
                tiles[rowRand + 1, colRand].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                if (colRand != 31)
                {
                    // Below Right 
                    tiles[rowRand + 1, colRand + 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                }
                if (colRand != 0)
                {
                    // Below Left
                    tiles[rowRand + 1, colRand - 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                }
            }

            if (rowRand != 0)
            {
                // Above Selected Tile
                tiles[rowRand - 1, colRand].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                if (colRand != 0)
                {
                    // Above Left 
                    tiles[rowRand - 1, colRand - 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                }
                if (colRand != 31)
                {
                    // Above Right
                    tiles[rowRand - 1, colRand + 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
                }
            }
            if (colRand != 31)
            {
                // Right Selected Tile
                tiles[rowRand, colRand + 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
            }
            if (colRand != 0)
            {
                // Left Selected Tile
                tiles[rowRand, colRand - 1].GetComponent<Tile>().currentTileType = tileTypes.MEDIUM;
            }

            // Outer Ring

            if (rowRand != 31 && rowRand != 30)
            {
                if (colRand != 31)
                {
                    if (colRand != 30)
                    {
                        // Bottom Right
                        tiles[rowRand + 2, colRand + 2].GetComponent<Tile>().currentTileType = tiles[rowRand + 2, colRand + 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                    }
                    // Bottom MidRight
                    tiles[rowRand + 2, colRand + 1].GetComponent<Tile>().currentTileType = tiles[rowRand + 2, colRand + 1].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
                // Bottom Mid
                tiles[rowRand + 2, colRand].GetComponent<Tile>().currentTileType = tiles[rowRand + 2, colRand].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;

                if (colRand != 0)
                {
                    // Bottom MidLeft
                    tiles[rowRand + 2, colRand - 1].GetComponent<Tile>().currentTileType = tiles[rowRand + 2, colRand - 1].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                    if (colRand != 1)
                    {
                        // Bottom Left
                        tiles[rowRand + 2, colRand - 2].GetComponent<Tile>().currentTileType = tiles[rowRand + 2, colRand - 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                    }
                }
            }

            if (rowRand != 0 && rowRand != 1)
            {
                if (colRand != 0)
                {
                    if (colRand != 1)
                    {
                        // Top Left
                        tiles[rowRand - 2, colRand - 2].GetComponent<Tile>().currentTileType = tiles[rowRand - 2, colRand - 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                    }
                    // Top MidLeft
                    tiles[rowRand - 2, colRand - 1].GetComponent<Tile>().currentTileType = tiles[rowRand - 2, colRand - 1].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
                // Top Mid
                tiles[rowRand - 2, colRand].GetComponent<Tile>().currentTileType = tiles[rowRand - 2, colRand].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                if (colRand != 31)
                {

                    // Top MidRight
                    tiles[rowRand - 2, colRand + 1].GetComponent<Tile>().currentTileType = tiles[rowRand - 2, colRand + 1].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;

                    if (colRand != 30)
                    {
                        // Top Right
                        tiles[rowRand - 2, colRand + 2].GetComponent<Tile>().currentTileType = tiles[rowRand - 2, colRand + 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                    }
                }
            }

            if (colRand != 30 && colRand != 31)
            {
                // Right Mid
                tiles[rowRand, colRand + 2].GetComponent<Tile>().currentTileType = tiles[rowRand, colRand + 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                if (rowRand != 0)
                {
                    // Right TopMid
                    tiles[rowRand - 1, colRand + 2].GetComponent<Tile>().currentTileType = tiles[rowRand - 1, colRand + 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
                if (rowRand != 31)
                {
                    // Right BotMid
                    tiles[rowRand + 1, colRand + 2].GetComponent<Tile>().currentTileType = tiles[rowRand + 1, colRand + 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
            }

            if (colRand != 0 && colRand != 1)
            {
                // Left Mid
                tiles[rowRand, colRand - 2].GetComponent<Tile>().currentTileType = tiles[rowRand, colRand - 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                if (rowRand != 0)
                {
                    // Left TopMid
                    tiles[rowRand - 1, colRand - 2].GetComponent<Tile>().currentTileType = tiles[rowRand - 1, colRand - 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
                if (rowRand != 31)
                {
                    // Left BotMid
                    tiles[rowRand + 1, colRand - 2].GetComponent<Tile>().currentTileType = tiles[rowRand + 1, colRand - 2].GetComponent<Tile>().currentTileType == tileTypes.MEDIUM ? tileTypes.MEDIUM : tileTypes.MINIMAL;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishGame()
    {
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                tiles[r, c].GetComponent<Tile>().image.raycastTarget = false;
            }
        }
        GameFinishedObject.SetActive(true);
        scoreText2.text = score.ToString();
    }

    public void increaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }

    public void updateText()
    {
        scanNumText.text = scanNumbers.ToString();
        extractsNumText.text = extractNumbers.ToString();
    }

    public void changeTool(Text textToChange)
    {
        if (currentTool == toolSelect.SCAN)
        {
            currentTool = toolSelect.EXTRACT;
            textToChange.text = "SCAN";
        }
        else
        {
            currentTool = toolSelect.SCAN;
            textToChange.text = "EXTRACT";
        }
    }
}
