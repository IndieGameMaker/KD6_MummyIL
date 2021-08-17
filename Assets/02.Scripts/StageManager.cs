using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum HINT_COLOR
    {
        BLACK, RED, GREEN, BLUE
    }

    // 힌트의 색상
    public HINT_COLOR hintColor = HINT_COLOR.BLACK;

    public Material[] hintMt;
    public string[] hintTag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
