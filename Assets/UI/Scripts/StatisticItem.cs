using System;
using Gameplay.Scripts.DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts
{
    public class StatisticItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _durationText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Image _background;
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;
        public void Initialize(Session session)
        {
            _scoreText.text = $"Score:{session.Score}";
            var time = TimeSpan.FromSeconds(session.Duration);
            _durationText.text = $"Time:{time.Minutes}:{time.Seconds}";
            _levelText.text = $"Level: {session.Level + 1}";
            
            _background.color = session.IsWin ? _winColor : _loseColor;
        }
    }
}