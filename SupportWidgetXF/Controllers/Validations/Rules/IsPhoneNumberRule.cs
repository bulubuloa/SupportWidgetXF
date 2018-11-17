using System;
namespace SupportWidgetXF.Controllers.Validations.Rules
{
    public class IsPhoneNumberRule<T> : IValidationRule<T>
    {
        public IsPhoneNumberRule()
        {
            ValidationMessage = "";
        }

        public string ValidationMessage
        {
            get; set;
        }

        public bool Check(T value)
        {
            var str = value as string;
            var util = new RegexUtilities();
            return util.isValidPhone(str);
        }
    }
}
