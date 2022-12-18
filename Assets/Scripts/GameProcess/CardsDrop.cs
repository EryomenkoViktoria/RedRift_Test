using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RedRift.Test.GameMechanic
{
    public class CardsDrop
    {
        private IHolder _iHolder;
        private RectTransform _cardsGameField;
        private List<Card> _cards;
        private bool _isFoldCenter;

        private int moveDuration = 1;

        public CardsDrop(IHolder iHolder, RectTransform cardsGameField, List<Card> cards)
        {
            _iHolder = iHolder;
            _cardsGameField = cardsGameField;
            _cards = cards;

            AddListeners();
        }

        private void AddListeners()
        {
            foreach (var card in _cards)
            {
                card.OnCardPicked += RemoveCard;
                card.OnCardDropped += CompareCardDrop;
                card.OnDestroyed += RemoveCard;
            }
        }

        private void CompareCardDrop(Card card)
        {
            bool result = CheckRectPosition(_cardsGameField).Overlaps(CheckRectPosition(card.GetRectTransform()));

            if (result)
            {
                if (_isFoldCenter)
                    MovingCard(card.GetRectTransform(), _cardsGameField.transform.position);
            }
            else
            {
                _cards.Add(card);
                card.transform.SetAsLastSibling();
                _iHolder.RebuildCards(_cards);
            }
        }

        private void RemoveCard(Card card)
        {
            _cards.Remove(card);
            _iHolder.RebuildCards(_cards);
        }

        private Rect CheckRectPosition(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        }

        private void MovingCard(Transform transform, Vector3 destination)
        {
            transform.DOMove(destination, moveDuration);
        }
    }
}