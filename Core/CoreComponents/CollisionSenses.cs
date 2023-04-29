using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{

    public Transform  GroundCheckPointA     { get => groundCheckPointA;     private set => groundCheckPointA    = value; }
    public Transform  GroundCheckPointB     { get => groundCheckPointB;     private set => groundCheckPointB    = value; }
    public float      GroundCheckRadius     { get => groundCheckRadius;     private set => groundCheckRadius    = value; }
    public LayerMask  WhatIsGround          { get => whatIsGround;          private set => whatIsGround         = value; }

    [SerializeField] private Transform  groundCheckPointA;
    [SerializeField] private Transform  groundCheckPointB;
    [SerializeField] private float      groundCheckRadius;
    [SerializeField] private LayerMask  whatIsGround;
    
    [SerializeField] private LayerMask  whatIsObstacle;
 
    public bool Ground 
    {
        get => Physics2D.OverlapArea(groundCheckPointA.position, groundCheckPointB.position, whatIsGround);
    }
}
