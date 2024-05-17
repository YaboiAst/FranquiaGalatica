using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float dropSpeed = 10f;
    [SerializeField] private LayerMask checkLimit;
    
    private Collider2D _collider2D;
    private Tween _dropTween;
    
    public static UnityEvent OnBlockDrop = new();

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.enabled = false;
    }

    public void Drop()
    {
        _collider2D.enabled = true;
        _dropTween = transform.DOMoveY(transform.position.y - 20f, dropSpeed)
            .SetEase(Ease.Linear);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bloco") && !other.gameObject.CompareTag("Chao"))
            return;
        
        _dropTween.Kill();
        if (other.gameObject.CompareTag("Chao"))
        {
            if (BlockManager.Ground is not null) return; // Fim do nivel?
            BlockManager.OnStackSet?.Invoke(other.transform);
        }

        TestLimit();
        OnBlockDrop?.Invoke();
        BlockManager.OnStackAdd?.Invoke(this);
        this.enabled = false;
    }

    private void TestLimit()
    {
        bool isOnLimit = Physics2D.OverlapBox(transform.position, Vector2.one * 0.5f, 0f, checkLimit);
        if (!isOnLimit) return;
        BlockManager.OnUpdateCamera?.Invoke();
    }
}
