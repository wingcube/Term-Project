using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    //변수 설정
    public Transform[] allySpawnPoint;
    [SerializeField]
    PlayerScore playerscore;
    [SerializeField]
    AllyPoolManager allyPool;
    [SerializeField]
    void Awake()
    {
        allySpawnPoint = GetComponentsInChildren<Transform>();
    }
    
    public void spawnAlly() // 아군 구매 버튼을 눌렀을 때 실행, Spawn()을 실행하고 스코어, 즉 골드를 50 감소시킴
    {
        Spawn();
        playerscore.Score-=50;
    }
    public void Spawn()
    {
        GameObject ally = allyPool.Get(0);//allyPool의 0번째 index를 가져옴
        ally.transform.position = allySpawnPoint[Random.Range(1,allySpawnPoint.Length)].position; // allySpawnPoint 2개중 한 군대 랜덤 생성
        
    } 
}
