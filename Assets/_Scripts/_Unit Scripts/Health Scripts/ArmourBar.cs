using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameProject.ProjectAssets.Units.UnitHealth
{
    public class ArmourBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetMaxArmour(int armour)
        {
            slider.maxValue = armour;
            slider.value = armour;

            fill.color = gradient.Evaluate(1f);
        }
        public void SetArmour(int armour)
        {
            slider.value = armour;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}