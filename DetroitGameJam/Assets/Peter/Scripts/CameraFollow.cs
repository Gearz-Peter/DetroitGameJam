using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    [SerializeField] private Transform playerTransform;

    private GameObject mapBounds;
    private float xmin, xmax, ymin, ymax;
    [SerializeField] private float camHeight, camWidth;
    private float camX, camY;

    void Awake()
    {
        mapBounds = GameObject.FindGameObjectWithTag("CameraLimit");
        xmin = mapBounds.GetComponent<BoxCollider2D>().bounds.min.x;
        xmax = mapBounds.GetComponent<BoxCollider2D>().bounds.max.x;
        ymin = mapBounds.GetComponent<BoxCollider2D>().bounds.min.y;
        ymax = mapBounds.GetComponent<BoxCollider2D>().bounds.max.y;
        camHeight = this.GetComponentInChildren<Camera>().orthographicSize;
        camWidth = camHeight * this.GetComponentInChildren<Camera>().aspect;
    }

    void Update()
    {
        camX = Mathf.Clamp(playerTransform.position.x, xmin + camWidth, xmax - camWidth);
        camY = Mathf.Clamp(playerTransform.position.y, ymin + camHeight, ymax - camHeight);

        this.transform.position = new Vector3(camX, camY, -10);
    }
}