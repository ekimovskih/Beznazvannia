using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder_scr : MonoBehaviour
{
    public int GridWidth = 2;
    public int GridHigh = 2;
    //public bool rebuildable = true;
    //public bool builded = false;
    //public string Location = "Vilage";
    public Texture2D RoomMap;
    public GameObject[] LocationCells;
    private GameObject[,] LocationGrids;
    private int xONmap;
    private int yONmap;
    public Vector3 door1;
    private Vector3 door2;
    private Vector3 door3;
    private Vector3 door4;
    private Vector2[] enemyCells = new Vector2[30];
    private int enemyCellsIndex = 0;
    public GameObject arrowRight;
    public GameObject arrowDown;
    public GameObject arrowLeft;
    public GameObject arrowUp;
    public GameObject redportal;
    public GameObject blueportal;


    public Sprite[] Textures = new Sprite[2];

    private GameObject[,] Grid;
    private void Start()
    {
        //Build();
    }
    public void TeleportPlayer(int  door)
    {
        GameObject Next = null;
        GameObject player = GameObject.Find("Player");
        switch (door) 
        {
            case 1:
                Next = LocationGrids[xONmap + 1, yONmap];
                player.transform.position = Next.GetComponent<GridBuilder_scr>().door3;// + Vector2.right;
                //Debug.Log(door3);
                Next.SetActive(true);
                
                break;
            case 2:
                Next = LocationGrids[xONmap, yONmap-1];
                player.transform.position = Next.GetComponent<GridBuilder_scr>().door4;// + Vector2.down;
                //Debug.Log(door3);
                Next.SetActive(true);
                break;
            case 3:
                Next = LocationGrids[xONmap -1, yONmap];
                player.transform.position = Next.GetComponent<GridBuilder_scr>().door1;// + Vector2.left;
                Debug.Log(player.transform.position);
                Next.SetActive(true);
                break;
            case 4:
                Next = LocationGrids[xONmap, yONmap+1];
                player.transform.position = Next.GetComponent<GridBuilder_scr>().door2+Vector3.up;
                //Debug.Log(new Vector3 f = Vector3.up + Next.GetComponent<GridBuilder_scr>().door2);
                Next.SetActive(true);
                break;
            default:
                Debug.Log("Выхода нет, Выходааа неееет");
                return;
        }
        player.transform.position += player.GetComponent<Player_movement_scr>().PlayerContainer.position;
        GameObject.Find("Cursor").GetComponent<Cursor_scr>().GridBuilder = Next;
        Next.GetComponent<GridBuilder_scr>().LightUp();
        this.gameObject.SetActive(false);

    }

    public void Set(Texture2D spr, int x, int y, GameObject[,] FullMap)
    {
        RoomMap = spr;
        LocationGrids = FullMap;
        xONmap = x;
        yONmap = y;
        Build();
        //Debug.Log(enemyCellsIndex + "   " + RoomMap.name);
        
    }
    public void LightUp()
    {
        StartCoroutine(GameObject.Find("DarkScreen").GetComponent<DarkScreen>().Lighter());
    }
    void Build()
    {
        if (RoomMap!=null)
        {
            GameObject thisObject = this.gameObject;
            GridWidth = RoomMap.width;
            GridHigh = RoomMap.height;
            Grid = new GameObject[GridWidth, GridHigh];
            
            BuildGrid();
        }
    }
    void BuildGrid()
    {
        GameObject thisObject = this.gameObject;
        GridWidth = RoomMap.width;
        GridHigh = RoomMap.height;
        //Debug.Log(GridWidth);
        Grid = new GameObject[GridWidth, GridHigh];
        float red, green, blue;

        for (int i = 0; i < GridHigh; i++)
        {
            for (int j = 0; j < GridWidth; j++)
            {
                int CellNum;
                red = (RoomMap.GetPixel(i, j).r);
                green = (RoomMap.GetPixel(i, j).g);
                blue = (RoomMap.GetPixel(i, j).b);

                if (Mathf.Max(green, Mathf.Max(red, blue)) == 0) //черный стены & со
                {
                    CellNum = 0 + CellType(i, j); // 
                }
                else if (blue ==0 && red==1 && green == 1) //желтый  пол с обьектом
                {
                    CellNum = 10;
                }
                else if (blue == 0 && red == 0 && green == 128 / 255f) //темно зеленый дверь 1
                {
                    if(door1 == Vector3.zero)
                    {
                        door1 = new Vector3(i, j,0);
                        CellNum = 12;
                        Instantiate(LocationCells[11], new Vector3(i, j - 1, j - 1), Quaternion.identity, thisObject.transform);
                        Destroy(Grid[j - 1, i]);
                    }
                    else
                    {
                        if (RoomMap.GetPixel(i, j+1).g == 0)
                        {
                            CellNum = 13;
                            door1 = (door1 + new Vector3(i, j,0)) /2;
                        }
                        else
                        {
                            CellNum = 12;
                        }   
                    }
                    //Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform); 
                    //CellNum = 4;
                }
                else if (blue == 0 && red == 128 / 255f && green == 128 / 255f) //темно желтый дверь 2
                {
                    if (door2 == Vector3.zero)
                    {
                        CellNum = 14;
                        door2 = new Vector3(i, j,0);
                    }
                    else
                    {
                        if (RoomMap.GetPixel(i+1, j).r == 0)
                        {
                            CellNum = 16;
                            door2 = (door2 + new Vector3(i, j,0)) / 2;
                        }
                        else
                        {
                            CellNum = 15;
                        }
                       
                    }
                    //Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform); 
                    //CellNum = 4;
                }
                else if (blue == 0 && red == 128 / 255f && green == 0) //темно красный дверь 3
                {
                    if (door3 == Vector3.zero)
                    {
                        CellNum = 18;
                        door3 = new Vector3(i, j,0);
                        Instantiate(LocationCells[17], new Vector3(i, j-1, j-1), Quaternion.identity, thisObject.transform);
                        Destroy(Grid[j-1, i]);
                    }
                    else
                    {
                        if (RoomMap.GetPixel(i, j+1).r == 0)
                        {
                            CellNum = 19;
                            door3 = (door3 + new Vector3(i, j,0))/2;
                        }
                        else
                        {
                            CellNum = 18;
                        }
                       
                    }
                    //Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform); 
                    //CellNum = 4;
                }
                else if (blue == 128 / 255f && red == 0 && green == 0) //темно синий дверь 4
                {
                    if (door4 == Vector3.zero)
                    {
                        CellNum = 20;
                        door4 = new Vector3(i, j);
                    }
                    else
                    {
                        if (RoomMap.GetPixel(i+1, j).b == 0)
                        {
                            CellNum = 9;
                            Instantiate(LocationCells[22], new Vector3(i+1, j, j), Quaternion.identity, thisObject.transform);
                            door4 = (door4 + new Vector3(i, j,0)) * 0.5f;
                        }
                        else
                        {
                            CellNum = 21;
                        }
                       
                    }
                    //Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform); 
                    //CellNum = 4;
                }
                else if (blue == 1 && red == 1 && green == 0) // розовый враги
                {
                    enemyCells[enemyCellsIndex] = new Vector2(i+0.5f, j+0.5f);
                    enemyCellsIndex++;
                    CellNum = 9;
                }
                else if ((blue == 0 && red == 1 && green == 0))
                {
                    CellNum = 9;
                    Instantiate(redportal, new Vector3(i, j, 0), Quaternion.identity, thisObject.transform);
                }
                else if ((blue == 1 && red == 0 && green == 0))
                {
                    CellNum = 9;
                    Instantiate(blueportal, new Vector3(i, j, 0), Quaternion.identity, thisObject.transform);
                    blueportal.GetComponent<Portal_scr>().WhereToPort = GetComponentInParent<DungeonBuilder_scr>().NextLevel;
                }
                else //белый пустой
                {
                    CellNum = 9;
                }
                //Debug.Log(Grid[j, 0]);
                Grid[j, i] = Instantiate(LocationCells[CellNum], new Vector3(i, j, j), Quaternion.identity, thisObject.transform);
                Grid[j, i].GetComponent<Cell_class>().Position = new Vector2(i, j);
                if (CellNum == 13 || CellNum == 19)
                {
                    j++;
                }
            }
            //Debug.Log(i);
        }
        //Instantiate(arrowRight, thisObject.transform);
        arrowRight.transform.position = door1 + Vector3.left*2; 
        //Instantiate(arrowDown, thisObject.transform);
        arrowDown.transform.position = door2 + Vector3.up * 2.5f;
        //Instantiate(arrowLeft, thisObject.transform);
        arrowLeft.transform.position = door3 + Vector3.right * 2;
        //Instantiate(arrowUp, thisObject.transform);
        arrowUp.transform.position = door4 + Vector3.down * 2;
    }
    
    int CellType(int i, int j) //можно переписать на считывание 4 ячеек (просто оно работало и ладно)
    {
        //float cur;
        float u1=1, u2=1,u3=1,m1=1,m3=1,d1=1,d2=1,d3=1;
        if(j < GridHigh)
        {
            if (i > 0)
            {
                u1 = RoomMap.GetPixel(i-1, j+1).r;
            }
            if (i < GridWidth)
            {
                u3 = RoomMap.GetPixel(i+1, j+1).r;
            }
            u2 = RoomMap.GetPixel(i, j +1).r; ;
        }
        if (i > 0)
        {
            m1 = RoomMap.GetPixel(i - 1, j).r;
        }
        if (i < GridWidth)
        {
            m3 = RoomMap.GetPixel(i + 1, j).r;
        }
        if (j > 0)
        {
            if (i > 0)
            {
                d1 = RoomMap.GetPixel(i - 1, j - 1).r;
            }
            if (i < GridWidth)
            {
                d3 = RoomMap.GetPixel(i + 1, j - 1).r;
            }
            d2 = RoomMap.GetPixel(i, j - 1).r; ;
        }






        if (Mathf.Max(u1, u2 , u3 , m1 , m3, d1, d2, d3) == 0 || i==0||j==0||i == GridWidth||j== GridHigh)
        {
            return 2;
        }
        if (u2 != 0)
        {
            if (0 != m3)
            {
                return 8;
            }
            if (m1 != 0)
            {
                return 7;
            }
            return 1;
        }
        if (d2 != 0)
        {
            if (0 != m3)
            {
                return 6;
            }
            if (m1 != 0)
            {
                return 5;
            }
            return 0;
        }
        if (m3 != 0)
        {
            return 4;
        }
        if (m1 != 0)
        {
            return 3;
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
        //Debug.Log(high + " " + width);
        if (width >= GridWidth || high >= GridHigh)
        {
            Debug.Log("Out of grid");
        }
        else
        {
            //Debug.Log(Grid[width, high]);
            Grid[width, high].GetComponent<Cell_class>().GetObj(InHand);
        }
    }
}
