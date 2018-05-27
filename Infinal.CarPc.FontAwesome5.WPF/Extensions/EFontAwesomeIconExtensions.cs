using System;
using System.Reflection;
using System.Windows.Media;

namespace Infinal.CarPc.FontAwesome5.WPF.Extensions
{
    /// <summary>EFontAwesomeIcon extensions</summary>
    public static class EFontAwesomeIconExtensions
    {
        /// <summary>Get the Font Awesome Style of an icon</summary>
        public static EFontAwesomeStyle GetStyle(this EFontAwesomeIcon icon)
        {
            FontAwesomeInformationAttribute informationAttribute = icon.GetInformationAttribute<FontAwesomeInformationAttribute>();
            return informationAttribute?.Style ?? EFontAwesomeStyle.None;
        }

        /// <summary>Get the SVG path of an icon</summary>
        public static bool GetSvg(this EFontAwesomeIcon icon, out string path, out int width, out int height)
        {
            path = string.Empty;
            width = -1;
            height = -1;
            FontAwesomeSvgInformationAttribute informationAttribute = icon.GetInformationAttribute<FontAwesomeSvgInformationAttribute>();
            if (informationAttribute == null)
            {
                return false;
            }

            path = informationAttribute.Path;
            width = informationAttribute.Width;
            height = informationAttribute.Height;
            return true;
        }

        /// <summary>Get the Unicode of an icon</summary>
        public static string GetUnicode(this EFontAwesomeIcon icon)
        {
            FontAwesomeInformationAttribute informationAttribute = icon.GetInformationAttribute<FontAwesomeInformationAttribute>();
            if (informationAttribute == null)
            {
                return char.ConvertFromUtf32(0);
            }

            return char.ConvertFromUtf32(informationAttribute.Unicode);
        }

        /// <summary>Get the Typeface of an icon</summary>
        public static Typeface GetTypeFace(this EFontAwesomeIcon icon)
        {
            FontAwesomeInformationAttribute informationAttribute = icon.GetInformationAttribute<FontAwesomeInformationAttribute>();
            if (informationAttribute == null)
            {
                return Fonts.RegularTypeface;
            }

            switch (informationAttribute.Style)
            {
                case EFontAwesomeStyle.Solid:
                    return Fonts.SolidTypeface;
                case EFontAwesomeStyle.Regular:
                    return Fonts.RegularTypeface;
                case EFontAwesomeStyle.Brands:
                    return Fonts.BrandsTypeface;
                default:
                    return null;
            }
        }

        /// <summary>Get the FontFamily of an icon</summary>
        public static FontFamily GetFontFamily(this EFontAwesomeIcon icon)
        {
            FontAwesomeInformationAttribute informationAttribute = icon.GetInformationAttribute<FontAwesomeInformationAttribute>();
            if (informationAttribute == null)
            {
                return Fonts.RegularFontFamily;
            }

            switch (informationAttribute.Style)
            {
                case EFontAwesomeStyle.Solid:
                    return Fonts.SolidFontFamily;
                case EFontAwesomeStyle.Regular:
                    return Fonts.RegularFontFamily;
                case EFontAwesomeStyle.Brands:
                    return Fonts.BrandsFontFamily;
                default:
                    return null;
            }
        }

        internal static T GetInformationAttribute<T>(this EFontAwesomeIcon icon) where T : class
        {
            if (icon == EFontAwesomeIcon.None)
            {
                return null;
            }

            MemberInfo[] member = typeof(EFontAwesomeIcon).GetMember(icon.ToString());
            if (member.Length == 0)
            {
                throw new Exception("EFontAwesomeIcon not found.");
            }

            object[] customAttributes = member[0].GetCustomAttributes(typeof(T), false);
            if (customAttributes.Length == 0)
            {
                throw new Exception("FontAwesomeInformationAttribute not found.");
            }

            return customAttributes[0] as T;
        }
    }
}
