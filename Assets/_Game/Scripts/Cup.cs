using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cup : MonoBehaviour
{

    [SerializeField] private Tube tube;
    private List<Ball> _balls = new List<Ball>();
    private TextMeshProUGUI _textMeshProUGUI;

    
    #region Life Cycle

    private void Start()
    {
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        _textMeshProUGUI.text = _balls.Count + "/" + tube.BallCount;
    }

    #endregion

    
    #region Trigger Events

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            if (_balls.Contains(ball))
            {
                return;
            }
            
            _balls.Add(ball);
            _textMeshProUGUI.text = _balls.Count + "/" + tube.BallCount;
        }
    }

    #endregion
}
