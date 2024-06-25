using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class OBlock : Block
    {
            private readonly Position[][] tiles = new Position[][]
            {
            new Position[] {new (0,0), new (1,1), new (0,1), new (1,0)},
           

            };
            public override int Id => 2;

            protected override Position[][] Tiles => tiles;

            protected override Position StartOffset => new Position(0,4);
    }
}
