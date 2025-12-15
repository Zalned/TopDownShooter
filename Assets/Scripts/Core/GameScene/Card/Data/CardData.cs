using System.Collections.Generic;

public class VisualCardStat {
    public string StatText;
    public bool IsPositiveMod;
    public VisualCardStat( string statText, bool isPositiveMod ) {
        StatText = statText;
        IsPositiveMod = isPositiveMod;
    }
}

public class CardData {
    public string Title = "unknown";
    public string Description = "unknown";
    public List<VisualCardStat> CardStats = new( new List<VisualCardStat>() { new( "unknown", false ) } );
    public List<IMod> Mods = new();
    public bool Enabled = false;
}

public class CardDatas {
    public List<CardData> Cards = new() {

        { new() {
            Title = "Example", Description = "Example",
            Enabled = false,
            Mods = new() {},
            CardStats = new() {new ("Example", true) },}
        },

         //Bullet
        { new() {
            Title = "Piercing", Description = "",
            Enabled = true,

            Mods = new() {new PenetrationMod( 1 ), new BounceMod(1),
                new DamageMod ( StatType.Percent, 0.25f), new ReloadMod(StatType.Additive, 0.25f)},

            CardStats = new() {new ("+1 Penetration", true), new ("+1 Bounce", true),
                new ("+25% Damage",true ), new ("+0.25s Reload time",false) } }
        },

        { new() {
            Title = "Bouncy", Description = "",
            Enabled = true,

            Mods = new() {new BounceMod( 2 ), new DamageMod( StatType.Percent, 0.25f),
                new ReloadMod(StatType.Additive, 0.25f)},

            CardStats = new() {new ("+2 Bounces", true), new ("+25% Damage", true),
                new ("+0.25s Reload time", false)}}
        },

        { new() {
            Title = "Explosive bullet", Description = "",
            Enabled = true,

            Mods = new() {new ExplosionMod( StatType.Additive, 3f ), new ReloadMod(StatType.Additive, 0.25f ),
                new AttackSpeedMod( StatType.Percent, 1f) },

            CardStats = new() {new ("+Explosion", true), new ("+0.25s Reload time", false),
                new ("+100% Attack speed", false) }}
        },

        { new() {
            Title = "Glass cannon", Description = "",
            Enabled = true,

            Mods = new() { new DamageMod(StatType.Percent, 1f), new HealthMod(StatType.Percent, -1f),
                new ReloadMod (StatType.Additive, 0.25f) },

            CardStats = new() {new ("+100% Damage", true),new ("-100% Health", false),
                new ("+0.25s Reload time", false)},}
        },

        { new() {
            Title = "Leech", Description = "",
            Enabled = false,

            Mods = new() { new LifeStealMod( StatType.Percent, 0.75f ), new HealthMod( StatType.Percent, 0.3f ) },
            CardStats = new() {new ("+75% Life steel", true),new ("+30% Health", true)},}
        },

        { new() {
            Title = "Mayhem", Description = "",
            Enabled = true,

            Mods = new() {new BounceMod( 5 ), new DamageMod( StatType.Percent, -0.15f),
                new ReloadMod(StatType.Additive, 0.5f)},

            CardStats = new() {new ("+5 Bounces", true),new ("-15% Damage", false),
                new ("+0.5s Reload time", false)}}
        },

        { new() {
            Title = "Wind Up", Description = "",
            Enabled = true,

            Mods = new() { new BulletSpeedMod(StatType.Percent, 1f), new DamageMod(StatType.Percent, 0.6f),
                new AttackSpeedMod(StatType.Percent, 0f), new ReloadMod(StatType.Additive, 0.5f) },

            CardStats = new() {new ("+100% Bullet speed", true), new ("+60% Damage", true),
                new ("-100% Atack speed", false), new ("+0.5s Reload time", false) }}
        },

        // Player
        { new() {
            Title = "Huge", Description = "",
            Enabled = true,

            Mods = new() { new HealthMod( StatType.Percent, 0.80f ) },
            CardStats = new() {new ("+80% Health", true) },}
        },

        { new() {
            Title = "Quick Reload", Description = "",
            Enabled = true,

            Mods = new() { new ReloadMod( StatType.Percent, -0.70f) },
            CardStats = new() {new ("-70% Reload time", true) },}
        },

        // Dash
        { new() {
            Title = "Big dash", Description = "",
            Enabled = true,

            Mods = new() { new HealthMod(StatType.Percent, 0.5f), new DashLengthMod(StatType.Percent, 0.5f),
                new DashCountMod(1) },

            CardStats = new() {new ("+50% Health", true), new ("+50% Dash Length", true), new ("+1 Dash", true) }}
        }
    };
}