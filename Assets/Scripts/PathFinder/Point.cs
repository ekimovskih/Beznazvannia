using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public PointsBuilder PB;
    public int x;
    public int y;
    public int cost=0;
    public int value;
    public Point prev;
    //public Point next;

    public void Checkneighbors() //проверяет сосед клетки на возможность изменение путей к ним(не учитывает препятствия)
    {
        Point[,] Grid = PB.Grid;
        int hor = Grid.Length;
        int ver = Grid.GetLength(1);
        for (int i = x-1; i < x+2; i++)
        {
            for (int k = y - 1; k < y + 2; k++)
            {
                if (i == x && k== y)
                {
                    if (i < 0 && i >= hor)
                    {
                        break;
                    }
                    else
                    {
                        if (k >= 0 && k < ver)
                        {
                            Point cur = Grid[i, k];
                            //int val = value + cur.cost;
                            if (cur.value > value + cur.cost)
                            {
                                PB.Grid[i, k].Change(value + cur.cost, this);
                                PB.Grid[i, k].Checkneighbors();
                            }
                        }
                    }
                }
                
            }
        }

    }
    public Point(int hor, int ver, int valuev, PointsBuilder PointBdr)
    {
        x = hor;
        y = ver;
        value = valuev;
        PB = PointBdr;
    }
    void Change(int valuev, Point point)
    {
        value = valuev;
        prev = point;
    }
}
