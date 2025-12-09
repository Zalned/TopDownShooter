using System.Collections.Generic;

public class CardStat {
    public string StatText;
    public bool IsPositiveMod;
    public CardStat( string statText, bool isPositiveMod ) {
        StatText = statText;
        IsPositiveMod = isPositiveMod;
    }
}

public class CardData {
    public string Title;
    public string Description;
    public List<CardStat> CardStats;
    public List<IMod> Mods;
    public bool Enabled;
}

public class CardDatas {
    public List<CardData> Cards = new() {

        // Bullet
        { new() {
            Title = "Example", Description = "Example",
            Enabled = false,
            Mods = new() {new ReloadMod( 0.5f )},
            CardStats = new() {new CardStat("Example", true) },}
        },

        { new() {
            Title = "Damage", Description = "Increase bullet damage",
            Enabled = true,
            Mods = new() {new DamageMod( 0.5f )},
            CardStats = new() {new CardStat("+50% Bullet damage", true) },}
        },

        { new() {
            Title = "Penetration", Description = "Add bullet penetration",
            Enabled = true,
            Mods = new() {new PenetrationMod( 1 )},
            CardStats = new() {new CardStat("+1 penetration", true) },}
        },

        { new() {
            Title = "Bounce", Description = "Add bullet bounce",
            Enabled = true,
            Mods = new() {new BounceMod( 2 )},
            CardStats = new() {new CardStat("+2 bounces", true) },}
        },

        { new() {
            Title = "Explosion", Description = "Add bullet splash",
            Enabled = true,
            Mods = new() {new SplashMod( )},
            CardStats = new() {new CardStat("+Splash", true) },}
        },

        // Player
        { new() {
            Title = "Health", Description = "Increase max health",
            Enabled = true,
            Mods = new() {new HealthMod( 0.5f )},
            CardStats = new() {new CardStat("+50% Health", true) },}
        },

        { new() {
            Title = "Reload", Description = "Decrease reload time",
            Enabled = true,
            Mods = new() {new ReloadMod( 0.5f )},
            CardStats = new() {new CardStat("-50% Reload time", true) },}
        },

        // Dash
        { new() {
            Title = "More dashes", Description = "Increase dash count",
            Enabled = true,
            Mods = new() {new DashCountMod( 2 )},
            CardStats = new() {new CardStat("+2 dashes", true) },}
        },

        { new() {
            Title = "Dash length", Description = "Increase dash length",
            Enabled = true,
            Mods = new() {new DashLengthMod( 1f )},
            CardStats = new() {new CardStat("+100% Dash length", true) },}
        },
    };
}