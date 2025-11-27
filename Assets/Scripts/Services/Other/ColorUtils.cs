using UnityEngine;

public static class ColorUtils {
    public static Color RGB255( byte r, byte g, byte b ) {
        return new Color( r / 255f, g / 255f, b / 255f );
    }

    public static Color RGB255( byte r, byte g, byte b, byte a ) {
        return new Color( r / 255f, g / 255f, b / 255f, a / 255f );
    }

    public static Color GetComplementary( Color color ) {
        Color.RGBToHSV( color, out float h, out float s, out float v );
        h = (h + 0.5f) % 1f; // +180°
        return Color.HSVToRGB( h, s, v );
    }
}