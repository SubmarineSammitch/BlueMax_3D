using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileGenerator : MonoBehaviour {

    public GameObject hexTilePreFab;
    public GameObject playerStart;
    public GameObject player2Start;
    ClickableHex Hexpos;

    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;

    int[,] tiles;
    Node[,] graph;

    public float tileXoffset = 1.8f;
    public float tileZoffset = 1.565f;



    // Start is called before the first frame update
    void Start()
    {
        playerStartPoint();
        GenerateMapData();
        CreateHexTileMap();
        //basicMovement();
        GeneratePathfindingGraph();
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

    void GeneratePathfindingGraph() {
        // Initialize the array
        graph = new Node[mapWidth, mapHeight];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapWidth; z++) {
                graph[x, z] = new Node();
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapWidth; z++) {

                // This is the 4-way connection version:
                /*				if(x > 0)
                                    graph[x,y].neighbours.Add( graph[x-1, y] );
                                if(x < mapSizeX-1)
                                    graph[x,y].neighbours.Add( graph[x+1, y] );
                                if(y > 0)
                                    graph[x,y].neighbours.Add( graph[x, y-1] );
                                if(y < mapSizeY-1)
                                    graph[x,y].neighbours.Add( graph[x, y+1] );
                */

                // This is the 8-way connection version (allows diagonal movement)
                // Try left
                if (x > 0) {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                    if (z < mapHeight - 1)
                        graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                }

                // Try Right
                if (x < mapWidth - 1) {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                    if (z < mapHeight - 1)
                        graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                }

                // Try straight up and down
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < mapHeight - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);

                // This also works with 6-way hexes and n-way variable areas (like EU4)
            }
        }


    }

    void setTileInfo(GameObject GO, int x, int z) {
        GO.transform.parent = transform;
        GO.name = x.ToString() +", " + z.ToString();
    }

    void playerStartPoint() {
        //Debug.Log("Loaded: playerStartPoint");
        playerStart.transform.position = new Vector3(playerStart.GetComponent<Player>().tileX,0,playerStart.GetComponent<Player>().tileZ);
        player2Start.transform.position = new Vector3(player2Start.GetComponent<Player2>().tileX, 0, player2Start.GetComponent<Player2>().tileZ);

        //playerStart.GetComponent<Player>().tileX = playerStart.transform.position.x;
        //playerStart.GetComponent<Player>().tileZ = playerStart.transform.position.z;
        //playerStart.GetComponent<Player>().map = this;
    }
    public void player1Move(Vector3 pos) {
        playerStart.transform.position = pos;
    }
    public void player2Move(Vector3 pos) {
        player2Start.transform.position = pos;
    }

    public void basicMovement() {

    }
}
