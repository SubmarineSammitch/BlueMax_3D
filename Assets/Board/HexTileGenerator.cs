using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileGenerator : MonoBehaviour {

    public GameObject hexTilePreFab;
    public GameObject playerStart;

    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;

    int[,] tiles;

    public float tileXoffset = 1.8f;
    public float tileZoffset = 1.565f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateMapData();
        CreateHexTileMap();
        playerStartPoint();
    }


    void GenerateMapData() {
        //Allocate map tiles
        tiles = new int[mapWidth, mapHeight];
        int x, z;

        //Initaialze map hex tiles
        for (x = 0; x < mapWidth; x++) {
            for (z = 0; z < mapHeight; z++) {
                tiles[x, z] = 0;
            }
        }
    }

    void CreateHexTileMap()
    {
        for(int x = 0; x <= mapWidth; x++) {
            for(int z = 0; z <= mapHeight; z++) {
                GameObject TileGo = Instantiate(hexTilePreFab);

                if (z %2 == 0) {
                    TileGo.transform.position = new Vector3(x * tileXoffset, 0, z * tileZoffset);
                }
                else {
                    TileGo.transform.position = new Vector3(x * tileXoffset + tileXoffset / 2, 0, z * tileZoffset);
                }
                
                ClickableHex ct = TileGo.GetComponent<ClickableHex>();
                ct.tileX = x;
                ct.tileZ = z;
                ct.map = this;

                setTileInfo(TileGo, x, z);
            }
        }
    }
    void setTileInfo(GameObject GO, int x, int z) {

        GO.transform.parent = transform;
        GO.name = x.ToString() +", " + z.ToString();
    }
    void playerStartPoint() {
        playerStart.GetComponent<Player>().tileX = (int)playerStart.transform.position.x;
        playerStart.GetComponent<Player>().tileZ = (int)playerStart.transform.position.z;
        playerStart.GetComponent<Player>().map = this;
    }
}
