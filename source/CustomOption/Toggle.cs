namespace CustomOption.CustomOption
{
    public class CustomToggleOption : CustomOption
    {
        public CustomToggleOption(int id, string name, bool value = true) : base(id, name,
            CustomOptionType.Toggle,
            value)
        {
            Format = val => (bool) val ? "On" : "Off";
        }

        public bool Get()
        {
            return (bool) Value;
        }

        public void Toggle()
        {
            Set(!Get());
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
            Setting.Cast<ToggleOption>().CheckMark.enabled = Get();
        }
    }
}