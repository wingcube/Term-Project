using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //변수 설정
    [SerializeField]
    private int damage=1;
    public RuntimeAnimatorController[] animCon;
    Animator anim;
    EnemyHP enemyHP;
    Rigidbody2D rb;
    [SerializeField]
    Transform target;
    [SerializeField]
    Transform mainTarget;
    [SerializeField]
    Transform allyTarget;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float playerAttackDistance = 1.2f;
    private float towerAttackDistance = 2f;

    public string targetLayerName = "Player";
    public string fTargetLayerName = "Ally";

    bool follow = false;

    Collider2D playerCol;
    Collider2D towerCol;
    Collider2D allyCol;
    
    private bool canTakeDamage = true; // 데미지 적용 속도를 제어하는 플래그

    private bool playerInside = false;
   // 플레이어와 메인타워의 collider가 겹치는 일이 없도록 하기 위한 변수


    private bool allyIsLive = false;
    private bool allyInside = false;
    SpriteRenderer spriter;

    [SerializeField]
    private int score;//적 처치 시 획득 점수
    private PlayerScore playerScore; //플레이어의 점수 정보 접근
    void Awake()
    {
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        anim = GetComponent<Animator>();
        enemyHP = GetComponent<EnemyHP>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainTarget = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
        spriter = GetComponent<SpriteRenderer>();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
        FollowTarget();
        
    }

    

    void FollowTarget()
    {
        allyIsLive=GameObject.FindGameObjectWithTag("Ally"); //현재 게임내에 Ally tag를 가진 오브젝트 저장
        if (allyIsLive)
        {
            // "Ally" 태그를 가진 모든 게임 오브젝트 찾기
            GameObject[] potentialAllies = GameObject.FindGameObjectsWithTag("Ally");
            GameObject closestAlly = null;
            float minDistance = float.MaxValue;

            // 모든 ally에 대해 반복
            foreach (GameObject ally in potentialAllies)
            {
                // 현재 ally까지의 거리 계산
                float distanceToAlly = Vector2.Distance(transform.position, ally.transform.position);

                // 최소 거리를 가진 Ally 찾기
                if (distanceToAlly < minDistance)
                {
                    minDistance = distanceToAlly;
                    closestAlly = ally;
                }
            }        
            allyTarget=closestAlly.GetComponent<Transform>();
            allyCol=closestAlly.GetComponent<Collider2D>();

        }

        if(follow)
        {
            //Ally가 감지범위 안에 있으면 Player보다 우선순위로 따라감
            if(allyInside)
            {
                if (Vector2.Distance(transform.position, allyTarget.position) > playerAttackDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, allyTarget.position, moveSpeed * Time.deltaTime);
                    spriter.flipX = allyTarget.position.x > rb.position.x;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    StartCoroutine(TakeDamageWithDelay()); // 일정 범위 안에 들어가면 데미지 입히기
                }
            }
                
            //감지범위 안에 Ally는 없고 Player가 있을 경우
            else if(playerInside) 
            {
                if (Vector2.Distance(transform.position, target.position) > playerAttackDistance) 
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    spriter.flipX = target.position.x > rb.position.x;
                }
                else 
                {
                    rb.velocity = Vector2.zero;
                    StartCoroutine(TakeDamageWithDelay()); // 일정 범위 안에 들어가면 데미지 입히기
                }
    
            }   
        }

        //Ally와 Player 모두 감지영역 안에 있지 않을 경우
        else if(Vector2.Distance(transform.position, mainTarget.position)>towerAttackDistance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, mainTarget.position, moveSpeed * Time.deltaTime);
            spriter.flipX = mainTarget.position.x > rb.position.x;
        }
        // 플레이어가 감지 영역 밖에 있고 메인타워와의 거리가 towerAttackDistance보다 클 때
        else 
        {
            rb.velocity=Vector2.zero;
            StartCoroutine(TakeDamageWithDelay());
        }
    }

    IEnumerator TakeDamageWithDelay()
    {
        if (canTakeDamage)
        {
            if(allyInside)
            {
                allyCol.GetComponent<AllyHP>().TakeDamage(damage);
                canTakeDamage = false;
                yield return new WaitForSeconds(2f); // 1초 동안 대기 후 다시 데미지 허용
                canTakeDamage = true;

            }
            else if(playerInside)
            {
                playerCol.GetComponent<PlayerHP>().TakeDamage(damage);
                canTakeDamage = false;
                yield return new WaitForSeconds(2f); // 1초 동안 대기 후 다시 데미지 허용
                canTakeDamage = true;
                
            }
            else
            {
                towerCol.GetComponent<TowerHP>().TakeDamage(damage);
                canTakeDamage = false;
                yield return new WaitForSeconds(2f); // 1초 동안 대기 후 다시 데미지 허용
                canTakeDamage = true;
            }
            
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Ally"))
        {
            allyInside = true; 
        }
        // Ally와 Player가 모두 탐지 Collider와 충돌했을 수도 있으므로 else if가 아닌 if로 따로 쓰기
        if (collision.CompareTag("Player"))
        {
          
            playerInside = true;
            playerCol=collision;
        
        }
        else if (collision.CompareTag("Tower"))
        {
            
            towerCol=collision;
        }
        follow = playerInside || allyInside; // Player나 Ally 중 어느 하나라도 감지 범위에 있으면 true
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Ally"))
        {
            allyInside = false;
        }
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
        follow = playerInside || allyInside; // Player나 Ally 중 어느 하나라도 감지 범위에 있으면 true
    }

    void OnEnable(){
        target = GameManager.instance.player.GetComponent<Transform>(); 
        enemyHP.currentHP = enemyHP.maxHP;
    }

    //따로 변수들을 편하게 사용할 수 있게 하는 함수
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        enemyHP.maxHP = data.health;
        enemyHP.currentHP = data.health;
        moveSpeed = data.speed;
        score = data.gold;
        damage = data.damage;
    }
    
    public void OnDie()
    {
        playerScore.Score +=score; //적 처치 시 스코어 획득
        canTakeDamage=true;
        Dead();
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
  
}