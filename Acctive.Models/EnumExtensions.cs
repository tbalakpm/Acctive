using System;
using System.Linq;

namespace Acctive.Models
{
    public class EnumExtensions
    {
        public static T ParseEnum<T>(string value)
        {
            return ParseEnum<T>(value, false);
        }

        public static T ParseEnum<T>(string value, bool partial)
        {
            if (partial)
                value = Enum.GetNames(typeof(T)).Where(e => e.Substring(0, value.Length).Equals(value, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return (T)Enum.Parse(typeof(T), value, true);
        }

        //public static T ParseObject<T>(string value)
        //{
        //    return (T)Enum.ToObject(typeof(T), value);
        //}
    }
}