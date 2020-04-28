using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Generator : MonoBehaviour
{
    public enum TileType
    {
        wall,floor,
    }

    public GameObject[] wall;
    public GameObject[] floor;

    private readonly int corridorWidth = 2;
    private readonly int minNumRooms = 15;
    private readonly int maxNumRooms = 20;
    private readonly Vector2 roomwidth = new Vector2(12, 30);
    private readonly Vector2 roomheight = new Vector2(10, 24);
    private readonly Vector2 corridorLength = new Vector2(15, 20);

    private readonly Vector2 MaxSize = new Vector2(200, 150);

    private readonly Vector2 numEnemies = new Vector2(18,20);
    private readonly int numChests = 4;

    public Tile[] floorTile;
    public Tile[] wallTile;

    public Tilemap floorTM;
    public Tilemap wallTM;

    private TileType[][] tiles;

    private Room[] rooms;
    private Corridor[] corridors;
    public GameObject boardHolder;

    public GameObject[] chests;

    public GameObject[] enemiesPrefab;
    public List<GameObject> enemies;

    public GameObject player;
    public GameObject[] boss;

    private System.Random r;

    public List<Room> Rooms;    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        boardHolder = new GameObject("BoardHolder");
        SetupTilesArray();
        CreateRooms();
        SetTilesValues();
        InstantiateTiles2();

        CreateEnemies();
    }

    void SetupTilesArray()
    {
        tiles = new TileType[(int)MaxSize.x+2][];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[(int)MaxSize.y+2];
        }
    }

    void CreateRooms()
    {
        r = new System.Random();
        rooms = new Room[r.Next(minNumRooms, maxNumRooms)];
        corridors = new Corridor[rooms.Length - 1];
        rooms[0] = new Room();
        corridors[0] = new Corridor();
        rooms[0].SetupRoom(roomwidth, roomheight, MaxSize, r);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomwidth, roomheight, MaxSize, r, true);
        for(int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomwidth, roomheight, MaxSize, corridors[i - 1], r);
            if( i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomwidth, roomheight, MaxSize, r, false);
            }

            if(i == (int)(rooms.Length * 0.5))
            {
                Vector3 playerPos = new Vector3(rooms[i].position.x + (int)(rooms[i].roomWidth/2), rooms[i].position.y + (int)(rooms[i].roomHeight / 2), -1);
                Instantiate(player, playerPos, Quaternion.identity);
            }
        }

        Room farthest = rooms[0];
        float d = 0;
        for (int i = 1; i < rooms.Length; i++)
        {
            if (Vector2.Distance(rooms[i].position,player.transform.position)>d)
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

    private void singleCorridor1(int x, int y)
    {
        for(int i = -corridorWidth; i <= corridorWidth ; i++)
        {
            tiles[x][y + i] = TileType.floor;
        }
    }

    private void singleCorridor2(int x, int y)
    {
        for (int i = -corridorWidth; i <= corridorWidth; i++)
        {
            tiles[x+i][y] = TileType.floor;
        }
    }

    void InstantiateTiles2()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i][1] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(i, 0, 0), wallTile[r.Next(0, wall.Length)]);
            }
            if (tiles[i][tiles[0].Length - 2] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(i, tiles[0].Length - 1, 0), wallTile[r.Next(0, wall.Length)]);
            }
        }
        for (int i = 0; i < tiles[0].Length; i++)
        {
            if (tiles[1][i] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(0, i, 0), wallTile[r.Next(0, wall.Length)]);
            }
            if (tiles[tiles.Length - 2][i] == TileType.floor)
            {
                wallTM.SetTile(new Vector3Int(tiles.Length - 1, i, 0), wallTile[r.Next(0, wall.Length)]);
            }
        }
        for (int i = 1; i < tiles.Length - 1; i++)
        {
            for (int j = 1; j < tiles[i].Length - 1; j++)
            {
                if (tiles[i][j] == TileType.floor)
                {
                    floorTM.SetTile(new Vector3Int(i, j, 0), floorTile[r.Next(0, wall.Length)]);
                    if (tiles[i + 1][j] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i + 1, j, 0), wallTile[r.Next(0, wall.Length)]);
                    }
                    if (tiles[i - 1][j] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i - 1, j, 0), wallTile[r.Next(0, wall.Length)]);
                    }
                    if (tiles[i][j + 1] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i, j + 1, 0), wallTile[r.Next(0, wall.Length)]);
                    }
                    if (tiles[i][j - 1] != TileType.floor)
                    {
                        wallTM.SetTile(new Vector3Int(i, j - 1, 0), wallTile[r.Next(0, wall.Length)]);
                    }

                }
            }
        }
    }
    void InstantiateTiles()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i][1] == TileType.floor)
            {
                GameObject g = Instantiate(wall[r.Next(0,wall.Length)], new Vector3(i, 0,-1), Quaternion.identity);
                g.transform.parent = boardHolder.transform;
            }
            if (tiles[i][tiles[0].Length - 2] == TileType.floor)
            {
                GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(i, tiles[0].Length-1,-1), Quaternion.identity);
                g.transform.parent = boardHolder.transform;
            }
        }
        for (int i = 0; i < tiles[0].Length; i++)
        {
            if (tiles[1][i] == TileType.floor)
            {
                GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(0,i,-1), Quaternion.identity);
                g.transform.parent = boardHolder.transform;
            }
            if (tiles[tiles.Length - 2][i] == TileType.floor)
            {
                GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(tiles.Length - 1,i,-1), Quaternion.identity);
                g.transform.parent = boardHolder.transform;
            }
        }
        for (int i = 1; i < tiles.Length-1; i++)
        {
            for (int j = 1; j < tiles[i].Length-1; j++)
            {
                if (tiles[i][j] == TileType.floor)
                {
                    GameObject g1 = Instantiate(floor[r.Next(0, floor.Length)], new Vector3(i, j,0), Quaternion.identity);
                    g1.transform.parent = boardHolder.transform;
                    if (tiles[i + 1][j] != TileType.floor) {
                        GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(i+1, j,-1), Quaternion.identity);
                        g.transform.parent = boardHolder.transform;
                    }
                    if(tiles[i - 1][j] != TileType.floor)
                    {
                        GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(i-1, j,-1), Quaternion.identity);
                        g.transform.parent = boardHolder.transform;
                    }
                    if (tiles[i][j + 1] != TileType.floor)
                    {
                        GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(i, j+1,-1), Quaternion.identity);
                        g.transform.parent = boardHolder.transform;
                    }
                    if (tiles[i][j - 1] != TileType.floor)
                    {
                        GameObject g = Instantiate(wall[r.Next(0, wall.Length)], new Vector3(i, j-1,-1), Quaternion.identity);
                        g.transform.parent = boardHolder.transform;
                    }
                    
                }
            }
        }
    }
    
    private bool noEnemy(int x, int y)
    {
        if (Vector2.Distance(player.transform.position, new Vector2(x, y)) < 8)
        {
            return false;
        }

        foreach (GameObject g in enemies)
        {
            if (Vector2.Distance(g.transform.position,new Vector2(x,y)) < 3)
            {
                
                return false;
            }
            
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
        int currChests = 0;
        while (numChests > currChests)
        {
            int xPos = r.Next(1, (int)MaxSize.x);
            int yPos = r.Next(1, (int)MaxSize.y);

            if (tiles[xPos][yPos] == TileType.floor && noEnemy(xPos, yPos) && nearWall(xPos,yPos))
            {
                putChest(xPos, yPos);
                currChests--;
            }
        }
    }

    private void putChest(int x, int y)
    {
        int chest = r.Next(0, 101);
        int typeChest = 0;

        if (chest > 90) typeChest = 4;
        else if (chest > 75) typeChest = 3;
        else if (chest > 50) typeChest = 2;
        else if (chest > 25) typeChest = 1;

        Instantiate(chests[typeChest], new Vector3(x, y, -1), Quaternion.identity);
    }

    private bool nearWall(int x,int y)
    {
        return tiles[x + 1][y] == TileType.wall || tiles[x - 1][y] == TileType.wall ||
            tiles[x][y + 1] == TileType.wall || tiles[x][y - 1] == TileType.wall;
    }
}
