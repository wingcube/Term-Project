using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    // 변수 설정
    public Animator animator;
    Rigidbody2D rb;
    [SerializeField]
    Transform target;
    [SerializeField]
    private float moveSpeed = 2f;
    Transform towerPos;
    private float enemyAttackDistance = 1.2f;
    private float waitAtTowerDistance = 2f;
    bool follow = false;
    SpriteRenderer spriter;


    private bool canTakeDamage = true; // 데미지 적용 속도를 제어하는 플래그
    [SerializeField]
    public int damage = 2; // Ally의 공격력
    Collider2D enemyCol;
    private bool targetIsLive;
    

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        towerPos = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        // 가장 가까운 Enemy를 찾는 단계
        targetIsLive = GameObject.FindGameObjectWithTag("Enemy"); //현재 게임 내의 Enemy tag를 가지고 있는 오브젝트 저장
        if (targetIsLive)
        {
            GameObject[] potentialEnemies = GameObject.FindGameObjectsWithTag("Enemy"); //Enemy tag를 가지고 있는 모든 오브젝트를 배열에 저장
            GameObject closestEnemy = null;
            float minDistance = float.MaxValue;
            
            foreach (GameObject enemy in potentialEnemies)
            {
                // 현재 아군까지의 거리를 계산
                float distanceToAlly = Vector2.Distance(transform.position, enemy.transform.position);

                // 최소 거리를 가진 아군 탐색
                if (distanceToAlly < minDistance)
                {
                    minDistance = distanceToAlly;
                    closestEnemy = enemy;
                }
            }        

            target = closestEnemy.GetComponent<Transform>(); // 가장 가까운 Enemy의 Transform 컴포넌트 가져오기, Enemy에게 이동하기 위한 것
            enemyCol = closestEnemy.GetComponent<Collider2D>();//공격으로 데미지를 입는 대상



        }
        // 가장 가까운 Enemy를 찾은 후 직접 행동하는 단계
        if(targetIsLive) //target이 있을 때
        {
            //Enemy가 탐지 영역 안에 들어오고 Enemy까지의 거리가 enemyAttackDistance보다 클 경우 Enemy 방향으로 이동
            if (Vector2.Distance(transform.position, target.position) > enemyAttackDistance && follow) 
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                spriter.flipX = target.position.x > rb.position.x;
            }
            //Enemy에게 다가가다가 enemyAttackDistance보다 가까워졌을 경우 멈추고 데미지를 입힘
            else if(Vector2.Distance(transform.position, target.position) <= enemyAttackDistance && follow)
            {
                rb.velocity = Vector2.zero;
            
                StartCoroutine(TakeDamageWithDelay());
            }

            //감지영역 안에 Enemy가 없을 때는 타워 옆으로 돌아가도록
            else
            {
                
                if(Vector2.Distance(transform.position, towerPos.position)>waitAtTowerDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, towerPos.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            
            }
        }

        else // target이 없을 때
        {
            if(Vector2.Distance(transform.position, towerPos.position)>waitAtTowerDistance)
            {
                    transform.position = Vector2.MoveTowards(transform.position, towerPos.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                    rb.velocity = Vector2.zero;
            }
        }
    }


    IEnumerator TakeDamageWithDelay()
    {
        if (canTakeDamage)
        {
            
            enemyCol.GetComponent<EnemyHP>().TakeDamage(damage); // Enemy에게 데미지를 줌
            canTakeDamage = false;
            yield return new WaitForSeconds(2f); // 2초 동안 대기 후 다시 데미지 허용
            canTakeDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.CompareTag("Enemy"))
        {
            //감지 영역 Collider 안에 들어온 오브젝트의 태그가 Enemy일 경우 follow = true
            follow = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy"))
        {
            // Enemy와 충돌이 끝날 때 follow = false
            follow = false;
        }
    }


    public void AddDamage() //상점에서 AttackDamage +1을 구매했을 때 실행되는 함수
    {
        damage+=1;
        Debug.Log("damage"+damage);
    }

    public void OnDie()
    {
        canTakeDamage=true;
        Dead();
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
