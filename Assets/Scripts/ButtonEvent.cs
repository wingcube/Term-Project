using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    //변수 설정
    [SerializeField]
    ShopManager store;
    [SerializeField]
    PlayerScore Score;
    [SerializeField]
    AllyHP allyHP;
    [SerializeField]
    Ally ally;
    [SerializeField]
    AllySpawner allySpawner;

    public void SpawnAlly() // 상점에서 아군을 구매했을 때 실행
    {
        allySpawner.spawnAlly();
    }
    public void BuyMaxHeath() //상점에서 MaxHealth를 구매했을 때 실행
    {
        
        Debug.Log(allyHP.maxHP);
        allyHP.AddMaxHealth();
        Score.Score-=100;
        
    }
    public void BuyAttackDamage() //상점에서 AttackDamage를 구매했을 때 실행
    {
        
        ally.AddDamage();
        Score.Score-=100;
    }
    public void StoreButton() //상점 버튼을 눌렀을 때 실행
    {
        store.ToggleShop();
    }
    
    public void SceneLoader(string sceneName) //다음 씬으로 이동하는 함수
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GameQuit() //게임 종료 함수
    {
        Application.Quit();
    }
    
}
