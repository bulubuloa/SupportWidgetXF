using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.Controllers.Validations
{
    public class ValidatePoolObject<T> : BaseModel, IValidity
    {
        private readonly List<IValidationRule<T>> validationRules;

        private ObservableCollection<string> errorList;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => validationRules;

        private string _CurrentValidMessage;
        public string CurrentValidMessage
        {
            get
            {
                return _CurrentValidMessage;
            }
            set
            {
                _CurrentValidMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Errors
        {
            get
            {
                return errorList;
            }
            set
            {
                errorList = value;
                OnPropertyChanged();
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        public ValidatePoolObject()
        {
            _isValid = true;
            errorList = new ObservableCollection<string>();
            validationRules = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();
            IEnumerable<string> errors = validationRules.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
            IsValid = Errors.Count == 0;
            CurrentValidMessage = IsValid ? string.Empty : Errors.ElementAt(0);
            return this.IsValid;
        }
    }
}