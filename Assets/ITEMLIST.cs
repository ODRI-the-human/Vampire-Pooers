using System.ComponentModel;

public enum ITEMLIST
{
    HP25, // Increases HP by 25
    HP50, // Increases HP by 50
    DMGADDPT5, // Increases damage stat by 0.5
    DMGMLT2, // Doubles damage. Make it so it stacks additively.
    FIRERATE, // Increases fire rate by 15%
    SOY, // Divides damage by 3, multiplies fire rate by 5.
    HOMING, // Homing shots
    ATG, // ATG ripoff
    MORESHOT, // Fire +1 extra shot in an arc.
    WAPANT, // Every few seconds, release a pool of liquid that slows any enemies that touch it.
    HOLYMANTIS, // First damage you take in a round is halved.
    CONVERTER, // At the end of each round, 3% of the %HP you're missing becomes a damage multiplier. Stacks additively.
    EASIERTIMES, // 30% chance to avoid taking damage.
    STOPWATCH, // Enemy movespeed, shot speed and firerate reduced.
    BOUNCY, // Your bullets bounce +1 time.
    FOURDIRMARTY, // Every 5 shots, shoot 4 bullets in the cardinal directions.
    PIERCING, // Piercing shots
    CREEP // Leave creep on the ground.
    // Contact damage with an enemy pushes all enemies away from you, dealing 50 damage.
    // Deal 50 damage to any nearby enemies when you dodge.
    // Dodge length and iframes increased
    // Gain an orbital that shoots the same projectiles as you, dealing 1/3 the damage, and blocks enemy shots.
    // Gain a long-range orbital that blocks shots and deals damage to enemies.
    // Bullets that hit you are reflected back as a piercing shot that deals double the damage of the enemy's shot.
    // Every frame you're not firing, get a temporary damage and firerate up.
    // HP and damage up
    // Shot speed and speed up.
    // Range and firerate up.
    // 10% chance on hit to leave a bit of creep on the ground.
}