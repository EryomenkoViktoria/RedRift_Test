using System.Collections.Generic;
using UnityEngine;

namespace RedRift.Test.GameMechanic
{
    public class ValueChange
    {
        private IUIProcessing _uIProcess;
        private List<Card> _cards;

        private int _minCardValue;
        private int _maxCardValue;
        private int _currentIndexCard;

        public ValueChange(IUIProcessing uIProcess, List<Card> cards, int min, int max)
        {
            _uIProcess = uIProcess;
            _cards = cards;

            _minCardValue = min;
            _maxCardValue = max;

            AddListeners();
        }

        private void AddListeners()
        {
            _uIProcess.OnChangeValueButtonClicked += ChangeCardValue;

            foreach (var cardView in _cards)
            {
                cardView.OnDestroyed += RemoveValue;
            }
        }

        private void ChangeCardValue()
        {
            if (_cards.Count > 0)
            {
                var randomValue = Random.Range(_minCardValue, _maxCardValue);

                if (_cards[_currentIndexCard] != null)
                {
                    _cards[_currentIndexCard].ReceiveNewHealth(randomValue);
                    CardSequence();
                }
            }
        }

        private void CardSequence()
        {
            var correntIndex = _currentIndexCard;
            correntIndex += 1;

            if (correntIndex >= _cards.Count)
                _currentIndexCard = 0;
            else
                _currentIndexCard += 1;
        }

        private void RemoveValue(Card card)
        {
            _cards.Remove(card);
            _currentIndexCard -= 1;
        }
    }
}