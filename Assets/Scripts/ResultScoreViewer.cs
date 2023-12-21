using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;
    private void Awake()
    {
        textResultScore = GetComponent<TextMeshProUGUI>();
        //Stage에서 저장한 점수를 불러와서 저장
        int score = PlayerPrefs.GetInt("Score");
        //저장한 점수를 화면에 출력
        textResultScore.text = "Result Score "+score;
    }
    
}
