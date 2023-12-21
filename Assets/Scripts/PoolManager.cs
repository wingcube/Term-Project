using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩들을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    // 변수, 리스트 초기화
    void Awake() {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) {
            pools[index] = new List<GameObject>();
        }
        
    }

    public GameObject Get(int index) {
        GameObject select = null;
        // 선택한 풀의 비활성화된 게임오브젝트 접근
        
        

        foreach (GameObject item in pools[index]) {
            if(!item.activeSelf){
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
            
        }

        if (!select) {
                // 못찾으면 새롭게 생성하고 select 변수에 할당
                select = Instantiate(prefabs[index], transform);
                pools[index].Add(select);
            }


        return select;
    }
}
