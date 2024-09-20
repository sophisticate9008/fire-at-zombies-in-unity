using System;
using System.Collections.Generic;
using MyBase;
using MyComponents;
using UnityEngine;
using VContainer;

public class Bullet : ArmChildBase
{
    
    public new void OnTriggerEnter2D(Collider2D collider) {
        base.OnTriggerEnter2D(collider);
        
    }
}

