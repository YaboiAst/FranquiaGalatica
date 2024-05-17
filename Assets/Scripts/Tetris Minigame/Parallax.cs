using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Renderer sky;
    private Material _skyMaterial;
    [SerializeField] private float parallaxOffset;
    [SerializeField] private float parallaxDuration;
    
    private void Start()
    {
        _skyMaterial = sky.material;
        
        BlockManager.OnUpdateCamera.AddListener(UpdateCamera);
    }

    private void UpdateCamera()
    {
        transform.DOMoveY(transform.position.y - parallaxOffset, parallaxDuration);
        StartCoroutine(MaterialParallax());
    }

    private IEnumerator MaterialParallax()
    {
        var counter = parallaxDuration;
        var offset = new Vector2(0f, parallaxOffset / (parallaxDuration * 50f));
        while (counter >= 0f)
        {
            _skyMaterial.mainTextureOffset -= offset * Time.deltaTime;
            counter -= Time.deltaTime;
            yield return null;
        }
    }
}
