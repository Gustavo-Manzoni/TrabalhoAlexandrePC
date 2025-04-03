using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangePos : MonoBehaviour
{
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();  
    }
    public void ChangePos()
    {
        rectTransform.anchoredPosition = new Vector2(Random.Range(-800,  800), Random.Range(-200, 700));
    }
}
