using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public abstract void Hit();
    public abstract void GetDamage();
}