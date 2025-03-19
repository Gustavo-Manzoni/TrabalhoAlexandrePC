using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction);
}
