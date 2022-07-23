using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{

    #region Life Cycle

    private void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Random.ColorHSV(0.2f,1,0.65f,1,0.5f,1);
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y > 1f)
        {
            transform.parent = null;
        }
    }

    #endregion
}
