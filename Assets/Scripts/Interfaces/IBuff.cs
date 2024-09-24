using System;
using UnityEngine;

public interface IBuff {
    GameObject TheObj { get; set; }
    float Duration { get; set; }
    string BuffName { get; set;}
    void Effect();
    void EffectAndAutoRemove();

    void Remove();
    void EffectControl();
    void RemoveControl();
}