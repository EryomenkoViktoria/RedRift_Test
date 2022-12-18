using RedRift.Test.GameMechanic.Buffering;

namespace RedRift.Test.GameMechanic.Presenters
{
    public class CardPresenter
    {
        private readonly Card _cards;
        private readonly NetConverter _cardModel;
        private int _limitDestroyCard;

        public CardPresenter(Card cards, NetConverter cardModel, int limit)
        {
            _cards = cards;
            _cardModel = cardModel;
            _limitDestroyCard = limit;

            AddListeners();
            cardModel.BufferingImages();
        }

        private void AddListeners()
        {
            _cardModel.ImageAnswer += _cards.CreateTexture;
            _cards.OnNewHealthReceived += SetNewHP;
        }

        private void SetNewHP(int hp)
        {
            if (hp < _limitDestroyCard)
                RemoveListeners();
            else
                _cards.ChangeHealth(hp);
        }

        private void RemoveListeners()
        {
            _cardModel.ImageAnswer -= _cards.CreateTexture;
            _cards.Destroy();
        }
    }
}