using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Box.View
{
    public class ScoreView : MonoBehaviour
    {
        public TextMeshPro ScoreText;

        private int _score;

        void Start()
        {
            Box.OnDestroyed += OnDestroyed;
            UpdateUI();
        }

        private void OnDestroyed()
        {
            _score++;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ScoreText.text = _score.ToString();
        }

        private void OnDestroy()
        {
            Box.OnDestroyed -= OnDestroyed;
        }
    }
}