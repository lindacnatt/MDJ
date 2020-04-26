using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public enum TileType
    {
        wall,floor,
    }


    public GameObject[] wall;
    public GameObject[] floor;

    private readonly int minNumRooms = 15;
    private readonly int maxNumRooms = 20;
    private readonly Vector2 roomwidth = new Vector2(12, 30);
    private readonly Vector2 roomheight = new Vector2(10, 24);
    private readonly Vector2 corridorLength = new Vector2(15, 20);

    private readonly Vector2 MaxSize = new Vector2(200, 150);


    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    public GameObject boardHolder;
    public GameObject[] enemies;

    public GameObject player;
    public GameObject[] boss;

    private System.Random r;

    public List<Room> Rooms;    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("BoardHolder");
        SetupTilesArray();
        CreateRooms();
        SetTilesValues();
        InstantiateTiles();
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
                        tiles[xCoord-2][yCoord] = TileType.floor;
                        tiles[xCoord-1][yCoord] = TileType.floor;
                        tiles[xCoord][yCoord] = TileType.floor;
                        tiles[xCoord+1][yCoord] = TileType.floor;
                        tiles[xCoord+2][yCoord] = TileType.floor;
                        break;
                    case direction.right:
                        xCoord += j;
                        tiles[xCoord][yCoord-2] = TileType.floor;
                        tiles[xCoord][yCoord-1] = TileType.floor;
                        tiles[xCoord][yCoord] = TileType.floor;
                        tiles[xCoord][yCoord+1] = TileType.floor;
                        tiles[xCoord][yCoord+2] = TileType.floor;
                        break;
                    case direction.down:
                        yCoord -= j;
                        tiles[xCoord - 2][yCoord] = TileType.floor;
                        tiles[xCoord - 1][yCoord] = TileType.floor;
                        tiles[xCoord][yCoord] = TileType.floor;
                        tiles[xCoord + 1][yCoord] = TileType.floor;
                        tiles[xCoord + 2][yCoord] = TileType.floor;
                        break;
                    case direction.left:
                        xCoord -= j;
                        tiles[xCoord][yCoord - 2] = TileType.floor;
                        tiles[xCoord][yCoord - 1] = TileType.floor;
                        tiles[xCoord][yCoord] = TileType.floor;
                        tiles[xCoord][yCoord + 1] = TileType.floor;
                        tiles[xCoord][yCoord + 2] = TileType.floor;
                        break;
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


}
