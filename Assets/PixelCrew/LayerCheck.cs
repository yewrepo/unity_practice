using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private Collider2D _collider2D;
    public bool isTouchingLayer;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        isTouchingLayer = _collider2D.IsTouchingLayers(groundLayer);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTouchingLayer = _collider2D.IsTouchingLayers(groundLayer);
    }
}