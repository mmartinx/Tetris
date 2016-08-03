using System.Collections.Generic;

namespace Tetris
{
    public class PieceReference
    {
        private readonly Dictionary<string, Point[]> _reference;
        private Point[] this[string key] => _reference[key];

        public Point[] GetPoints(char type, int state) => this[type + state.ToString()];

        public PieceReference()
        {
            _reference = new Dictionary<string, Point[]>
            {
                { "I1", new[] { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) } },
                { "I2", new[] { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) } },
                { "I3", new[] { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) } },
                { "I4", new[] { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) } },

                { "J1", new[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },
                { "J2", new[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(0, 2) } },
                { "J3", new[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "J4", new[] { new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, 2) } },

                { "L1", new[] { new Point(0, 1), new Point(0, 2), new Point(1, 1), new Point(2, 1) } },
                { "L2", new[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(1, 2) } },
                { "L3", new[] { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "L4", new[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 2) } },

                { "O1", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O2", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O3", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O4", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },

                { "S1", new[] { new Point(1, 1), new Point(2, 1), new Point(0, 2), new Point(1, 2) } },
                { "S2", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },
                { "S3", new[] { new Point(1, 1), new Point(2, 1), new Point(0, 2), new Point(1, 2) } },
                { "S4", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },

                { "T1", new[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },
                { "T2", new[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) } },
                { "T3", new[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "T4", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },

                { "Z1", new[] { new Point(0, 1), new Point(1, 1), new Point(1, 2), new Point(2, 2) } },
                { "Z2", new[] { new Point(2, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },
                { "Z3", new[] { new Point(0, 1), new Point(1, 1), new Point(1, 2), new Point(2, 2) } },
                { "Z4", new[] { new Point(2, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } }
            };
        }
    }
}
