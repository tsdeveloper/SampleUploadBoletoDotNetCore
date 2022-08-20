using System.ComponentModel;
using System.Reflection;

namespace Core.Test.EnumFaker.UploadBoletos;

public static class EnumExtensionsFaker
{
    public static string DescriptionAttrFaker<T>(this T source)
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }
    
    public static string GetDescriptionFaker(Enum value)  
    {  
        var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();  
        var descriptionAttribute =  
            enumMember == null  
                ? default(DescriptionAttribute)  
                : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;  
        return  
            descriptionAttribute == null  
                ? value.ToString()  
                : descriptionAttribute.Description;  
    }

    public static bool GetEnumByDescriptionFaker<T>(this string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public|BindingFlags.Static))
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description.Equals(description))
                    return true;
            }
            else
            {
                if (field.Name == description)
                    return true;
            }
        }

        return false;
    }
}