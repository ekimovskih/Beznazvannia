using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsBuilder
{
    public Point[,] Grid;
    public int startPointX;
    public int startPointY;
    public int HorLen;
    public int VerLen;

    public void SetClearGrid()
    { 
        for (int i = 0; i < VerLen; i++)
        {
            for (int k = 0; k < HorLen; k++)
            {
                Grid[k, i] = new Point(k, i, 1000, this);
            }
        }
        FillGid();
    }
    public void SetPoint(int x, int y, Point point)
    {
        Grid[x, y] = point;
    }
    public void FillGid()
    {
        Grid[startPointX, startPointY].value = 0;
        //SetPoint(startPoint.x, startPoint.y, startPoint);
        Grid[startPointX, startPointY].Checkneighbors();
    }
}
