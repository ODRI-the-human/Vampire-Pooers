using System.ComponentModel;

public enum ITEMOWNERS
{
    BULLET, // Only bullets get this.
    BEING, // Only player and enemies get this.
    ALL, // Everybody liked this (everybody gets this)
    MASTERANDPLAYER, // Master and player (wow)
    CANSHOOT, // Different from being, in that orbitals also get this item, not just enemies/player.
}