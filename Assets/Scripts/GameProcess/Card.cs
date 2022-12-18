using System;
using TMPro;
using UniRx;
using RedRift.Test.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedRift.Test.GameMechanic
{
    public class Card : CommonBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Outline _light;
        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _manaText;

        private Camera _camera;

        public event Action<Card> OnCardDropped;
        public event Action<Card> OnCardPicked;
        public event Action<Card> OnDestroyed;

        public event Action<int> OnNewHealthReceived;
        public event Action OnHealthChanged;

        public void ChangeHealth(int health)
        {
            float time = 0;
            int currentHealth = Convert.ToInt32(_hpText.text);

            Observable.EveryUpdate().Select
                (x =>
                {
                    time += Time.deltaTime / 1;
                    return
                        (int)Mathf.Lerp(currentHealth, health, time);
                })
                .TakeWhile(x => x < health).Subscribe(x => { _hpText.text = x.ToString(); }, () =>
                {
                    _hpText.text = health.ToString();
                    OnHealthChanged?.Invoke();
                });
        }

        public void CreateTexture(byte[] image)
        {
            var texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(image);
            texture2D.Apply();
            _image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        }

        public int GetAttackCount() => Converter(_attackText.text);

        public int GetHpCount() => Converter(_hpText.text);

        public int GetManaCount() => Converter(_manaText.text);

        private int Converter(string text)
        {
            return Convert.ToInt32(text);
        }

        public void ReceiveNewHealth(int health)
        {
            OnNewHealthReceived?.Invoke(health);
        }

        public RectTransform GetRectTransform()
        {
            return _rectTransform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _light.enabled = true;
            _rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _camera = Camera.main;
            transform.SetSiblingIndex(9);
            OnCardPicked?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var distanceToScreen = _camera.WorldToScreenPoint(gameObject.transform.position).z;
            var movePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            _rectTransform.position = new Vector3(movePosition.x, movePosition.y, transform.position.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _light.enabled = false;
            OnCardDropped?.Invoke(this);
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}