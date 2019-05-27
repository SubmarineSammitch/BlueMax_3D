using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public Vector3 pos;
    public HexTileGenerator map;

    private void Start() {

        pos = GetComponent<Renderer>().bounds.center;
    }
    private void OnMouseUp() {
        Debug.Log("Click");
        Debug.Log(pos);
        map.playerMove(pos);
    }
    
}
