using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject capsule;

    private Renderer meshRenderer;

    private float speed = 0.005f;
    private float startTime;
    private float secondUntilDestroy = 10f;

    void Start()
    {
        startTime = Time.time;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= secondUntilDestroy)
        {
            Destroy(this.gameObject);
        }

        this.gameObject.transform.position += speed * this.gameObject.transform.forward;

        float destroyRatio = (Time.time - startTime) / secondUntilDestroy;
        //Debug.Log($"destroyRatio {destroyRatio}");
        Color color = meshRenderer.material.color;
        color.a = 1 - destroyRatio;
        meshRenderer.material.color = color;
        capsule.transform.localScale = Vector3.one * (1 + destroyRatio);
    }
}
