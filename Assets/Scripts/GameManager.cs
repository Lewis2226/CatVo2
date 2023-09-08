using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    public GameObject Cat_Prefab, Crate_Prefab;
    public SpriteRenderer Fence;


    public string[] Cat_Names;
    public Sprite[] Cat_Sprites, Yarn_Sprites;

    public int Coins, Bought_Cat;

    void Awake() {
        gameManager = this;

    }

    private void Start() {
        Spawn_Cat();

        Add_Coins(600);
    }
    // Update is called once per frame
    void Update() {

    }

    public void Buy_Cat() 
    {
        if(Coins >=25 * (Bought_Cat + 1)) 
        {
            Add_Coins(-25 * (Bought_Cat + 1));
            Bought_Cat++;
            Spawn_Cat();
        }
    }
    public void Spawn_Cat()
    {
        Vector3 position = new Vector3(Random.Range(Fence.bounds.extents.x -0.15f, (Fence.bounds.extents.x * -1) + 0.15f), Random.Range(Fence.bounds.extents.y -0.5f,(Fence.bounds.extents.y * -1)+ 0.5f),0);
        Instantiate(Cat_Prefab, position, Quaternion.identity,null);
    }

    public void Spawn_Crate() {
        Vector3 position = new Vector3(Random.Range(Fence.bounds.extents.x - 0.15f, (Fence.bounds.extents.x * -1) + 0.15f), Random.Range(Fence.bounds.extents.y - 0.5f, (Fence.bounds.extents.y * -1) + 0.5f), 0);
        Instantiate(Crate_Prefab, position, Quaternion.identity, null);
    }

    public void Start_Crate() 
    {
        InvokeRepeating("Spawn_Crate", 1, 5);

    }

    public void Add_Coins(int amount)
    {
        Coins += amount;

        Texts.texts.ChangeText_Coins(Coins);
    }
}
