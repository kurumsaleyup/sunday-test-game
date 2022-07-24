using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private Tube tube;
    private List<Ball> _balls = new List<Ball>();
    private TextMeshProUGUI _textMeshProUGUI;
    private float _initialFontSize;
    private Level _level;


    #region Life Cycle

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        _textMeshProUGUI.text = _balls.Count + "/" + tube.BallCount;
        _initialFontSize = _textMeshProUGUI.fontSize;

    }

    #endregion


    #region Trigger Events

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Ball ball)) 
            return;
        
        if (_balls.Contains(ball))
        {
            return;
        }

        _balls.Add(ball);
        _level.BallDidGetInCup();
        _textMeshProUGUI.text = _balls.Count + "/" + tube.BallCount;
        var endSize = Mathf.Min(0.9f,_textMeshProUGUI.fontSize + 0.05f);
        DOTween.To(() => _textMeshProUGUI.fontSize, x => _textMeshProUGUI.fontSize = x, endSize, 0.05f)
            .OnComplete(()=>_textMeshProUGUI.fontSize = _initialFontSize);
    }
    #endregion

}



