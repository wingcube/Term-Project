using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreViewer : MonoBehaviour
{
    [SerializeField]
    public PlayerScore PlayerScore;
    private TextMeshProUGUI textScore;
    void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        //text UI에 현재 점수 정보를 업데이트
        textScore.text = "Gold "+PlayerScore.Score;
    }
}
