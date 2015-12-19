using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    void OnAnimationFinish()
    {
        Destroy(gameObject);
    }
}
