using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStates
{
    gameBegin,rotateHex
}
[System.Serializable]
public struct Level
{
    public int gridSizeX,gridSizeY;
}

public class GameManager : MonoBehaviour
{
    public Level[] level;

    public int currentLevel = 0;
    public GameStates isGame;
    public Color[] hexColors;

    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelLoad()
    {
        
    }

}
