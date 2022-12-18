using System.Collections.Generic;
using DG.Tweening;
using RedRift.Test.Common;
using UnityEngine;

namespace RedRift.Test.GameMechanic.Holder
{
    public class CardHolderView : CommonBehaviour, IHolder
    {
        [SerializeField] private RectTransform _rectTransform;

        private int _centerId;
        private int _startX;
        private int _startY;
        private int _startAngle;
        private int _indexCard;

        private int _xDiff = 80;
        private int _yDiff = 10;
        private int _angle = 10;
        private int _moveDuration = 1;

        public void RebuildCards(List<Card> cards)
        {
            int currentCoutCards = cards.Count;

            if (cards.Count % 2 == 1)
            {
                _centerId = GetCenterCard(cards.Count);
                _startX = _centerId * -_xDiff;
                _startY = _centerId * -_yDiff;
                _startAngle = _centerId * _angle;
                _indexCard = 0;
            }
            else
            {
                _centerId = GetCenterCard( cards.Count);
                _startX = GetDifference(_xDiff);

                _startY = (_centerId - 1) * -_yDiff;
                _startAngle = _centerId * _angle - _angle / 2;
                _indexCard = 1;
            }

            RotateCard(cards);
        }

        private int GetCenterCard(int currentCountCards)
        {
            var center = (currentCountCards - 1) / 2;
            return center;
        }

        private int GetDifference(int Diff)
        {
            var start = _centerId * -Diff + Diff / 2;
            return start;
        }

        private void RotateCard(List<Card> cards)
        {
            foreach (var card in cards)
            {
                MovingCard(card, new Vector3(_startX, _startY));
                card.GetRectTransform().rotation = Quaternion.Euler(0f, 0f, _startAngle);

                _startX += _xDiff;
                _startY += _indexCard < _centerId ? _yDiff : -_yDiff;
                _startAngle -= _angle;

                _indexCard++;
            }
        }

        private void MovingCard(Card card, Vector3 destination)
        {
            card.GetRectTransform().DOLocalMove(destination, _moveDuration);
        }
    }
}