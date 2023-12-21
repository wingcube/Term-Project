using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject storePanel; // Inspector에서 연결할 상점 패널

    private void Start()
    {
        // 시작 시에는 상점을 비활성화
        if (storePanel != null)
        {
            storePanel.SetActive(false);
        }
    }

    public void ToggleShop()
    {
        // 버튼 클릭 시 상점을 토글 (보이면 감추고, 감춰져 있으면 보이게)
        if (storePanel != null)
        {
            storePanel.SetActive(!storePanel.activeSelf);
        }
    }
}
