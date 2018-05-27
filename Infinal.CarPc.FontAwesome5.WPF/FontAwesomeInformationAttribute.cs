using System;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>FontAwesome Information Attribute</summary>
    public class FontAwesomeInformationAttribute : Attribute
    {
        /// <summary>FontAwesome Style</summary>
        public EFontAwesomeStyle Style { get; set; }

        /// <summary>FontAwesome Label</summary>
        public string Label { get; set; }

        /// <summary>FontAwesome Unicode</summary>
        public int Unicode { get; set; }

        public FontAwesomeInformationAttribute(string label, EFontAwesomeStyle style, int unicode)
        {
            Label = label;
            Style = style;
            Unicode = unicode;
        }
    }
}
