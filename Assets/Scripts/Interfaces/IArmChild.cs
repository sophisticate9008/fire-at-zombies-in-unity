using System.Collections.Generic;
using UnityEngine;


public interface IArmChild
{
    public List<IComponent> InstalledComponents {get;set;}
    public float Speed{get;set;}
    public Vector2 Direction{get;set;}
}