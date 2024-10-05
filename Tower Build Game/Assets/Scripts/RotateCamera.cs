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
