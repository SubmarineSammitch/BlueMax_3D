using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public Vector3 pos;
    public HexTileGenerator map;
    private Color m_HoverColor;

    private void Start() {

        pos = GetComponent<Renderer>().bounds.center;
    }
    private void OnMouseUp() {
        // Debug.Log("Click");
        //Debug.Log(pos);
        map.player1Move(pos);
    }
    private void OnMouseEnter() {
        //Debug.Log("Im a Hoverin");
        m_HoverColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnMouseExit() {
        GetComponent<Renderer>().material.color = m_HoverColor;
    }

}
