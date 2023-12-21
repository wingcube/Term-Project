using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score;
    public int Score
    {
        //set 값이 음수가 되지 않도록
        set => score = Mathf.Max(0,value);
        get => score;
    }
   
}
