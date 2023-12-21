using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;//밑에서 만든 클래스 이용
    float timer;
    int level;
    
    void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
        
    }
   
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 30f),spawnData.Length-1);

        if (timer > spawnData[level].spawnTime) {
            timer = 0;
            Spawn();
        }

        
    }
    void Spawn() {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


//소환 데이터 관리
[System.Serializable] //클래스 직렬화, 따로 속성으로 볼 수 있다. 
public class SpawnData
{
    public float speed;
    public int spriteType;
    public float spawnTime;
    public int health;
    public int gold;
    public int damage;
}
