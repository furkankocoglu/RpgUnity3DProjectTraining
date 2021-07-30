using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followHeight = 7f;
    public float followDistance = 6f;
    public float followHeightSpeed = 0.9f;

    private Transform Player=default;

    private float targetHeight = default;
    private float currentHeight = default;
    private float currentRotation = default;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        targetHeight = Player.position.y + followHeight;
        currentRotation = transform.eulerAngles.y;
        currentHeight = Mathf.Lerp(transform.position.y,targetHeight,followHeightSpeed*Time.deltaTime);
        Quaternion eular = Quaternion.Euler(0f, currentRotation, 0f);

        Vector3 targetPosition = Player.position - (eular * Vector3.forward) * followDistance;
        targetPosition.y = currentHeight;
        transform.position = targetPosition;
        transform.LookAt(Player);
    }
}
