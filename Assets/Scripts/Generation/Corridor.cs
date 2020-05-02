using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    up,left,down,right,
}
public class Corridor : ScriptableObject
{

    public int startXpos;
    public int startYpos;
    public int corridorLength;
    public direction dir;

    private System.Random r;


    public int EndPositionX
    {
        get
        {
            if (dir == direction.up || dir == direction.down) return startXpos;
            if (dir == direction.right) return startXpos + corridorLength -1;
            return startXpos - corridorLength + 1;
        }
    }

    public int EndPositionY
    {
        get
        {
            if (dir == direction.left || dir == direction.right) return startYpos;
            if (dir == direction.up) return startYpos + corridorLength - 1;
            return startYpos - corridorLength + 1;
        }
    }

    private void changeDir()
    {
        int di = (int)dir;
        di++;
        di = di % 4;
        dir = (direction)di;
    }
    internal void SetupCorridor(Room room, Vector2 cl, Vector2 rmwid, Vector2 rmhgt, Vector2 maxSize, System.Random random, bool first)
    {
        bool good = false;
        r = random;
        dir = (direction)r.Next(0, 4);
        direction oposite = (direction)(((int)room.enteringCor + 2) % 4);

        if(!first && dir == oposite) changeDir();
        
        while (!good)
        {
            switch (dir)
            {
                case direction.up:
                    if (room.position.y + (room.roomHeight + 1 + rmhgt.y) > maxSize.y + 1)
                    {
                        changeDir();
                    }
                    else good = true;
                    break;
                case direction.right:
                    if (room.position.x + (room.roomWidth +1 + rmwid.y)  > maxSize.x +1)
                    {
                        changeDir();
                    }
                    else good = true;
                    break;
                case direction.down:
                    if (room.position.y < rmhgt.y + 2)
                    {
                        changeDir();
                    }
                    else good = true;
                    break;
                case direction.left:
                    if (room.position.x < rmwid.y + 2)
                    {
                        changeDir();
                    }
                    else good = true;
                    break;
            }
        }
        
                corridorLength = r.Next((int)cl.x,(int) cl.y);

        int maxLength = (int)cl.y;

        switch (dir)
        {
            case direction.up:
                if((int)room.position.x + 2 > (int)room.position.x + room.roomWidth - 3) startXpos = r.Next((int)room.position.x + room.roomWidth - 3, (int)room.position.x + 2);
                else startXpos = r.Next((int)room.position.x+2, (int)room.position.x + room.roomWidth - 3);
                startYpos = (int) room.position.y + room.roomHeight;
                maxLength = (int) (maxSize.y - startYpos - rmhgt.y);
                break;
            case direction.right:
                startXpos = (int)room.position.x + room.roomWidth;
                if ((int)room.position.y + 2 > (int)room.position.y + room.roomHeight - 3) startXpos = r.Next((int)room.position.y + room.roomHeight - 3, (int)room.position.y + 2);
                else startYpos = r.Next((int)room.position.y+2, (int)room.position.y + room.roomHeight - 3);
                maxLength = (int)(maxSize.x - startXpos - rmwid.y);
                break;
            case direction.down:
                if ((int)room.position.x + 2 > (int)room.position.x + room.roomWidth - 3) startXpos = r.Next((int)room.position.x + room.roomWidth - 3, (int)room.position.x + 2);
                else startXpos = r.Next((int)room.position.x+2, (int)room.position.x +room.roomWidth - 3);
                startYpos = (int)room.position.y;
                maxLength = (int)(startYpos - rmhgt.y);
                break;
            case direction.left:
                startXpos = (int)room.position.x;
                startYpos = r.Next((int)room.position.y+2, (int)room.position.y + room.roomHeight - 3);
                maxLength = (int)(startXpos - rmwid.y);
                break;
        }
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}
