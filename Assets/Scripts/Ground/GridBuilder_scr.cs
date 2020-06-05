using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder_scr : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject Cell = null;
    public int GridWidth = 2;
    public int GridHigh = 2;
    public bool rebuildable = true;
    public bool builded = false;
    public string Location = "Vilage";
    public Texture2D LevelMap;
    public GameObject[] LocationCells;

    public Sprite[] Textures = new Sprite[2];

    private GameObject[,] Grid;
    void Start()
    {
        if (!builded)
        {
            GameObject thisObject = this.gameObject;
            GridWidth = LevelMap.width;
            GridHigh = LevelMap.height;
            Grid = new GameObject[GridWidth, GridHigh];
            
            BuildGrid();
        }
    }
    void BuildGrid()
    {
        GameObject thisObject = this.gameObject;
        GridWidth = LevelMap.width;
        GridHigh = LevelMap.height;
        Debug.Log(GridWidth);
        Grid = new GameObject[GridWidth, GridHigh];
        float red, green, blue;

        for (int i = 0; i < GridHigh; i++)
        {
            for (int j = 0; j < GridWidth; j++)
            {
                int CellNum;
                red = (LevelMap.GetPixel(i, j).r);
                green = (LevelMap.GetPixel(i, j).g);
                blue = (LevelMap.GetPixel(i, j).b);

                if (Mathf.Max(green, Mathf.Max(red, blue)) == 0)
                {
                    CellNum = 0 + CellType(i, j);
                }
                else if (blue ==0 / 255f && red==255 / 255f && green == 255 / 255f)
                {
                    CellNum = 3;
                }
                else
                {
                    CellNum = 4;
                }
                //Debug.Log(i + "  " + j + "  " + CellNum);
                Grid[j, i] = Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform);
                Grid[j, i].GetComponent<Cell_class>().Position = new Vector2(i, j);
            }
            //Debug.Log(i);
        }
    }
    

    int CellType(int i, int j)
    {
        //float cur;
        float u1=1, u2=1,u3=1,m1=1,m3=1,d1=1,d2=1,d3=1;
        if(j < GridHigh)
        {
            if (i > 0)
            {
                u1 = LevelMap.GetPixel(i-1, j+1).r;
            }
            if (i < GridWidth)
            {
                u3 = LevelMap.GetPixel(i+1, j+1).r;
            }
            u2 = LevelMap.GetPixel(i, j +1).r; ;
        }
        if (i > 0)
        {
            m1 = LevelMap.GetPixel(i - 1, j).r;
        }
        if (i < GridWidth)
        {
            m3 = LevelMap.GetPixel(i + 1, j).r;
        }
        if (j > 0)
        {
            if (i > 0)
            {
                d1 = LevelMap.GetPixel(i - 1, j - 1).r;
            }
            if (i < GridWidth)
            {
                d3 = LevelMap.GetPixel(i + 1, j - 1).r;
            }
            d2 = LevelMap.GetPixel(i, j - 1).r; ;
        }






        if (Mathf.Max(u1, u2 , u3 , m1 , m3, d1, d2, d3) == 0 || i==0||j==0||i == GridWidth||j== GridHigh)
        {
            return 2;
        }
        if (u2 != 0)
        {
            if (0 != m3)
            {
                return 10;
            }
            if (m1 != 0)
            {
                return 9;
            }
            return 1;
        }
        if (d2 != 0)
        {
            if (0 != m3)
            {
                return 8;
            }
            if (m1 != 0)
            {
                return 7;
            }
            return 0;
        }
        if (m3 != 0)
        {
            return 6;
        }
        if (m1 != 0)
        {
            return 5;
        }

        return 2;


        /*
        {
            float red = (LevelMap.GetPixel(i, j).r);
            float redup = (LevelMap.GetPixel(i, j - 1).r);
            if (red != redup)
            {
                return 0;
            }
        }
        if (j < GridHigh-1)
        {
            float red = (LevelMap.GetPixel(i, j).r);
            float redup = (LevelMap.GetPixel(i, j + 1).r);
            if (red != redup)
            {
                return 1;
            }
        }
        return 2;*/
    }
    public void PutOnCell(GameObject obj, int high, int width)
    {
        if (width>=GridWidth || high >= GridHigh)
        {
            Debug.Log("Out of grid");
        }
        else
        {
            Grid[width, high].GetComponent<Cell_class>().PutObj(obj);
        }
    }
    public void GetFromCell(int high, int width, Drop_scr InHand)
    {
        if (width >= GridWidth || high >= GridHigh)
        {
            Debug.Log("Out of grid");
        }
        else
        {
            Grid[width, high].GetComponent<Cell_class>().GetObj(InHand);
        }
    }
}
