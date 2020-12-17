using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarWpf
{
    class Astar
    {
        public enum AstarNodeStatus
        {
            Start = 1,
            End = 2,
            Open = 3,
            Closed = 4,
            Path = 5,
            Block = 6,
            Free = 7
        }

        public class AllGrid
        {
            public string AllGridId { get; set; }
            public int[,] AllGridCoor { get; set; }
            public AstarNodeStatus AllGridStatus { get; set; }
            public int AllGridX { get; set; }
            public int AllGridY { get; set; }

        }

        public class OpenList
        {
            public string OpenId { get; set; }
            public AstarNodeStatus OpenStatus { get; set; }
            public int OpenX { get; set; }
            public int OpenY { get; set; }
            public double OpenFValue { get; set; }
            public string OpenParentNode { get; set; }
        }

        public class ClosedList
        {
            public string ClosedId { get; set; }
            public AstarNodeStatus ClosedStatus { get; set; }
            public int ClosedX { get; set; }
            public int ClosedY { get; set; }
            public double ClosedFValue { get; set; }
            public string ClosedParentNode { get; set; }
        }

        public class PathNode
        {
            public string PathId { get; set; }
            public int PathNodeX { get; set; }
            public int PathNodeY { get; set; }
            public AstarNodeStatus PathStatus { get; set; }
        }

        // used for blocks/obstacles
        public class NodeClicked
        {
            public string NodeClickedId { get; set; }
            public int NodeClickedX { get; set; }
            public int NodeClickedY { get; set; }
            public AstarNodeStatus GridStatus { get; set; }
        }

        public class FNodes
        {
            public double FValue { get; set; }
            public int FX { get; set; }
            public int FY { get; set; }
            public string FId { get; set; }
        }

        public class RandomXY
        {
            public static int RandomStartX { get; set; }
            public static int RandomStartY { get; set; }
            public static int RandomEndX { get; set; }
            public static int RandomEndY { get; set; }
            public static int RandomBlockX { get; set; }
            public static int RandomBlockY { get; set; }
        }

    }    
}
