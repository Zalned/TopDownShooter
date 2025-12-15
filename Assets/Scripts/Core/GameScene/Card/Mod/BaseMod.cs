public abstract class BaseMod {
    protected StatType type;
    protected float value;

    public BaseMod( StatType type, float value ) {
        this.type = type;
        this.value = value;
    }
}