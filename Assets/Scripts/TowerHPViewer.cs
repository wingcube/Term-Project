using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHPViewer : MonoBehaviour
{
    [SerializeField]
    private TowerHP towerHP;
    private Slider sliderHP;
    // Start is called before the first frame update
    void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderHP.value = towerHP.currentHP / towerHP.maxHP;
    }
}
