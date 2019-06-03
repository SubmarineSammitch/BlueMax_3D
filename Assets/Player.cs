using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float tileX;
    public float tileZ;
    public HexTileGenerator map;
    public ClickableHex m_ClickHex;
    
    void Start()
    {
        //Debug.Log(m_ClickHex.tileX + " " + m_ClickHex.tileZ);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger event on start "+ other.GetComponent<ClickableHex>().tileX);
    }

    void Update()
    {
    }
}
