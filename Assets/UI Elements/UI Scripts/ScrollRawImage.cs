using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRawImage : MonoBehaviour
{
    public float horizontalSpeed = default;
    public float verticalSpeed = default;

    RawImage myRawImage = default;
    // Start is called before the first frame update
    void Start()
    {
        myRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect currentUv = myRawImage.uvRect;
        currentUv.x -= Time.deltaTime * horizontalSpeed;
        currentUv.y -= Time.deltaTime * verticalSpeed;

        if (currentUv.x <= -1f || currentUv.x >= 1f)
        {
            currentUv.x = 0f;
        }
        if (currentUv.y <= -1f || currentUv.y >= 1f)
        {
            currentUv.y = 0f;
        }
        myRawImage.uvRect = currentUv;
    }
}
