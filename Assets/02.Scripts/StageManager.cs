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

    // Hint Cube의 Renderer
    private new Renderer renderer;

    void Start()
    {
        renderer = transform.Find("Hint").GetComponent<Renderer>();
    }

    public void InitStage()
    {
        // 난수 발생
        int idx = Random.Range(0, hintMt.Length); // 0, 1, 2, 3

        // Hint 머티리얼 교체
        renderer.material = hintMt[idx];
        // Hint 태그를 지정
        renderer.gameObject.tag = hintTag[idx];

        // 목표 타겟의 색상
        hintColor = (HINT_COLOR)idx;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitStage();
        }
    }
}
