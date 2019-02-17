using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fracking
{
    public class Map : ReadOnlyCollection<ReadOnlyCollection<Ground>>, IEnumerator<Map>
    {
        #region FIELDS

        private readonly IList<ReadOnlyCollection<Ground>> _original;

        #endregion FIELDS


        #region CONSTRUCTORS

        public Map(IEnumerable<IEnumerable<bool>> underground)
            : this(Convert(underground))
        {
        }

        public Map(IList<ReadOnlyCollection<Ground>> list) : base(list)
        {
            _original = list;
            ResetConnections();
            ResetHasChangeds();
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public bool IsNotCollapsed { get; private set; }
        public int Repetitions { get; private set; }

        public Map Current => this;

        object IEnumerator.Current => Current;

        #endregion PROPERTIES


        #region METHODS

        public bool MoveNext()
        {
            ResetHasChangeds();
            Frack();

            ResetHasChangeds();
            WriteMap(this);

            CalculateIsCollapsed();

            WriteMap(this);

            return IsNotCollapsed;
        }

        private void Frack()
        {
            for (var y = 0; y < Count; y++)
            for (var x = 0; x < this[y].Count; x++)
            {
                if (!this[y][x].IsHard || this[y][x].HasChanged)
                    continue;

                if (y - 1 >= 0 && !this[y - 1][x].OldValue)
                    this[y][x].IsHard = false;

                else if (y + 1 < Count && !this[y + 1][x].OldValue)
                    this[y][x].IsHard = false;

                else if (x - 1 >= 0 && !this[y][x - 1].OldValue)
                    this[y][x].IsHard = false;

                else if (x + 1 < this[y].Count && !this[y][x + 1].OldValue)
                    this[y][x].IsHard = false;
            }
        }

        private void ResetHasChangeds()
        {
            foreach (var row in this)
            foreach (var ground in row)
            {
                ground.OldValue = ground.IsHard;
                ground.HasChanged = false;
            }
        }

        private void CalculateIsCollapsed()
        {
            ResetConnections();
            ValidateGrounds();

            WriteMap(this);

            Repetitions++;
            
            IsNotCollapsed = false;
            if (!this[0].Any(x => x.IsHard))
                return;

            foreach (var row in this)
            foreach (var ground in row)
                if (ground.IsHard && !ground.HasConnection)
                    return;

            IsNotCollapsed = true;
        }

        private void ValidateGrounds()
        {
            for (var i = 0; i < this[0].Count; i++)
            {
                if (!this[0][i].IsHard || this[0][i].HasConnection)
                    continue;

                this[0][i].HasConnection = true;
                ValidateGrounds(0, i);
            }
        }

        private void ValidateGrounds(int y, int x)
        {
            if (y - 1 >= 0 && this[y - 1][x].IsHard && !this[y - 1][x].HasConnection)
            {
                this[y - 1][x].HasConnection = true;
                ValidateGrounds(y - 1, x);
            }
            if (y + 1 < Count && this[y + 1][x].IsHard && !this[y + 1][x].HasConnection)
            {
                this[y + 1][x].HasConnection = true;
                ValidateGrounds(y + 1, x);
            }
            if (x - 1 >= 0 && this[y][x - 1].IsHard && !this[y][x - 1].HasConnection)
            {
                this[y][x - 1].HasConnection = true;
                ValidateGrounds(y, x - 1);
            }
            if (x + 1 < this[y].Count && this[y][x + 1].IsHard && !this[y][x + 1].HasConnection)
            {
                this[y][x + 1].HasConnection = true;
                ValidateGrounds(y, x + 1);
            }
        }

        private void ResetConnections()
        {
            foreach (var row in this)
            foreach (var ground in row)
                ground.HasConnection = false;
        }


        public void Reset()
        {
            for (var y = 1; y < Count - 1; y++)
            for (var x = 1; x < this[y].Count - 1; x++)
            {
                this[y][x].IsHard = _original[y][x].IsHard;
                this[y][x].HasChanged = false;
            }
        }

        public void Dispose()
        {
        }

        private static IList<ReadOnlyCollection<Ground>> Convert(IEnumerable<IEnumerable<bool>> underground)
        {
            return underground
                .Select(row => new ReadOnlyCollection<Ground>(row.Select(x => new Ground {IsHard = x}).ToList()))
                .ToList();
        }

        public static void WriteMap(Map map)
        {
            foreach (var row in map)
            {
                foreach (var ground in row)
                {
                    Console.BackgroundColor = ground.IsHard
                        ? ConsoleColor.DarkGray
                        : ConsoleColor.Black;

                    Console.ForegroundColor = ground.HasConnection
                        ? ConsoleColor.White
                        : ConsoleColor.Red;

                    Console.Write(ground.OldValue ? 1 : 0);
                }

                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        #endregion METHODS
    }
}