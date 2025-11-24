using UnityEngine;

public static class ColorUtils {
    public static Color RGB255( byte r, byte g, byte b ) {
        return new Color( r / 255f, g / 255f, b / 255f );
    }

    public static Color RGB255( byte r, byte g, byte b, byte a ) {
        return new Color( r / 255f, g / 255f, b / 255f, a / 255f );
    }
}