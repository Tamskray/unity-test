using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Invoke("DestroyVFXEffect", lifeTime);

    }

    private void DestroyVFXEffect()
    {
        Destroy(gameObject);
    }
}
