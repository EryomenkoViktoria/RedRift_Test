using System.Collections.Generic;
using UnityEngine;
using RedRift.Test.Common;
using RedRift.Test.Data;
using RedRift.Test.GameMechanic.Presenters;
using RedRift.Test.GameMechanic.Buffering;

namespace RedRift.Test.GameMechanic
{
    public sealed class GameProcess : CommonBehaviour
    {
        [SerializeField] private GameConfiguration _config;
        [Space]
        [SerializeField] private GameObject _cardPrefab;
        [Space]
        [SerializeField] private GameObject _cardHolderView;
        private IHolder _iHolder;
        [SerializeField] private RectTransform _cardsGameField;
        [SerializeField] private GameObject _uIProcess;
        private IUIProcessing _iUIProcessing;

        private void OnEnable()
        {
            SetRefences();
            SetRangeHp();
            var cardsCount = CreaterCards(Random.Range(_config.Config.MinCountCard, _config.Config.MaxCountCard));
            
            CreateCardsDrop(cardsCount);
            CreateValueChange(cardsCount);
            CreateInfo(cardsCount);
        }

        private void SetRefences()
        {
            if (_cardHolderView.TryGetComponent(out IHolder holder))
                _iHolder = holder;
            if (_uIProcess.TryGetComponent(out IUIProcessing ui))
                _iUIProcessing = ui;
        }

        private void SetRangeHp()
        {
            _iUIProcessing.SetRangeHp(_config.Config.MinRandomChange, _config.Config.MaxRandomChange);
        }

        private List<Card> CreaterCards(int count)
        {
            var cards = new List<Card>();

            for (var i = 0; i < count; i++)
            {
                var card = Instantiate(_cardPrefab, _cardHolderView.transform).GetComponent<Card>();
                var buffer = new NetConverter(_config.Config.UrlImageResources);
                var presenter = new CardPresenter(card, buffer, _config.Config.MinHpForDestroyCard);
                cards.Add(card);
            }

            _iHolder.RebuildCards(cards);
            return cards;
        }

        private void CreateCardsDrop(List<Card> card)
        {
            var service = new CardsDrop(_iHolder, _cardsGameField, card);
        }

        private void CreateValueChange(List<Card> card)
        {
            var service = new ValueChange(_iUIProcessing, card, _config.Config.MinRandomChange, _config.Config.MaxRandomChange);
        }

        private void CreateInfo(List<Card> card)
        {
            var presenter = new InfoPresenter(_iUIProcessing, card);
        }
    }
}