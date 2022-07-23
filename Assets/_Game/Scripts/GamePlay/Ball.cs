using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Level _level;
    
    #region Life Cycle

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Random.ColorHSV(0.2f,1,0.65f,1,0.5f,1);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -20f)
        {
            _level.BallDidFallOff();
            Destroy(gameObject);
        }
    }

    #endregion


    #region Trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TubeExit"))
        {
            transform.parent = null;
        }
    }

    #endregion
    
}
