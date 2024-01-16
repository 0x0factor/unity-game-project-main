using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameProject.ProjectAssets.Units.UnitHealth {
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            //set color of the fill
            fill.color = gradient.Evaluate(1f);
        }
        public void SetHealth(int health)
        {
            slider.value = health;

            //set color of the fill
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}