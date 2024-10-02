using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDestroyAfterLifetime : MonoBehaviour
{

    [SerializeField] private float lifetime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
