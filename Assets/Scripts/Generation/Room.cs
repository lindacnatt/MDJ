using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject
{
    public Vector2 position;
    public int roomWidth;
    public int roomHeight;
    public direction enteringCor;

    private System.Random r;


    internal void SetupRoom(Vector2 rw, Vector2 rh, Vector2 max, System.Random random)
    {
        r = random;
        roomWidth = r.Next((int)rw.x,(int)rw.y);
        roomHeight = r.Next((int)rh.x, (int)rh.y);

        position.x = Mathf.RoundToInt(max.x / 2f - roomWidth / 2f);
        position.y = Mathf.RoundToInt(max.y / 2f - roomHeight / 2f);

    }

    internal void SetupRoom(Vector2 rw, Vector2 rh, Vector2 max, Corridor entry, System.Random random)
    {
        enteringCor = entry.dir;

        r = random;
        roomWidth = r.Next((int)rw.x, (int)rw.y);
        roomHeight = r.Next((int)rh.x, (int)rh.y);

        switch (enteringCor)
        {
            case direction.up:
                roomHeight = Mathf.Clamp(roomHeight, 1, (int)max.y - entry.EndPositionY);
                position.y = entry.EndPositionY;
                position.x = r.Next(entry.EndPositionX - roomWidth + 3, entry.EndPositionX);
                position.x = Mathf.Clamp(position.x, 0, max.x - roomWidth-1);
                break;
            case direction.right:
                roomWidth = Mathf.Clamp(roomWidth, 1, (int)max.x);
                position.x = entry.EndPositionX;
                position.y = r.Next(entry.EndPositionY - roomHeight + 3, entry.EndPositionY);
                position.y = Mathf.Clamp(position.y, 0, max.y - roomHeight-1);
                Debug.Log(roomHeight + "   " + roomWidth);
                break;
            case direction.down:
                roomHeight = Mathf.Clamp(roomHeight, 1, entry.EndPositionY);
                position.y = entry.EndPositionY - roomHeight + 1;
                position.x = r.Next(entry.EndPositionX - roomWidth + 3, entry.EndPositionX);
                position.x = Mathf.Clamp(position.x, 0, max.x - roomWidth-1);
                break;
            case direction.left:
                roomWidth = Mathf.Clamp(roomWidth, 1, entry.EndPositionX);
                position.x = entry.EndPositionX - roomWidth + 1;
                position.y = r.Next(entry.EndPositionY - roomHeight + 3, entry.EndPositionY);
                position.y = Mathf.Clamp(position.y, 0, max.y - roomHeight-1);
                break;
        }

    }
}
