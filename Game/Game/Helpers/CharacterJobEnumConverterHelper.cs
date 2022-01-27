using Game.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Game.Helpers
{
    public class CharacterJobEnumConverter: IValueConverter
    {

            /// <summary>
            /// Converts a value to the String
            /// </summary>
            /// <param name="value"></param>
            /// <param name="targetType"></param>
            /// <param name="parameter"></param>
            /// <param name="culture"></param>
            /// <returns></returns>
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is Enum)
                {
                    //return (int)value;
                    return ((CharacterJobEnum)value).ToMessage();
                }

                if (value is string)
                {
                    // Convert String Enum and then Enum to Message
                    var myEnum = CharacterJobEnumHelper.ConvertMessageToEnum((string)value);
                    var myReturn = myEnum.ToMessage();

                    return myReturn;
                }

                return 0;
            }

            /// <summary>
            /// Converts a String to the Value
            /// </summary>
            /// <param name="value"></param>
            /// <param name="targetType"></param>
            /// <param name="parameter"></param>
            /// <param name="culture"></param>
            /// <returns></returns>
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is int)
                {
                    var myReturn = Enum.ToObject(targetType, value);
                    return ((CharacterJobEnum)myReturn).ToMessage();
                }

                if (value is string)
                {
                    // Convert the Message String to the Enum
                    var myReturn = CharacterJobEnumHelper.ConvertMessageToEnum((string)value);

                    return myReturn;
                }
                return 0;
            }
        
    }
}
