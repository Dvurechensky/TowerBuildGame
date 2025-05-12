/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 05:48:46
 * Version: 1.0.4
 */

using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed = 5f;
    private Transform _rotator;

    private void Start()
    {
        _rotator = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _rotator.Rotate(0, speed * Time.deltaTime, 0);
    }
}
