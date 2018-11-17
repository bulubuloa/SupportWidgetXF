using System;
namespace SupportWidgetXF.Controllers.Validations.Rules
{
    public class IsEmailRule<T> : IValidationRule<T>
    {
        public IsEmailRule()
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
            return util.IsValidEmail(str);
        }
    }
}
