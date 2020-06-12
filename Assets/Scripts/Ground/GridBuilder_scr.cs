using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder_scr : MonoBehaviour
{
    private GameObject[] EnemyList = null;
    public GameObject[] ObjectList = null;
    public int[] ObjectVariationsChanses;
    private int GridWidth = 2;
    private int GridHigh = 2;
    //public bool rebuildable = true;
    //public bool builded = false;
    //public string Location = "Vilage";
    public Texture2D RoomMap;
    public GameObject[] LocationCells;
    private GameObject[,] LocationGrids;
    private int xONmap;
    private int yONmap;
    public Vector3 door1;
    public Vector3 door2;
    public Vector3 door3;
    public Vector3 door4;
    private Vector2[] enemyCells = new Vector2[30];
    private int enemyCellsIndex = 0;
    public GameObject arrowRight;
    public GameObject arrowDown;
    public GameObject arrowLeft;
    public GameObject arrowUp;
    public GameObject redportal;
    public GameObject blueportal;
    public GameObject LocalGrid;
    public Vector3 door0;

    public bool IsDungeon = true;
    private int MaxEnemysInRoom = 5;
    private int EnemyCellsAtAll = 0;


    public Sprite[] Textures = new Sprite[2];

    private GameObject[,] Grid;
    private void Start()
    {
        MaxEnemysInRoom = MaxEnemysInRoom + GameObject.Find("Player").GetComponent<Player_movement_scr>().Completedlevels*2;
        if (!IsDungeon)
        {
            Build();
        }
        //Build();
        //Debug.Log(enemyCells[0] == Vector2.zero);
    }
    public void TeleportPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = LocalGrid.GetComponent<GridBuilder_scr>().door0;
        LocalGrid.SetActive(true);
        player.transform.position += player.GetComponent<Player_movement_scr>().PlayerContainer.position;
        GameObject.Find("Cursor").GetComponent<Cursor_scr>().GridBuilder = LocalGrid;
        LocalGrid.GetComponent<GridBuilder_scr>().LightUp();
        this.gameObject.SetActive(false);
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

    public void Set(Texture2D spr, int x, int y, GameObject[,] FullMap, GameObject[] EnemysVariations, GameObject[] ObjVariations, int[] ObjectChanses)
    {
        ObjectVariationsChanses = ObjectChanses;
        ObjectList = ObjVariations;
        RoomMap = spr;
        LocationGrids = FullMap;
        xONmap = x;
        yONmap = y;
        EnemyList = EnemysVariations;
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
                            door2 = (door2 + new Vector3(i+1, j,0)) / 2;
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
                            door3 = (door3 + new Vector3(i, j+1,0))/2;
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
                            door4 = (door4 + new Vector3(i+1, j,0)) * 0.5f;
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
                    EnemyCellsAtAll++;
                    CellNum = 9;
                }
                else if ((blue == 0 && red == 1 && green == 0)) // красный портал
                {
                    CellNum = 9;
                    Instantiate(redportal, new Vector3(i, j, 0), Quaternion.identity, thisObject.transform);
                    //rp.GetComponent<Portal_scr>().JustTeleported();
                        //Debug.Log(new Vector3(i, j, 0));
                        Transform pl = GameObject.Find("Player").transform;
                        
                        pl.position = new Vector3(i, j, 0);
                        pl.position += GameObject.Find("Player").GetComponent<Player_movement_scr>().PlayerContainer.position;
                        //Debug.Log (pl.position);
                    GameObject.Find("Cursor").GetComponent<Cursor_scr>().GridBuilder = this.gameObject;
                }
                else if ((blue == 1 && red == 0 && green == 0)) // синий портал
                {
                    CellNum = 9;
                    GameObject bp = Instantiate(blueportal, new Vector3(i, j, 0), Quaternion.identity, thisObject.transform);
                    bp.GetComponent<Portal_scr>().WhereToPort = GetComponentInParent<DungeonBuilder_scr>().NextLevel;
                    //player.transform.position += player.GetComponent<Player_movement_scr>().PlayerContainer.position;
                    //blueportal.GetComponent<Portal_scr>().WhereToPort = GetComponentInParent<DungeonBuilder_scr>().NextLevel;
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
                else if (CellNum == 10)
                {
                    Grid[j, i].GetComponent<Cell_class>().objOnIt =  ChoseObjOnCell();
                }
            }
            //Debug.Log(i);
        }
        arrowRight.transform.position = door1 + Vector3.left*2; 
        arrowDown.transform.position = door2 + Vector3.up * 2.5f;
        arrowLeft.transform.position = door3 + Vector3.right * 2;
        arrowUp.transform.position = door4 + Vector3.down ;
        SpawnMobs();
    }

    GameObject ChoseObjOnCell()
    {
        int num = Random.Range(0, ObjectVariationsChanses.Length);
        //chance = ObjectVariationsChanses[num];
        int chance = Random.Range(0, 100);
        if (chance < ObjectVariationsChanses[num])
        {
            return ObjectList[num];
        }
        return null;
    }
    
    void SpawnMobs()
    {
        //Debug.Log(EnemyCellsAtAll + "EnemyCellsAtAll");
        if (EnemyCellsAtAll > 0)
        {
            for (int i = 0; i < enemyCells.Length; i++)
            {
                if (EnemyCellsAtAll < 0)
                {
                    return;
                }
                if (MaxEnemysInRoom > 0 && enemyCells[i] != Vector2.zero)
                {
                    int chanse = Random.Range(0, 100);
                    if (chanse > 80)
                    {
                        int type = Random.Range(0, EnemyList.Length);
                        Instantiate(EnemyList[type], enemyCells[i], Quaternion.identity, transform);
                        MaxEnemysInRoom--;
                        enemyCells[i] = Vector2.zero;
                        EnemyCellsAtAll--;
                    }
                }
            }
            //Debug.Log(MaxEnemysInRoom + " MaxEnemysInRoom");
            
            if (MaxEnemysInRoom > 0)
            {
                SpawnMobs();
            }
        }
        
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
