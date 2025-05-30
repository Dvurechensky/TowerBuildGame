﻿/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 05:48:46
 * Version: 1.0.4
 */

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
    private float shakeDuration = 1f, shakeAmount = 0.04f, decreaseFactor = 1.5f;
    private Vector3 originPosition;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        originPosition = camTransform.localPosition;
    }

    private void Update()
    {
        if(shakeDuration > 0)
        {
            //Случайное значение внутри сферы с радиусом 1
            camTransform.localPosition = originPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0;
            camTransform.localPosition = originPosition;
        }
    }

}
