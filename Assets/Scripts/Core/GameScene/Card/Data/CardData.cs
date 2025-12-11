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
    public string Title = "unknown";
    public string Description = "unknown";
    public List<CardStat> CardStats = new( new List<CardStat>() { new( "unknown", false ) } );
    public List<IMod> Mods = new();
    public bool Enabled = false;
}

public class CardDatas {
    public List<CardData> Cards = new() {

        { new() {
            Title = "Example", Description = "Example",
            Enabled = false,
            Mods = new() {},
            CardStats = new() {new CardStat("Example", true) },}
        },

         //Bullet
        { new() {
            Title = "Damage", Description = "Increase bullet damage and speed",
            Enabled = true,
            Mods = new() {new DamageMod( 0.5f ), new BulletSpeedMod(0.5f), new ReloadMod(-0.15f)},
            CardStats = new() {new CardStat("+50% Bullet damage", true),
                                new CardStat("+50% Bullet speed", true),
                                new CardStat("+15% Reload time", false)},}
        },

        { new() {
            Title = "Piercing", Description = "Add bullet penetration and damage",
            Enabled = true,
            Mods = new() {new PenetrationMod( 1 ), new DamageMod(0.25f), new ReloadMod(-0.15f), new ShotCdMod(-1)},
            CardStats = new() {new ("+1 penetration", true),
                               new ("+50% Damage",true ),
                               new ("+15% Reload time",false),
                               new ("+100% Shot cooldown",false)}, }
        },

        { new() {
            Title = "Bounce", Description = "Add bullet bounces",
            Enabled = true,
            Mods = new() {new BounceMod( 2 ), new ReloadMod(-0.20f), new ShotCdMod(-0.75f) },
            CardStats = new() {new ("+2 Bounces", true),
                               new ("+20% Reload time", false),
                                new ("+75% Shot cooldown", false)},}
        },

        { new() {
            Title = "Explosion bullet", Description = "Add bullet explosion",
            Enabled = true,
            Mods = new() {new ExplosionMod( 6f ), new DamageMod(0.75f), new ReloadMod(-0.15f), new ShotCdMod(-1f) },
            CardStats = new() {new ("+Explosion", true),
                               new ("+75% Damage", true),
                               new ("+15% Reload time", false),
                               new ("+100% Shot cooldown", false) },}
        },

        { new() {
            Title = "Impulse", Description = "Increase bullet speed",
            Enabled = true,
            Mods = new() {new BulletSpeedMod(0.85f), new DamageMod(0.25f), new ShotCdMod(-1f) },
            CardStats = new() {new ("+85% Bullet speed", true),
                               new ("+25% Damage", true),
                               new ("+100% Shot cooldown", false)},}
        },

        // Player
        { new() {
            Title = "Health", Description = "Increase max health",
            Enabled = true,
            Mods = new() {new HealthMod( 0.75f )},
            CardStats = new() {new CardStat("+75% Health", true) },}
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
            Mods = new() {new DashLengthMod( 0.75f )},
            CardStats = new() {new CardStat("+75% Dash length", true) },}
        },
    };
}