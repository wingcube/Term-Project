using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // 변수 설정
    [SerializeField]
    public float maxHP;
    public float currentHP;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;
    Color color;

    private void Awake() {
        currentHP = maxHP; //현재 체력을 최대 체력과 같게 설정
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color =spriteRenderer.color;
    }

    public void TakeDamage(float damage) {

        //현재 체력을 damage만큼 감소
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //적 사망
        if(currentHP <=0){
            
            enemy.OnDie();
        }
        
    }

    private IEnumerator HitAlphaAnimation()
    {
        
        //공격받으면 색깔을 빨강으로 설정
        if(currentHP>0)
        {
            
            spriteRenderer.color=Color.red;
            //0.2초 동안 대기
            yield return new WaitForSeconds(0.2f);
            //색깔을 다시 복귀
            spriteRenderer.color = color;
        }
    }
}
