using System.Collections.Generic;

namespace RedRift.Test.GameMechanic.Presenters
{
    public class InfoPresenter
    {
        private readonly IUIProcessing _iUIProcessing;
        private readonly List<Card> _cards;

        public InfoPresenter(IUIProcessing iUIProcessing, List<Card> cards)
        {
            _iUIProcessing = iUIProcessing;
            _cards = cards;
            CalculateValues();
            AddListeners();
        }

        private void CalculateValues()
        {
            var health = 0;
            var attack = 0;
            var mana = 0;

            foreach (var cardView in _cards)
            {
                health += cardView.GetHpCount();
                attack += cardView.GetAttackCount();
                mana += cardView.GetManaCount();
            }

            _iUIProcessing.SetValues(health, attack, mana);
        }

        private void AddListeners()
        {
            foreach (var cardView in _cards)
            {
                cardView.OnHealthChanged += CalculateValues;
                cardView.OnDestroyed += RemoveValue;
            }
        }

        private void RemoveValue(Card card)
        {
            card.OnHealthChanged -= CalculateValues;
            card.OnDestroyed -= RemoveValue;
            _cards.Remove(card);
            CalculateValues();
        }
    }
}