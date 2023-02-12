﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model.Players
{
    /// <summary>
    /// Statistics represent various data collected through different games of bowling and are intrinsic to a particular player.
    /// We can for example cite the average score, the number of games won or the best result. 
    /// This class is therefore responsible for encapsulating this data.
    /// </summary>
    public class Statistics : IEquatable<Statistics>, IComparable, IComparable<Statistics>
    {
        public int ID { get; private set; }
        /// <summary>
        /// Represents the number of total wins by a player.
        /// </summary>
        public int NumberOfVictory { get; private set; }

        /// <summary>
        /// Represents the total number of games lost by a player.
        /// </summary>
        public int NumberOfDefeat { get; private set; }

        /// <summary>
        /// Represents the total number of games a player has participated in.
        /// </summary>
        public int NumberOfGames { get; private set; }

        /// <summary>
        /// Represents the player's best score.
        /// </summary>
        public int BestScore { get; private set; }

        public Statistics(int iD, int numberOfVictory, int numberOfDefeat, int numberOfGames, int bestScore)
        {
            ID = iD;
            NumberOfVictory = numberOfVictory;
            NumberOfDefeat = numberOfDefeat;
            NumberOfGames = numberOfGames;
            BestScore = bestScore;
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (obj.GetType() != typeof(Statistics)) return false;
            return Equals(obj as Statistics);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The statistics to compare with the actual stats.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(Statistics other)
        {
            return other != null 
                && other.BestScore == BestScore 
                && other.NumberOfDefeat== NumberOfDefeat
                && other.NumberOfVictory == NumberOfVictory
                && other.NumberOfGames == NumberOfGames;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(BestScore);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Statistics of {0} games. Victories: {1}. Defeats: {2}. \nBest Score: {3}", 
                NumberOfGames, NumberOfVictory, NumberOfDefeat, BestScore);
        }

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
            if (obj is not Statistics)
            {
                throw new ArgumentException("The argument is not a statistic.", nameof(obj));
            }
            return CompareTo((Statistics)obj);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as 
        /// the other object.
        /// </summary>
        /// <param name="other">The others statistics to compare to this.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public int CompareTo(Statistics other)
        {
            return BestScore.CompareTo(other.BestScore);
        }

        /// <summary>
        /// Returns true if its left-hand operand is less than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator <(Statistics left, Statistics right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns true if its left-hand operand is less than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator <=(Statistics left, Statistics right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator >(Statistics left, Statistics right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator >=(Statistics left, Statistics right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns true if its left-hand operand is equals to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator ==(Statistics left, Statistics right)
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
        public static bool operator !=(Statistics left, Statistics right)
            => !(left == right);
    }
}