using System;
using UnityEngine;

public interface IBuff {
    GameObject TheObj { get; set; }
    float Duration { get; set; }
    string BuffName { get; set;}
    void Effect();
    void ApplyAndAutoRemove();

    void Remove();
}