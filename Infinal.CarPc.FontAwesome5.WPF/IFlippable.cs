namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>Represents a flippable control</summary>
    public interface IFlippable
    {
        /// <summary>
        /// Gets or sets the current orientation (horizontal, vertical).
        /// </summary>
        EFlipOrientation FlipOrientation { get; set; }
    }
}
