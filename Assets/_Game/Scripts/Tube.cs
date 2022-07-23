using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tube : MonoBehaviour
{
    [Header("Ball Configurations")] 
    [SerializeField] [Range(20, 100)] private int ballCount = 40;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float gapBetweenBalls = 0.4f;

    
    #region Editor

    public void CreateBalls()
    {
        var balls = GetComponentsInChildren<Ball>();
        foreach (var ball in balls)
        {
            DestroyImmediate(ball.gameObject);
        }

        for (int i = 0; i < ballCount; i++)
        {
            var pos = transform.position + (Vector3.down*0.5f) + 
                      (Vector3.forward * Random.Range(-gapBetweenBalls, gapBetweenBalls)) +
                      (Vector3.up * Random.Range(-gapBetweenBalls, gapBetweenBalls)) +
                      (Vector3.right * Random.Range(-gapBetweenBalls,gapBetweenBalls));
            
            Instantiate(ballPrefab, pos, Quaternion.identity, transform);
        }
    }

    #endregion
}