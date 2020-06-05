using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder_scr : MonoBehaviour
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
            //GetComponent<GridLayout>()
            BuildGrid();
        }
    }
    void BuildGrid()
    {
        GameObject thisObject = this.gameObject;
        GridWidth = LevelMap.width;
        GridHigh = LevelMap.height;
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
                else if (blue == 0 / 255f && red == 255 / 255f && green == 255 / 255f)
                {
                    CellNum = 3;
                }
                else
                {
                    CellNum = 4;
                }
                Grid[j, i] = Instantiate(LocationCells[CellNum], thisObject.transform);
                Grid[j, i].GetComponent<Cell_class>().Position = new Vector2(i, j);
            }
            //Debug.Log(i);
        }
    }


    int CellType(int i, int j)
    {
        if (j > 0)
        {
            float red = (LevelMap.GetPixel(i, j).r);
            float redup = (LevelMap.GetPixel(i, j - 1).r);
            if (red != redup)
            {
                return 0;
            }
        }
        if (j < GridHigh - 1)
        {
            float red = (LevelMap.GetPixel(i, j).r);
            float redup = (LevelMap.GetPixel(i, j + 1).r);
            if (red != redup)
            {
                return 1;
            }
        }
        return 2;
    }
    public void PutOnCell(GameObject obj, int high, int width)
    {
        if (width >= GridWidth || high >= GridHigh)
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
