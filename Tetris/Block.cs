using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block // have many type of block
    {
        protected abstract Position[][] Tiles { get; } // Each block is a max square to contain block inside, and Tiles is have block
        protected abstract Position StartOffset { get; } // Start in game, compair with board

        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
            rotationState = 0;
        }

        public IEnumerable<Position> TilePosition()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        public void RotateCCW()
        {
            rotationState = (rotationState - 1 + Tiles.Length) % Tiles.Length;
        }

        public void Move(int  x, int y)
        {
            offset.Row += x;
            offset.Column += y;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
