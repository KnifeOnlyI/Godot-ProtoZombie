namespace ProtoZombie.Scripts
{
    /// <summary>
    /// Contains all game constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The points gained for an any shot on an enemy
        /// </summary>
        public static readonly uint PointsPerShot = 10;

        /// <summary>
        /// The points gained for an headshot kill on an enemy
        /// </summary>
        public static readonly uint PointsPerHeadshotKill = 100;

        /// <summary>
        /// The points gained for a simple kill on an enemy (no headshot kill)
        /// </summary>
        public static readonly uint PointsPerKill = 50;
    }
}