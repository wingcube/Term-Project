using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public PoolManager pool;
    public Player player;
    public Ally ally;
    void Awake() {
        instance = this;
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        Debug.Log(gameTime);
    }
}
