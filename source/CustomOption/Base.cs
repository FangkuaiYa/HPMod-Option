using System;
using System.Collections.Generic;

namespace CustomOption.CustomOption
{
    public class CustomOption
    {
        public static List<CustomOption> AllOptions = new List<CustomOption>();
        public readonly int ID;

        public Func<object, string> Format;
        public string Name;


        public CustomOption(int id, string name, CustomOptionType type, object defaultValue,
            Func<object, string> format = null)
        {
            ID = id;
            Name = name;
            Type = type;
            DefaultValue = Value = defaultValue;
            Format = format ?? (obj => $"{obj}");

            if (Type == CustomOptionType.Button) return;
            AllOptions.Add(this);
            Set(Value);
        }

        public object Value { get; set; }
        public OptionBehaviour Setting { get; set; }
        public CustomOptionType Type { get; set; }
        public object DefaultValue { get; set; }

        public static bool LobbyTextScroller { get; set; } = true;

        public bool Indent { get; set; }

        public override string ToString()
        {
            return Format(Value);
        }

        public virtual void OptionCreated()
        {
            Setting.name = Setting.gameObject.name = Name;
        }


        public void Set(object value, bool SendRpc = true)
        {
            System.Console.WriteLine($"{Name} set to {value}");

            Value = value;

            if (Setting != null && AmongUsClient.Instance.AmHost && SendRpc) Rpc.SendRpc(this);

            try
            {
                if (Setting is ToggleOption toggle)
                {
                    var newValue = (bool) Value;
                    toggle.oldValue = newValue;
                    if (toggle.CheckMark != null) toggle.CheckMark.enabled = newValue;
                }
                else if (Setting is NumberOption number)
                {
                    var newValue = (float) Value;

                    number.Value = number.oldValue = newValue;
                    number.ValueText.text = ToString();
                }
                else if (Setting is StringOption str)
                {
                    var newValue = (int) Value;

                    str.Value = str.oldValue = newValue;
                    str.ValueText.text = ToString();
                }
            }
            catch
            {
            }
        }
    }
}