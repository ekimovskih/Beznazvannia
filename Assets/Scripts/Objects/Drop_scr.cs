using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_scr : MonoBehaviour
{
    // Start is called before the first frame update
    [Range (0,50)]public float BounceStr = 40f;
    public string type = "none";
    public string supertype;
    public int id;
    public int InStack;
    public int count = 1;

    //public bool Craftable = false;
    public GameObject[] Recipe;
    public int[] RecipeCount;
    public int ResultCount;

    public bool Interactive = false;
    public float ActionSpeed = 0.4f;
    public int DMG = 1;
    public float knockBack = 910f;
    public GameObject AttackZone = null;
    public int Efficiency = 0;
    public int Armor = 0;
    public int MaxHealth = 0;
    public int Health = 0;
    public int MaxStamina = 0;
    public int Stamina = 0;
    public int RegenHP = 0;
    public int RegenSTM = 0;
    public float speed = 0; // сорость бега
    public float ShiftSpeed = 0; // + скорость с зажатым LeftShift
    public float JumpStrengh = 0;
    public int JumpWaste = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y+0.5f);
    }
    public void Bounce(GameObject collision)
    {
        GameObject ThisObj = this.gameObject;
        Vector2 self = new Vector2(ThisObj.transform.position.x, ThisObj.transform.position.y);
        Vector2 other = new Vector2(collision.transform.position.x, collision.transform.position.y);

        self = -(other - self);
        float VecLength = Mathf.Sqrt(self.x * self.x + self.y * self.y);
        self /= VecLength;
        //Debug.Log(self);
        ThisObj.transform.GetComponent<Rigidbody2D>().AddForce(self * BounceStr);
    }
    public void IsEmpty()
    {
        Destroy(this.gameObject);
    }
    /*
    int CheckRecipe(int[] CraftSlots, int numberOFslots)
    {
        int RecipeLength = Recipe.Length;
        int coincidences = 0;
        for (int i)
        return coincidences;
    }
    */
}
