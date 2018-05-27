using System;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>FontAwesome SVG Information Attribute</summary>
    public class FontAwesomeSvgInformationAttribute : Attribute
    {
        /// <summary>FontAwesome SVG Path</summary>
        public string Path { get; set; }

        /// <summary>FontAwesome SVG Width</summary>
        public int Width { get; set; }

        /// <summary>FontAwesome SVG Height</summary>
        public int Height { get; set; }

        public FontAwesomeSvgInformationAttribute(string path, int width, int height)
        {
            Path = path;
            Width = width;
            Height = height;
        }
    }
}
