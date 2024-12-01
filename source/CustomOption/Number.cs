using System;
using UnityEngine;

namespace CustomOption.CustomOption
{
    public class CustomNumberOption : CustomOption
    {
        public CustomNumberOption(int id, string name, float value, float min, float max, float increment,
            Func<object, string> format = null) : base(id, name, CustomOptionType.Number, value, format)
        {
            Min = min;
            Max = max;
            Increment = increment;
        }

        public CustomNumberOption(bool indent, int id, string name, float value, float min, float max,
            float increment,
            Func<object, string> format = null) : this(id, name, value, min, max, increment, format)
        {
            Indent = indent;
        }

        protected float Min { get; set; }
        protected float Max { get; set; }
        protected float Increment { get; set; }

        public float Get()
        {
            return (float) Value;
        }

       
        public void Increase()
        {
            var increment = Increment > 5 && Input.GetKeyInt(KeyCode.LeftShift)
                ? 5
                : Increment;
            Set(Mathf.Clamp(Get() + increment, Min, Max));
        }

        public void Decrease()
        {
            var increment = Increment > 5 && Input.GetKeyInt(KeyCode.LeftShift)
                ? 5
                : Increment;
            Set(Mathf.Clamp(Get() - increment, Min, Max));
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            var number = Setting.Cast<NumberOption>();

            number.TitleText.text = Name;
            number.ValidRange = new FloatRange(Min, Max);
            number.Increment = Increment;
            number.Value = number.oldValue = Get();
            number.ValueText.text = ToString();
        }
    }
}