using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using static Chest;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    public enum TileType{wall,floor,}

    private readonly Vector2Int corridorWidth = new Vector2Int(1, 4);
    private int corridorW = 0;
    private readonly Vector2Int numRooms = new Vector2Int(15,20);
    private readonly Vector2Int roomwidth = new Vector2Int(15, 25);
    private readonly Vector2Int roomheight = new Vector2Int(15, 20);
    private readonly Vector2Int corridorLength = new Vector2Int(15, 20);
    private readonly Vector2Int MaxSize = new Vector2Int(200, 150);

    private readonly Vector2Int numEnemies = new Vector2Int(18,25);

    private readonly Vector2Int numChests = new Vector2Int(5,8);

    public Tile[] floorTile;

    public Tile[] wallTileUp;
    public Tile[] wallTileDown;
    public Tile[] wallTileRight;
    public Tile[] wallTileLeft;

    public Tilemap floorTM;
    public Tilemap wallTM;

    private TileType[][] tiles;

    private Room[] rooms;
    private Corridor[] corridors;

    public GameObject[] enemiesPrefab;
    public List<GameObject> enemies;

    public GameObject player;
    public GameObject[] boss;

    public GameObject[] chests;

    public static System.Random r;

    public List<Room> Rooms;    // Start is called before the first frame update

    public int sceneIndex;

    [SerializeField] private NavMeshSurface2d navMesh = null;
    void Start()
    {

        

        enemies = new List<GameObject>();
        SetupTilesArray();
        CreateRooms();
        SetTilesValues();
        InstantiateTiles();

        navMesh.BuildNavMesh();

        CreateEnemies();

        CreateChests();

    }

    void SetupTilesArray()
    {
        //Instantiate
        tiles = new TileType[(int)MaxSize.x+2][];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[(int)MaxSize.y+2];
        }
    }

    void CreateRooms()
    {
        r = new System.Random();
        rooms = new Room[r.Next(numRooms.x, numRooms.y)];
        corridors = new Corridor[rooms.Length - 1];
        //first room
        rooms[0] = new Room();
        corridors[0] = new Corridor();
        rooms[0].SetupRoom(roomwidth, roomheight, MaxSize, r);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomwidth, roomheight, MaxSize, r, true);

        for(int i = 1; i < rooms.Length; i++)
        {
            //create room based on previous corridor
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomwidth, roomheight, MaxSize, corridors[i - 1], r);
            if( i < corridors.Length)
            {
                //create corridor 
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomwidth, roomheight, MaxSize, r, false);
            }

            if(i == (int)(rooms.Length * 0.5))
            {
                //instantiate player in the middle room
                Vector3 playerPos = new Vector3(rooms[i].position.x + (int)(rooms[i].roomWidth/2), rooms[i].position.y + (int)(rooms[i].roomHeight / 2), -1);
                Instantiate(player, playerPos, Quaternion.identity);
                Destroy(player);
            }
        }

        //instantiate boss on the furstest room feeling dumb might delete later
        Room farthest = rooms[0];
        float d = 0;
        for (int i = 1; i < rooms.Length; i++)
        {
            if (Vector2.Distance(rooms[i].position,player.transform.position) > d)
            {
                d = Vector2.Distance(rooms[i].position, player.transform.position);
                farthest = rooms[i];
            }
        }
        Vector3 bossPos = new Vector3(farthest.position.x + (int)(farthest.roomWidth / 2), farthest.position.y + (int)(farthest.roomHeight / 2), -1);
        Instantiate(boss[r.Next(0,boss.Length)], bossPos, Quaternion.identity);
    }

    
     void SetTilesValues()
    {
        //rooms
        for(int i = 0; i < rooms.Length; i++)
        {
            Room current = rooms[i];
            for(int j = 0; j < current.roomWidth; j++)
            {
                int xCoord = (int)current.position.x + j;
                for(int k = 0; k <current.roomHeight; k++)
                {
                    int yCoord = (int)current.position.y + k;
                    tiles[xCoord][yCoord] = TileType.floor;
                }
            }
        }

        //corridors
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor current = corridors[i];
            corridorW = r.Next(corridorWidth.x, corridorWidth.y);
            for (int j = 0; j < current.corridorLength; j++)
            {
                int xCoord = current.startXpos;
                int yCoord = current.startYpos;
                switch (current.dir)
                {
                    case direction.up:
                        yCoord += j;
                        singleCorridor2(xCoord, yCoord);
                        break;
                    case direction.right:
                        xCoord += j;
                        singleCorridor1(xCoord, yCoord);
                        break;
                    case direction.down:
                        yCoord -= j;
                        singleCorridor2(xCoord, yCoord);
                        break;
                    case direction.left:
                        xCoord -= j;
                        singleCorridor1(xCoord, yCoord);
                        break;
                }
            }
        }
    }

    //instantiate the sides of corridors
    private void singleCorridor1(int x, int y)
    {
        for(int i = -corridorW; i <= corridorW; i++)
        {
            tiles[x][y + i] = TileType.floor;
        }
    }

    private void singleCorridor2(int x, int y)
    {
        for (int i = -corridorW; i <= corridorW; i++)
        {
            tiles[x+i][y] = TileType.floor;
        }
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i][1] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(i, 0, 0), wallTileUp[r.Next(0, wallTileUp.Length)]);
            }
            if (tiles[i][tiles[0].Length - 2] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(i, tiles[0].Length - 1, 0), wallTileDown[r.Next(0, wallTileDown.Length)]);
            }
        }
        for (int i = 0; i < tiles[0].Length; i++)
        {
            if (tiles[1][i] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(0, i, 0), wallTileLeft[r.Next(0, wallTileLeft.Length)]);
            }
            if (tiles[tiles.Length - 2][i] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(tiles.Length - 1, i, 0), wallTileRight[r.Next(0, wallTileRight.Length)]);
            }
        }
        for (int i = 1; i < tiles.Length - 1; i++)
        {
            for (int j = 1; j < tiles[i].Length - 1; j++)
            {
                if (tiles[i][j] == TileType.floor)
                {
                    floorTM.SetTile(new Vector3Int(i, j, 0), floorTile[r.Next(0, floorTile.Length)]);
                    if (tiles[i + 1][j] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i + 1, j, 0), wallTileRight[r.Next(0, wallTileRight.Length)]);
                    }
                    if (tiles[i - 1][j] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i - 1, j, 0), wallTileLeft[r.Next(0, wallTileLeft.Length)]);
                    }
                    if (tiles[i][j + 1] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i, j + 1, 0), wallTileUp[r.Next(0, wallTileUp.Length)]);
                    }
                    if (tiles[i][j - 1] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i, j - 1, 0), wallTileDown[r.Next(0, wallTileDown.Length)]);
                    }

                }
            }
        }
    }
    
    //check if the position is apropriate
    private bool noEnemy(int x, int y)
    {
        if (Vector2.Distance(player.transform.position, new Vector2(x, y)) < 8) return false;

        foreach (GameObject g in enemies)
        {
            if (Vector2.Distance(g.transform.position,new Vector2(x,y)) < 3)   return false; 
        }
        return true;
    }

    void CreateEnemies()
    {
        int currNumEnemies = r.Next((int)numEnemies.x,(int)numEnemies.y);

        while(currNumEnemies > 0)
        {
            int xPos = r.Next(1, (int)MaxSize.x);
            int yPos = r.Next(1, (int)MaxSize.y);

            if (tiles[xPos][yPos] == TileType.floor && noEnemy(xPos,yPos))
            {
                GameObject g = Instantiate(enemiesPrefab[r.Next(0,enemiesPrefab.Length)], new Vector3(xPos, yPos, -1), Quaternion.identity);
                enemies.Add(g);
                currNumEnemies--;
            }
            
        }
    }

    void CreateChests()
    {
        int numeroChests = r.Next(numChests.x, numChests.y);
        while (numeroChests > 0)
        {
            int xPos = r.Next(1, (int)MaxSize.x);
            int yPos = r.Next(1, (int)MaxSize.y);
            //check if near wall, on the floor and no enemies on top
            if (tiles[xPos][yPos] == TileType.floor && noEnemy(xPos, yPos) && nearWall(xPos,yPos))
            {
                //instantiate the chest
                int n = r.Next(0, 100);
                GameObject g;
                if (n > 80)
                {
                    g = Instantiate(chests[4], new Vector3(xPos, yPos, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.golden);
                }
                else if (n > 65)
                {
                    g = Instantiate(chests[3], new Vector3(xPos, yPos, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.purple);
                }
                else if (n > 40)
                {
                    g = Instantiate(chests[2], new Vector3(xPos, yPos, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.blue);
                }
                else if (n > 10)
                {
                    g = Instantiate(chests[1], new Vector3(xPos, yPos, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.green);
                }
                else
                {
                    g = Instantiate(chests[0], new Vector3(xPos, yPos, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.brown);
                }
                numeroChests--;
            }
        }
    }


    private bool nearWall(int x,int y)
    {
        return tiles[x + 1][y] == TileType.wall || tiles[x - 1][y] == TileType.wall ||
            tiles[x][y + 1] == TileType.wall || tiles[x][y - 1] == TileType.wall;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
