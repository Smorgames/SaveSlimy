using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
}
