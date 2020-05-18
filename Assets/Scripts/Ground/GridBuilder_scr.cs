using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cell = null;
    public int GridWidth = 2;
    public int GridHigh = 2;
    public bool rebuildable = true;
    public bool builded = false;
    public string Location = "Vilage";

    public Sprite[] Textures = new Sprite[2];

    private GameObject[,] Grid;
    void Start()
    {
        if (!builded)
        {
            Grid = new GameObject[GridWidth, GridHigh];
            for (int i = 0; i< GridHigh; i++)
            {
                for (int k = 0; k < GridWidth; k++)
                {
                    Grid[k, i] = new GameObject();
                    Grid[k, i].AddComponent<Cell_class>();
                    //Grid[k, i].AddComponent<SpriteRenderer>();
                    Grid[k, i].GetComponent<Cell_class>().Set(this.gameObject, k, i);
                    //Grid[k, i].SetPropertys();
                }
            }
            BuildGrid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildGrid()
    {
        for (int i = 0; i < GridHigh; i++)
        {
            for (int k = 0; k < GridWidth; k++)
            {
                
            }
        }
    }

    public void PutOnCell(GameObject obj, int width, int high)
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
    public void GetFromCell(int width, int high, GameObject InHand)
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
