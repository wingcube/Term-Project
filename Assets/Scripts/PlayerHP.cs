using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    public float maxHP = 10;
    public float currentHP;
    private SpriteRenderer spriteRenderer;
    private PlayerScore playerScore;
    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerScore = GetComponent<PlayerScore>(); // playerScore 초기화 추가
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if (currentHP <=0)
        {
            PlayerPrefs.SetInt("Score" , playerScore.Score); //scene이 넘어가면 데이터가 삭제되기 때문에 저장 용도 코드
            //플레이어 사망 시 nextSceneName으로 이동
            SceneManager.LoadScene(nextSceneName); 
            
        }
    }

    private IEnumerator HitColorAnimation()
    {
        //데미지를 받으면 잠시 빨간색으로 변함
        Color color =spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.05f);

        spriteRenderer.color = color;
    }
}
