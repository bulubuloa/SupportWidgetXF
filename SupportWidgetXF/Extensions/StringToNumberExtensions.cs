using System;
using System.Globalization;
using Xamarin.Forms;

namespace SupportWidgetXF.Extensions
{
    public static class StringToNumberExtensions
    {
        public static CultureInfo GetCurrentCulture()
        {
            CultureInfo usUS = new CultureInfo("en-US");
            usUS.NumberFormat.NumberDecimalSeparator = ".";
            usUS.NumberFormat.NumberGroupSeparator = ",";
            return usUS;
        }

        public static string ClearText(this string source)
        {
            try
            {
                return source.Replace("[^a-zA-Z0-9,.]", "");
            }
            catch (Exception ex)
            {
                return source;
            }
        }

        public static double FangDouble(this string source)
        {
            try
            {
                var stringRemovedGroup = source.Replace(GetCurrentCulture().NumberFormat.NumberGroupSeparator, "");
                return double.Parse(stringRemovedGroup);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public static int FangInt(this string source)
        {
            try
            {
                var stringRemovedGroup = source.Replace(GetCurrentCulture().NumberFormat.NumberGroupSeparator, "");
                return Int32.Parse(stringRemovedGroup);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public static string FangToIntegerNonFormated(this TextChangedEventArgs args)
        {
            try
            {
                CultureInfo usUS = GetCurrentCulture();

                var stringRemovedGroup = args.NewTextValue.Replace(usUS.NumberFormat.NumberGroupSeparator, "");
                if (stringRemovedGroup.Length == 0)
                    return "";
                if (stringRemovedGroup[stringRemovedGroup.Length - 1].Equals('.'))
                    return args.OldTextValue;

                return args.NewTextValue;
            }
            catch (Exception ex)
            {
                return args.OldTextValue;
            }
        }

        public static string FangToIntegerFormated(this TextChangedEventArgs args)
        {
            try
            {
                CultureInfo usUS = GetCurrentCulture();

                var stringRemovedGroup = args.NewTextValue.Replace(usUS.NumberFormat.NumberGroupSeparator, "");
                if (stringRemovedGroup.Length == 0)
                    return "";
                if (stringRemovedGroup[stringRemovedGroup.Length - 1].Equals('.'))
                    return args.OldTextValue;

                int numInt = 0;

                if (Int32.TryParse(stringRemovedGroup, out numInt))
                {
                    return numInt.ToString("N0", usUS);
                }

                return args.OldTextValue;
            }
            catch (Exception ex)
            {
                return args.OldTextValue;
            }
        }

        public static string FangToCurrencyFormated(this TextChangedEventArgs args)
        {
            try
            {
                CultureInfo usUS = GetCurrentCulture();

                var stringRemovedGroup = args.NewTextValue.Replace(usUS.NumberFormat.NumberGroupSeparator, "");
                if (stringRemovedGroup.Length == 0)
                    return "";
                if (stringRemovedGroup.Split('.').Length > 2)
                    return args.OldTextValue;
                if (stringRemovedGroup[stringRemovedGroup.Length - 1].Equals('.'))
                    return args.NewTextValue;

                int numInt = 0;
                double numDouble = 0d;

                if (Int32.TryParse(stringRemovedGroup, out numInt))
                {
                    return numInt.ToString("N0", usUS);
                }
                else if (double.TryParse(stringRemovedGroup, out numDouble))
                {
                    int numOfAfterGroup = stringRemovedGroup.Split('.')[1].Length;
                    var result = numDouble.ToString("N" + numOfAfterGroup, usUS);
                    return result;
                }
                return args.OldTextValue;
            }
            catch (Exception ex)
            {
                return args.OldTextValue;
            }
        }

        public static string FangToCurrencyFormated(this string args)
        {
            try
            {
                CultureInfo usUS = GetCurrentCulture();

                var stringRemovedGroup = args.Replace(usUS.NumberFormat.NumberGroupSeparator, "");
                if (stringRemovedGroup.Length == 0)
                    return "";
                if (stringRemovedGroup.Split('.').Length > 2)
                    return args.Substring(0, args.Length - 1);
                if (stringRemovedGroup[stringRemovedGroup.Length - 1].Equals('.'))
                    return args;

                int numInt = 0;
                double numDouble = 0d;

                if (Int32.TryParse(stringRemovedGroup, out numInt))
                {
                    return numInt.ToString("N0", usUS);
                }
                else if (double.TryParse(stringRemovedGroup, out numDouble))
                {
                    int numOfAfterGroup = stringRemovedGroup.Split('.')[1].Length;
                    var result = numDouble.ToString("N" + numOfAfterGroup, usUS);
                    return result;
                }
                return args.Substring(0, args.Length - 1); ;
            }
            catch (Exception ex)
            {
                return args.Substring(0, args.Length - 1);
            }
        }


        public static string FangToDoubleFormated(this string input)
        {
            try
            {
                CultureInfo usUS = GetCurrentCulture();
                var stringRemovedGroup = input.Replace(usUS.NumberFormat.NumberGroupSeparator, "");

                int numInt = 0;
                double numDouble = 0d;
                if (Int32.TryParse(stringRemovedGroup, out numInt))
                {
                    return numInt.ToString("N0", usUS);
                }
                else if (double.TryParse(stringRemovedGroup, out numDouble))
                {
                    if (stringRemovedGroup[stringRemovedGroup.Length - 1].Equals('.'))
                        return input;
                    if (stringRemovedGroup.Split('.').Length > 2)
                        return input;
                    int numOfAfterGroup = stringRemovedGroup.Split('.')[1].Length;
                    var result = numDouble.ToString("N" + numOfAfterGroup, usUS);
                    return result;
                }
                return input;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}