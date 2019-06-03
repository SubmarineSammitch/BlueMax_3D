using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileGenerator : MonoBehaviour {

    public GameObject hexTilePreFab;
    public GameObject playerStart;
    public GameObject player2Start;
    ClickableHex Hexpos;

    private Color m_TravelColor;

    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;

    int[,] tiles;
    Node[,] graph;

    public float tileXoffset = 1.8f;
    public float tileZoffset = 1.565f;



    // Start is called before the first frame update
    void Start()
    {
        
        GenerateMapData();
        CreateHexTileMap();
        //basicMovement();
        GeneratePathfindingGraph();
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
        for(int x = 0; x < mapWidth; x++) {
            for(int z = 0; z < mapHeight; z++) {
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
        graph = new Node[mapWidth , mapHeight];


        // Initialize a Node for each spot in the array
        Debug.Log("MapWidth, MapHeight " + mapWidth + "," + mapHeight);
        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapHeight; z++) {
                //Debug.Log("X, Z : " + x +" , "+ z);
                graph[x, z] = new Node();
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapHeight; z++) {

                
                if (graph[x, z].z%2 == 0) {
                    //Debug.Log("IF: "+ graph[x, z].x + "," + graph[x, z].z);
                    // Try left
                    if (x > 0) {
                        //Debug.Log("LEFT IF: " + graph[x, z].x + "," + graph[x, z].z);
                        if (z != mapHeight - 1) {
                            //Debug.Log("LT");
                            graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                        }
                        if (z > 0) {
                            //Debug.Log("LM");
                            graph[x, z].neighbours.Add(graph[x - 1, z]);
                        }
                        if (z <= mapHeight - 1 && z != 0) {
                            //Debug.Log("LL");
                            graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                        }
                    }

                    // Try Right
                    if (x < mapWidth - 1) {
                        //Debug.Log("RIGHT IF: " + graph[x, z].x + "," + graph[x, z].z);
                        if (z != mapHeight - 1) {
                            //Debug.Log("RT");
                            graph[x, z].neighbours.Add(graph[x, z + 1]);
                        }
                        if (z > 0) {
                            graph[x, z].neighbours.Add(graph[x + 1, z]);
                            //Debug.Log("RM");
                        }
                        if (z < mapHeight - 1 && z != 0) {
                            //Debug.Log("RL");
                            graph[x, z].neighbours.Add(graph[x, z - 1]);
                        }
                    }

                }
                else {
                    //Debug.Log("ELSE: "+graph[x, z].x + "," + graph[x, z].z);
                    // Try left
                    if (x > 0) {
                        //Debug.Log("LEFT ELSE: " + graph[x, z].x + "," + graph[x, z].z);
                        if (z != mapHeight - 1) {
                            //Debug.Log("LT");
                            graph[x, z].neighbours.Add(graph[x, z + 1]);
                        }
                        if (z > 0) {
                            //Debug.Log("LM");
                            graph[x, z].neighbours.Add(graph[x - 1, z]);
                        }
                        if (z <= mapHeight - 1 && z != 0) {
                            //Debug.Log("LL");
                            graph[x, z].neighbours.Add(graph[x, z - 1]);
                        }
                    }

                    // Try Right
                    if (x < mapWidth - 1) {
                        //Debug.Log("RIGHT ELSE: " + graph[x, z].x + "," + graph[x, z].z);
                        if (z != mapHeight - 1) {
                            //Debug.Log("RT");
                            graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                        }
                        if (z > 0) {
                            //Debug.Log("RM");
                            graph[x, z].neighbours.Add(graph[x + 1, z]);
                        }
                        if (z <= mapHeight - 1 && z != 0) {
                            //Debug.Log("RL");
                            graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                        }
                    }

                }
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
       // Debug.Log(playerStart.GetComponent<ClickableHex>().tileX + " " + playerStart.GetComponent<ClickableHex>().tileZ);
        player2Start.transform.position = new Vector3(player2Start.GetComponent<Player2>().tileX, 0, player2Start.GetComponent<Player2>().tileZ);
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
