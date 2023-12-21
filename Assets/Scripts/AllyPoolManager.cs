using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPoolManager : MonoBehaviour
{
    // 변수 설정
    public GameObject[] prefabs;
    List<GameObject>[] allyPools;

    void Awake() {
        allyPools = new List<GameObject>[prefabs.Length];
        // allyPools 초기화
        for (int index = 0; index < allyPools.Length; index++) {
            allyPools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) {
        GameObject select = null;
        // 선택한 풀의 비활성화된 게임오브젝트 접근
        
        foreach (GameObject item in allyPools[index]) {
            if(!item.activeSelf){
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true); //select를 다시 Active로 만듦
                break;
                
                
            }
                
        }
        if (!select) {
                // select를 못찾으면 새롭게 생성하고 select 변수에 할당
                select = Instantiate(prefabs[index], transform);
                allyPools[index].Add(select);
            }
        
        return select;
    }

    
}
