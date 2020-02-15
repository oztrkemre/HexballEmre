using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanic : MonoBehaviour
{
    public GameObject selectObj;
    public Transform levelParent;

    bool rotate;

    void Update()
    {
        if(GameManager.instance.isGame == GameStates.gameBegin)
            Swipe();
    }

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var dist = Vector2.Distance(firstPressPos, secondPressPos);

            if (dist < 10)
            {
                SelectHex();
            }
            else if (selectObj.activeInHierarchy == true)
            {
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                currentSwipe.Normalize();

                if(currentSwipe.x>0||currentSwipe.y>0)
                {
                    if (GameManager.instance.isGame == GameStates.gameBegin)
                    {
                        StartCoroutine(Rotate(1.5f));
                        GameManager.instance.isGame = GameStates.rotateHex;
                    }
                }
               
            }
        }
    }
    void SelectHex()
    {
        if (selectObj.transform.childCount > 1)
        {
            for (int i = 2; i >= 0; i--)
            {
                selectObj.transform.GetChild(i).parent = levelParent;
            }
        }

        var v3 = Input.mousePosition;
        v3.z = 0;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        RaycastHit2D[] hit;

        hit = Physics2D.CircleCastAll(v3, 0.15f, new Vector2(1, 1), 0.4f);

        if (hit.Length > 2)
        {
            var x0 = (hit[0].transform.position.x + hit[1].transform.position.x + hit[2].transform.position.x) / 3;
            var y0 = (hit[0].transform.position.y + hit[1].transform.position.y + hit[2].transform.position.y) / 3;
            if (selectObj.activeInHierarchy == false)
            {
                selectObj.SetActive(true);
                selectObj.transform.position = new Vector2(x0, y0);
            }
            else
                selectObj.transform.position = new Vector2(x0, y0);

            for (int i = 0; i < 3; i++)
            {
                hit[i].transform.parent = selectObj.transform;
            }
        }
    }

    IEnumerator Rotate(float duration)
    {
        float startRotation = selectObj.transform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        Debug.Log(t);
        while (t < duration)
        {
            
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            selectObj.transform.eulerAngles = new Vector3(selectObj.transform.eulerAngles.x, selectObj.transform.eulerAngles.y, zRotation);
            yield return null;
        }
        GameManager.instance.isGame = GameStates.gameBegin;
    }
}