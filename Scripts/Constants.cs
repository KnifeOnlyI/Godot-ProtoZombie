using Godot;

namespace ProtoZombie.Scripts
{
    /// <summary>
    /// Contains all game constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The points gained for an any shot on an enemy
        /// </summary>
        public const uint PointsPerShot = 10;

        /// <summary>
        /// The points gained for a simple kill on an enemy (no headshot kill)
        /// </summary>
        public const uint PointsPerKill = 50;
    }
}