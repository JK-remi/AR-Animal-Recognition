using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Loading : MonoBehaviour
{
    public Image imgLoading;
    public float rotSpeed = 10f;
    private void Update()
    {
        if(imgLoading != null)
        {
            imgLoading.transform.Rotate(new Vector3(0, 0, -rotSpeed * Time.deltaTime));
            //imgLoading.color = Color.Lerp(imgLoading.color, UnityEngine.Random.ColorHSV(), rotSpeed * Time.deltaTime);
        }
    }
}
