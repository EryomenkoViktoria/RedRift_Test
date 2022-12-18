using RedRift.Test.Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RedRift.Test.GameMechanic.Views
{
    public class UIProcess : CommonBehaviour, IUIProcessing
    {
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _manaText;

        [SerializeField] private Button _changeValueButton;
        [SerializeField] private TextMeshProUGUI _textButton;

        public event Action OnChangeValueButtonClicked;

        private void OnEnable()
        {
            _changeValueButton.onClick.AddListener(ChangeClicked);
        }

        private void OnDisable()
        {
            _changeValueButton.onClick.RemoveListener(ChangeClicked);
        }

        public void SetRangeHp(int min, int max)
        {
            _textButton.text = $"Random Hp {min}..{max}";
        }

        private void ChangeClicked()
        {
            OnChangeValueButtonClicked?.Invoke();
        }

        public void SetValues(int health, int attack, int mana)
        {
            _hpText.text = $"HP: {health}";
            _attackText.text = $"Attack: {attack}";
            _manaText.text = $"Mana: {mana}";
        }
    }
}