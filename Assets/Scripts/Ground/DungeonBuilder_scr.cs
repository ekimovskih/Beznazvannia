using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder_scr : MonoBehaviour
{

    public GameObject GridBuilderPrefab = null;
    
    public Texture2D[] LevelMaps;
    public Texture2D[] Grids1;
    public Texture2D[] Grids12;
    public Texture2D[] Grids13;
    public Texture2D[] Grids14;
    public Texture2D[] Grids123;
    public Texture2D[] Grids124;
    public Texture2D[] Grids134;
    public Texture2D[] Grids1234;
    public Texture2D[] Grids2;
    public Texture2D[] Grids23;
    public Texture2D[] Grids24;
    public Texture2D[] Grids234;
    public Texture2D[] Grids3;
    public Texture2D[] Grids34;
    public Texture2D[] Grids4;

    public Texture2D[] EnrerGrids1;
    public Texture2D[] EnrerGrids12;
    public Texture2D[] EnrerGrids13;
    public Texture2D[] EnrerGrids14;
    public Texture2D[] EnrerGrids123;
    public Texture2D[] EnrerGrids124;
    public Texture2D[] EnrerGrids134;
    public Texture2D[] EnrerGrids1234;
    public Texture2D[] EnrerGrids2;
    public Texture2D[] EnrerGrids23;
    public Texture2D[] EnrerGrids24;
    public Texture2D[] EnrerGrids234;
    public Texture2D[] EnrerGrids3;
    public Texture2D[] EnrerGrids34;
    public Texture2D[] EnrerGrids4;

    public Texture2D[] ExitGrids1;
    public Texture2D[] ExitGrids12;
    public Texture2D[] ExitGrids13;
    public Texture2D[] ExitGrids14;
    public Texture2D[] ExitGrids123;
    public Texture2D[] ExitGrids124;
    public Texture2D[] ExitGrids134;
    public Texture2D[] ExitGrids1234;
    public Texture2D[] ExitGrids2;
    public Texture2D[] ExitGrids23;
    public Texture2D[] ExitGrids24;
    public Texture2D[] ExitGrids234;
    public Texture2D[] ExitGrids3;
    public Texture2D[] ExitGrids34;
    public Texture2D[] ExitGrids4;

    public Texture2D CurrentMap = null;
    public GameObject[,] Grids;

    private int MapWidth;
    private int MapHigh;


    void Start()
    {
        int map = Random.Range(0, LevelMaps.Length - 1);
        CurrentMap = LevelMaps[map];
        MapWidth = CurrentMap.width;
        MapHigh = CurrentMap.height;
        Grids = new GameObject[MapWidth, MapHigh];
        for (int x= 0; x < MapWidth;x++)
        {
            for (int y = 0; y < MapHigh; y++)
            {
                Grids[x, y] = Instantiate(GridBuilderPrefab, this.transform);
                IdentyfyRoom(x, y);
            }
        }
    }

    public void IdentyfyRoom(int x,int y)
    {
        int state = 0;
        Color Cur = CurrentMap.GetPixel(x, y);
        float blue = Cur.b;
        float red = Cur.r;
        float green = Cur.g;
        {
            if (green == 1 && red == 1 && blue == 1)
            {
                Destroy(Grids[x, y]);
                return;
            }
            if (x + 1 < MapWidth && CurrentMap.GetPixel(x + 1, y).g == 0)
            {
                state = 1;
            }
            if (y - 1 >= 0 && CurrentMap.GetPixel(x, y - 1).g == 0)
            {
                state = state * 10 + 2;
            }
            if (x - 1 >= 0 && CurrentMap.GetPixel(x - 1, y).g == 0)
            {
                state = state * 10 + 3;
            }
            if (y + 1 < MapWidth && CurrentMap.GetPixel(x, y + 1).g == 0)
            {
                state = state * 10 + 4;
            }
        }
        Texture2D[] ExPortal =null, EnPortal =null, Room=null;
        switch (state)
        {
            case 1:
                Room = Grids1;
                EnPortal = EnrerGrids1;
                ExPortal = ExitGrids1;
                break;
            case 12:
                Room = Grids12;
                EnPortal = EnrerGrids12;
                ExPortal = ExitGrids12;
                break;
            case 13:
                Room = Grids13;
                EnPortal = EnrerGrids13;
                ExPortal = ExitGrids13;
                break;
            case 14:
                Room = Grids14;
                EnPortal = EnrerGrids14;
                ExPortal = ExitGrids14;
                break;
            case 123:
                Room = Grids123;
                EnPortal = EnrerGrids123;
                ExPortal = ExitGrids123;
                break;
            case 124:
                Room = Grids124;
                EnPortal = EnrerGrids124;
                ExPortal = ExitGrids124;
                break;
            case 134:
                Room = Grids134;
                EnPortal = EnrerGrids134;
                ExPortal = ExitGrids134;
                break;
            case 1234:
                Room = Grids1234;
                EnPortal = EnrerGrids1234;
                ExPortal = ExitGrids1234;
                break;
            case 2:
                Room = Grids2;
                EnPortal = EnrerGrids2;
                ExPortal = ExitGrids2;
                break;
            case 23:
                Room = Grids23;
                EnPortal = EnrerGrids23;
                ExPortal = ExitGrids23;
                break;
            case 24:
                Room = Grids24;
                EnPortal = EnrerGrids24;
                ExPortal = ExitGrids24;
                break;
            case 234:
                Room = Grids234;
                EnPortal = EnrerGrids234;
                ExPortal = ExitGrids234;
                break;
            case 3:
                Room = Grids3;
                EnPortal = EnrerGrids3;
                ExPortal = ExitGrids3;
                break;
            case 34:
                Room = Grids34;
                EnPortal = EnrerGrids34;
                ExPortal = ExitGrids34;
                break;
            case 4:
                Room = Grids4;
                EnPortal = EnrerGrids4;
                ExPortal = ExitGrids4;
                break;
        }
        if (blue == 1)
        {
            //Debug.Log("Blue" + blue + " red" + red + " green" + green + " state" + state);
            //Debug.Log(Grids[x, y]);
            //Grids[x, y] = Grids[x, y].AddComponent< GridBuilder_scr(ExPortal[Random.Range(0, ExPortal.Length - 1)], x, y, Grids) > as GridBuilder_scr;
            Grids[x, y].GetComponent<GridBuilder_scr>().Set(ExPortal[Random.Range(0, ExPortal.Length - 1)], x, y, Grids);
            Grids[x, y].SetActive(false);
            return;

        }
        if (red == 1)
        {
            //Debug.Log("Blue" + blue + " red" + red + " green" + green + " state" + state);
            Grids[x, y].GetComponent<GridBuilder_scr>().Set(EnPortal[Random.Range(0, EnPortal.Length - 1)], x, y, Grids);
            Grids[x, y].SetActive(true);
            GameObject.Find("Cursor").GetComponent<Cursor_scr>().GridBuilder = Grids[x, y];
            //Grids[x, y] = new GridBuilder_scr(EnPortal[Random.Range(0, EnPortal.Length - 1)], x, y, Grids);
            return;
        }
        else
        {
            //Debug.Log("Blue" + blue + " red" + red + " green" + green + " state" + state);
            Grids[x, y].GetComponent<GridBuilder_scr>().Set(Room[Random.Range(0, Room.Length - 1)], x, y, Grids);
            Grids[x, y].SetActive(false);
            //Grids[x, y] = new GridBuilder_scr(Room[Random.Range(0, Room.Length - 1)], x, y, Grids);
            return;
        }
    }
    /*
    void Unactivate(int xx, int yy)
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHigh; y++)
            {
                if (xx == x && yy == y)
                {
                    Grids[x, y].SetActive(true);
                }
                else
                {
                    Grids[x, y].SetActive(false);
                }
            }
        }
    }
    */
}
