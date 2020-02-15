using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexagonMaker : MonoBehaviour
{

    int gridX;
    int gridY;
    public GameObject Hexagon;

    public float xOffset = 1;
    public float yOffset = 1;

    private void Start()
    {
        TakeCurrenLevelData();
        CreateHexMap();
        
    }

    private void CreateHexMap()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                GameObject tempHex = Instantiate(Hexagon);

                if (y % 2 == 0)
                {
                    tempHex.transform.position = new Vector2(x * xOffset, y * yOffset);
                }
                else
                    tempHex.transform.position = new Vector2(x * xOffset + xOffset / 2, y * yOffset);

                tempHex.transform.parent = transform;
                tempHex.name = x.ToString() + "," + y.ToString();
            }
        }
        //Debug.Log(gridX + "<x|y>" + gridY);
    }

    void TakeCurrenLevelData()
    {
        gridX = Mathf.CeilToInt(GameManager.instance.level[GameManager.instance.currentLevel].gridSizeX / 2);
        gridY = GameManager.instance.level[GameManager.instance.currentLevel].gridSizeY;
    }

}
