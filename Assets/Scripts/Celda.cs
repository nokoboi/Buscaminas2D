using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Celda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TMP_Text tmpText;

    public void setY(int y)
    {
        this.y = y;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return this.y;
    }

    public int getX()
    {
        return this.x;
    }

    public void setBomb(bool bomb)
    {
        this.bomb = bomb;
    }
    
    public bool isBomb()
    {
        return bomb;
    }

    public void setText(string text)
    {
        this.tmpText.text = text;
    }

    private void OnMouseDown()
    {
        if (this.bomb)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else
        {
            tmpText.text = Generator.Instance.getBombsAround(x,y).ToString();
        }
    }

}
