using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AllyHP : MonoBehaviour
{
    // 변수 설정
    [SerializeField]
    public float maxHP;
    private float currentHP;
    private Ally ally;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlayerScore playerscore;
    
    void Awake()
    {
        currentHP = maxHP; //현재 체력을 최대 체력과 같게 설정
        ally = GetComponent<Ally>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(float damage) {

        //현재 체력을 damage만큼 감소
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        // currentHP가 0보다 작거나 같으면 사망
        if(currentHP <=0){
            ally.OnDie();
        }
        
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color =spriteRenderer.color;
        //공격받으면 색깔을 빨강으로 설정
        if(currentHP>0)
        {
            spriteRenderer.color=Color.red;

            // 0.2초 동안 대기
            yield return new WaitForSeconds(0.2f);
            //색깔을 다시 복귀
            spriteRenderer.color = color;
        }
        
    }

    public void AddMaxHealth() //상점에서 MaxHealth +1을 구매하면 실행되는 함수
    {
        Debug.Log(maxHP);
        maxHP++;
        Debug.Log("maxHP is "+ maxHP);

    }
}

