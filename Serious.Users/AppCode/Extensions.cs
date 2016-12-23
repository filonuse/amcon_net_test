using Serious.Users.AppCode.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serious.Users.AppCode
{
    public static class Extensions
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will only work if you assign the StringValue attribute to the items in your enum.
        /// </summary>
        /// <param name="value">Enumeration value</param>
        /// <returns>String value of the Enum or empty string if was not found</returns>
        public static string GetStringValue(this Enum value)
        {
            return ((object)value).GetStringValue();
        }

        public static string GetStringValue(this object value)
        {
            try
            {
                var type = value.GetType();
                var fieldInfo = type.GetField(value.ToString());
                var attribs =
                    fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                return attribs != null && attribs.Length > 0
                           ? attribs[0].StringValue
                           : string.Empty;
            }
            #region Exceptions
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.ToString());
                return string.Empty;
            }
            #endregion
        }
    }
}