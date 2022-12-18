using System;

public interface IUIProcessing
{
    event Action OnChangeValueButtonClicked;
    void SetRangeHp(int min, int max);
    void SetValues(int health, int attack, int mana);
}