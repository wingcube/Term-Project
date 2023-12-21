using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //변수 설정
    
    [SerializeField]
    private float moveSpeed;

    Animator animator;
    Rigidbody2D myrigidbody;
    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;
    public string targetLayerName = "Enemy";
    private int damage = 1;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput,0f);
        if(horizontalInput<0) {
            transform.localScale = new Vector3(-1,1,1);
            animator.SetBool("walk2",true);
        }
        else if(horizontalInput > 0) {
            transform.localScale = new Vector3(1,1,1);
            animator.SetBool("walk2",true);
        }
        else if(verticalInput >0 || verticalInput<0) {
             animator.SetBool("walk2",true);
        }
        else {
             animator.SetBool("walk2",false);
        }
        transform.position += moveTo* moveSpeed * Time.deltaTime;

       
           

        //Q키를 누르면 공격, targetLayerName을 가진 대상만 Trigger 적용
        if(curTime<=0) {
            if(Input.GetKey(KeyCode.Q)) {
            
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position , boxSize, 0);
                foreach(Collider2D collider in collider2Ds){
                    if (collider.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
                    {
                        collider.GetComponent<EnemyHP>().TakeDamage(damage);
                    }
                    
                }

                animator.SetTrigger("atk");
                
                curTime = coolTime;
            }
        }
        else {
            curTime -= Time.deltaTime;
        }
        
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
