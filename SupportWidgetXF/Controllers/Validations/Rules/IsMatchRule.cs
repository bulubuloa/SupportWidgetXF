using System;
namespace SupportWidgetXF.Controllers.Validations.Rules
{
    public class DataMatch
    {
        public string SourceMatch { set; get; }
    }
    public class IsMatchRule<T> : IValidationRule<T>
    {
        public DataMatch Match { set; get; }

        public IsMatchRule()
        {
            ValidationMessage = "";
        }

        public string ValidationMessage
        {
            get;
            set;
        }

        public bool Check(T value)
        {
            if (value == null)
                return false;
            if (Match == null)
                return false;
            var str = value as string;
            //Console.WriteLine("source = {0}",Match.SourceMatch);
            return str.Equals(Match.SourceMatch);
        }
    }
}