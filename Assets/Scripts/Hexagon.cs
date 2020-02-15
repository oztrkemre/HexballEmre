using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Color hexColor;
    private void Start()
    {
        hexColor = GameManager.instance.hexColors[RandomColor()];
        hexColor.a = 1;
        GetComponent<SpriteRenderer>().color = hexColor;
    }

    int RandomColor()
    {
        var colorIndis = Random.Range(0, GameManager.instance.hexColors.Length);
        return colorIndis;
    }
}
