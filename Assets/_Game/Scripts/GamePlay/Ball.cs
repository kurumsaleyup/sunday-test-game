using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [Header("Ball Physics Options")]
    [SerializeField] private float bounciness;
    [SerializeField] private float dynamicFriction;
    [SerializeField] private float staticFriction;
    [SerializeField] private PhysicMaterialCombine physicBounceCombine;
    [SerializeField] private PhysicMaterialCombine physicFrictionCombine;
    
    private Level _level;
    private PhysicMaterial _physicMaterial;


    #region Life Cycle

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Random.ColorHSV(0.2f,1,0.65f,1,0.5f,1);
        _physicMaterial = GetComponent<SphereCollider>().material;
        _physicMaterial.bounciness = bounciness;
        _physicMaterial.dynamicFriction = dynamicFriction;
        _physicMaterial.staticFriction = staticFriction;
        _physicMaterial.bounceCombine = physicBounceCombine;
        _physicMaterial.frictionCombine = physicFrictionCombine;
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
