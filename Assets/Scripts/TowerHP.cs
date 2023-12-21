using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerHP : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    public float maxHP = 100;
    public float currentHP;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerScore playerScore;
    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if (currentHP <=0)
        {
            PlayerPrefs.SetInt("Score" , playerScore.Score); //scene이 넘어가면 데이터가 삭제되기 때문에 저장 용도 코드
            //tower 파괴 시 nextSceneName으로 이동
            SceneManager.LoadScene(nextSceneName); 
        }
    }

    private IEnumerator HitColorAnimation()
    {
        Color color =spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = color;
    }
}
