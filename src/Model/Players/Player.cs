using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players
{
    /// <summary>
    /// A Player represent someone with a name, an image (a profile picture), and a unique identifier.
    /// He also has some statisctics about his past games.
    /// </summary>
    public class Player : IEquatable<Player>, IComparable<Player>, IComparable
    {
        /// <summary>
        /// A unique identifier for the player.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The profil picture of the player.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// The player's statistics.
        /// </summary>
        //public Statistics Statistics { get; private set; }

        /// <summary>
        /// Create a new instance of Player.
        /// </summary>
        /// <param name="ID">The identifier of the player.</param>
        /// <param name="name">The name of the player.</param>
        /// <param name="image">The image of the player.</param>
        /// <exception cref="ArgumentNullException">If the player's name or image is null or empty.</exception>
        public Player(int ID, string name, string image/*, Statistics? statistics = null*/)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "The name cannot be null or empty");
            }
            this.ID = ID;
            Name = name;
            Image = image;
            //Statistics = statistics ?? new Statistics(this);
            //if (!Statistics.Player.Equals(this)) throw new ArgumentException("The player is not the owner of these statistics.");
        }

        /// <summary>
        /// Create a new player with an empty ID.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="image">The image of the player.</param>
        public Player(string name, string image) : this(0, name, image) { }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
            => ID.GetHashCode();

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Player)) return false;
            return Equals((Player)obj);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The player to compare with the actual player.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(Player other)
        {
            if (other == null) return false;
            if (ID == 0 || other.ID == 0) return Name.Equals(other.Name);
            return ID.Equals(other.ID);
        }

        /// <summary>
        /// Returns a string representing a player.
        /// </summary>
        /// <returns>A string representing a player.</returns>
        public override string ToString() => $"{ID} - {Name}";

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as 
        /// the other object.
        /// </summary>
        /// <param name="obj">The object to compare to this.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        /// <exception cref="ArgumentException">If obj is not Statistics.</exception>
        int IComparable.CompareTo(object obj)
        {
            if (obj is not Player)
            {
                throw new ArgumentException("The argument is not a player.", nameof(obj));
            }
            return CompareTo((Player)obj);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as 
        /// the other object.
        /// </summary>
        /// <param name="other">The other player to compare to this.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public int CompareTo(Player other)
        {
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Returns true if its left-hand operand is less than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator <(Player left, Player right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns true if its left-hand operand is less than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator <=(Player left, Player right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator >(Player left, Player right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator >=(Player left, Player right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns true if its left-hand operand is equals to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator ==(Player left, Player right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Returns true if its left-hand operand is not equals to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator !=(Player left, Player right)
            => !(left == right);
    }
}
