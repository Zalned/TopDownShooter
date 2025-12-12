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
            Title = "Piercing", Description = "",
            Enabled = true,
            Mods = new() {new PenetrationMod( 1 ), new BounceMod(1), new DamageMod(0.25f), new ReloadMod(0.25f)},
            CardStats = new() {new ("+1 Penetration", true), new ("+1 Bounce", true),
                               new ("+25% Damage",true ), new ("+0.25s Reload time",false) } }
        },

        { new() {
            Title = "Bouncy", Description = "",
            Enabled = true,
            Mods = new() {new BounceMod( 2 ), new DamageMod(0.25f), new ReloadMod(0.25f)},
            CardStats = new() {new ("+2 Bounces", true), new ("+25% Damage", false),
                                new ("+0.25s Reload time", false)}}
        },

        { new() {
            Title = "Explosive bullet", Description = "",
            Enabled = true,
            Mods = new() {new ExplosionMod( 3f ), new ReloadMod(0.25f), new AttackSpeedMod(-1f) },
            CardStats = new() {new ("+Explosion", true), new ("+0.25s Reload time", false),
                               new ("+100% ATKSPD", false) }}
        },

        { new() {
            Title = "Glass cannon", Description = "",
            Enabled = true,
            Mods = new() { new DamageMod(1f), new HealthMod(-1f), new ReloadMod (0.25f) },
            CardStats = new() {new ("+100% Damage", true),new ("-100% Health", false),
                new ("+0.25s Reload time", false)},}
        },

        { new() {
            Title = "Leech", Description = "",
            Enabled = false,
            Mods = new() { new LifeStealMod(1f), new HealthMod(0.3f) },
            CardStats = new() {new ("+75% Life steel", true),new ("+30% Health", true)},}
        },

        { new() {
            Title = "Mayhem", Description = "",
            Enabled = true,
            Mods = new() {new BounceMod( 5 ), new DamageMod(-0.15f), new ReloadMod(0.5f)},
            CardStats = new() {new ("+5 Bounces", true),new ("-15% Damage", false),
                new ("+0.5s Reload time", false)}}
        },

        { new() {
            Title = "Quick Reload", Description = "",
            Enabled = true,
            Mods = new() { new ReloadMod(-0.65f) },
            CardStats = new() {new ("-65% Reload time", true) },}
        },

        { new() {
            Title = "Huge", Description = "",
            Enabled = true,
            Mods = new() { new HealthMod(0.80f) },
            CardStats = new() {new ("+80% Health", true) },}
        },

        { new() {
            Title = "Wind Up", Description = "",
            Enabled = true,
            Mods = new() { new BulletSpeedMod(1f), new DamageMod(0.6f), new AttackSpeedMod(-1f), new ReloadMod(0.5f) },
            CardStats = new() {new ("+100% Bullet speed", true), new ("+60% Damage", true),
                new ("-100% Atack speed", false), new ("+0.5s Reload time", false) }}
        },

        // Player


        // Dash
        { new() {
            Title = "Big dash", Description = "",
            Enabled = true,
            Mods = new() { new HealthMod(0.50f), new DashLengthMod(0.5f), new DashCountMod(1) },
            CardStats = new() {new ("+50% Health", true), new ("+50% Dash Length", true), new ("+1 Dash", true) }}
        }
    };
}