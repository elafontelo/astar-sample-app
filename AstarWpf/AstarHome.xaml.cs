using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAutoGrid;

namespace AstarWpf
{
    /// <summary>
    /// Interaction logic for AstarHome.xaml
    /// </summary>
    public partial class AstarHome : Page
    {
        //public bool gridClicked;

        public AstarHome()
        {
            InitializeComponent();
            ShowGrid();
        }

        int row = 25;
        int col = 25;
        public bool gridClicked = false;
        public bool restartClicked = false;
        int startX, startY, endX, endY;
        List<Astar.NodeClicked> blocks = new List<Astar.NodeClicked>();
        List<Astar.PathNode> pathNode = new List<Astar.PathNode>();
        List<Astar.AllGrid> allGrid = new List<Astar.AllGrid>();
        System.Windows.Controls.Primitives.ToggleButton toggleBtn = new ToggleButton();


        #region methods

        public void Random()
        {
            List<int> randomList = new List<int>();

            Random rnd = new Random();

            // Random X and Y
            for (int i = 0; i < row; i++)
            {
                randomList.Add(i);
                Astar.RandomXY.RandomStartX = rnd.Next(randomList.Count);
                Astar.RandomXY.RandomStartY = rnd.Next(randomList.Count);
                Astar.RandomXY.RandomEndX = rnd.Next(randomList.Count);
                Astar.RandomXY.RandomEndY = rnd.Next(randomList.Count);
            }

            // Random x and y for blocks (obstacles)
            for (int j = 0; j < (row * 4)*3; j++)
            {
                Astar.RandomXY.RandomBlockX = rnd.Next(randomList.Count);
                Astar.RandomXY.RandomBlockY = rnd.Next(randomList.Count);

                if (!((Astar.RandomXY.RandomBlockX == Astar.RandomXY.RandomStartX && Astar.RandomXY.RandomBlockY == Astar.RandomXY.RandomStartY)
                    || (Astar.RandomXY.RandomBlockX == Astar.RandomXY.RandomEndX && Astar.RandomXY.RandomBlockY == Astar.RandomXY.RandomEndY)))
                {
                    blocks.Add(new Astar.NodeClicked
                    {
                        NodeClickedId = (Astar.RandomXY.RandomBlockX.ToString() + Astar.RandomXY.RandomBlockY.ToString()),
                        NodeClickedX = Astar.RandomXY.RandomBlockX,
                        NodeClickedY = Astar.RandomXY.RandomBlockY,
                        GridStatus = Astar.AstarNodeStatus.Block
                    });
                }
            }

        }

        public void ShowGrid()
        {
            //System.Windows.Controls.Primitives.ToggleButton toggleBtn = new ToggleButton();

            Random();
            //int startX = Astar.RandomXY.RandomStartX;
            //int startY = Astar.RandomXY.RandomStartY;
            //int endX = Astar.RandomXY.RandomEndX;
            //int endY = Astar.RandomXY.RandomEndY;

            //string startId = (startX.ToString() + startY.ToString());
            //string endId = (endX.ToString() + endY.ToString());

            //int startX = 1;
            //int startY = 1;
            //int endX = 9;
            //int endY = 4;

            //blocks.AddRange(new List<Astar.NodeClicked>
            //{
            //    { new Astar.NodeClicked { NodeClickedId = "12", NodeClickedX = 1, NodeClickedY = 2} },
            //    { new Astar.NodeClicked { NodeClickedId = "23", NodeClickedX = 2, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "32", NodeClickedX = 3, NodeClickedY = 2} },
            //    { new Astar.NodeClicked { NodeClickedId = "43", NodeClickedX = 4, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "52", NodeClickedX = 5, NodeClickedY = 2} },
            //    { new Astar.NodeClicked { NodeClickedId = "63", NodeClickedX = 6, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "73", NodeClickedX = 7, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "81", NodeClickedX = 8, NodeClickedY = 1} },
            //    { new Astar.NodeClicked { NodeClickedId = "82", NodeClickedX = 8, NodeClickedY = 2} },
            //    { new Astar.NodeClicked { NodeClickedId = "83", NodeClickedX = 8, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "91", NodeClickedX = 9, NodeClickedY = 1} },
            //    { new Astar.NodeClicked { NodeClickedId = "102", NodeClickedX = 10, NodeClickedY = 2} },
            //    { new Astar.NodeClicked { NodeClickedId = "103", NodeClickedX = 10, NodeClickedY = 3} },
            //    { new Astar.NodeClicked { NodeClickedId = "104", NodeClickedX = 10, NodeClickedY = 4} },
            //    { new Astar.NodeClicked { NodeClickedId = "105", NodeClickedX = 10, NodeClickedY = 5} }
            //});

            if (!restartClicked)
            {
                for (int i = 0; i < col; i++)
                {
                    ColumnDefinition gridCol = new ColumnDefinition();
                    gridCol.Name = "Column" + i.ToString();
                    DynamicGrid.ColumnDefinitions.Add(gridCol);
                }

                for (int i = 0; i < row; i++)
                {
                    RowDefinition gridRow = new RowDefinition();
                    gridRow.Name = "Row" + i.ToString();
                    DynamicGrid.RowDefinitions.Add(gridRow);
                }
            }

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    System.Windows.Controls.Primitives.ToggleButton toggleBtn = new ToggleButton();
                    toggleBtn.VerticalAlignment = VerticalAlignment.Stretch;
                    toggleBtn.HorizontalAlignment = HorizontalAlignment.Stretch;
                    Grid.SetColumn(toggleBtn, x);
                    Grid.SetRow(toggleBtn, y);
                    DynamicGrid.Children.Add(toggleBtn);

                    string i = x.ToString();
                    string j = y.ToString();
                    allGrid.Add(new Astar.AllGrid()
                    {
                        AllGridId = (x.ToString() + y.ToString()),
                        AllGridStatus = Astar.AstarNodeStatus.Free,
                        AllGridX = x,
                        AllGridY = y
                    });
                    toggleBtn.Uid = (x.ToString() + y.ToString());

                    //if (x == startX && y == startY)
                    //{
                    //    tb.Background = Brushes.Red;
                    //    tb.ToolTip = "Start";
                    //}
                    //else if (x == endX && y == endY)
                    //{
                    //    tb.Background = Brushes.ForestGreen;
                    //    tb.ToolTip = "End";
                    //}

                    // insert black obstacles
                    if (blocks.Exists(f => f.NodeClickedX == x && f.NodeClickedY == y))
                    {
                        toggleBtn.Background = Brushes.Black;
                        toggleBtn.ToolTip = "Obstacle";
                    }
                }
            }

        }
                
        public void runBtn_Click(object sender, RoutedEventArgs e)
        {

            //int startX = Astar.RandomXY.RandomStartX;
            //int startY = Astar.RandomXY.RandomStartY;
            //int endX = Astar.RandomXY.RandomEndX;
            //int endY = Astar.RandomXY.RandomEndY;

            //string startId = (startX.ToString() + startY.ToString());
            //string endId = (endX.ToString() + endY.ToString());

            //int startX = 1;
            //int startY = 1;
            //int endX = 9;
            //int endY = 4;

            bool pathFound = false;

            List<int> heulist = new List<int>();
            List<Astar.FNodes> fList = new List<Astar.FNodes>();
            List<Astar.ClosedList> closedList = new List<Astar.ClosedList>();
            List<Astar.OpenList> openList = new List<Astar.OpenList>();

            closedList.Add(new Astar.ClosedList
            {
                ClosedId = (startX.ToString() + startY.ToString()),
                ClosedStatus = Astar.AstarNodeStatus.Start,
                ClosedFValue = 0,
                ClosedX = startX,
                ClosedY = startY
            });


            // Path plan starts here
            var newHeu = 0;
            int newNodeX, newNodeY;

            while (!pathFound)
            {
                int[,] direction = new int[,] { {startX, startY+1}, {startX+1, startY}, {startX, Math.Abs(startY-1)}, {Math.Abs(startX-1), startY},
                                                {startX+1, startY+1}, {startX+1, Math.Abs(startY-1)}, {Math.Abs(startX-1), Math.Abs(startY-1)}, {Math.Abs(startX-1), startY+1} };

                //int[,] direction = new int[,] { { startX, startY + 1 }, { startX + 1, startY }, { startX, Math.Abs(startY - 1) }, { Math.Abs(startX - 1), startY } };

                int G = 1;
                double diagonalG = Math.Sqrt(2);

                for (int i = 0; i < 8; i++)
                {
                    newNodeX = direction[i, 0];
                    newNodeY = direction[i, 1];
                    int[,] newNode = new int[newNodeX, newNodeY];

                    var newNodeId = (newNodeX.ToString() + newNodeY.ToString());
                    var newNodeHeu = Math.Abs(endX - newNodeX) + Math.Abs(endY - newNodeY);


                    if (!(blocks.Exists(f => f.NodeClickedId == newNodeId)))
                    {
                        // S, E, N, W directions (horizontal and vertical paths)
                        // F = G + H; G = 1 
                        var newParentNodeId = "";

                        if (i < 4)
                        {
                            double newNodeF = G + newNodeHeu;

                            if (openList.Exists(f => f.OpenId == newNodeId))
                            {
                                var prevIndex = openList[openList.FindIndex(f => f.OpenId == newNodeId)];
                                var prevFValue = prevIndex.OpenFValue;
                                var changeInF = newNodeF;

                                if (newNodeF < prevFValue)
                                {
                                    // change F values of existing open node
                                    newNodeF = changeInF;
                                    var currentNodeIndex = openList[openList.FindIndex(f => f.OpenId == newNodeId)];
                                    currentNodeIndex.OpenParentNode = (startX.ToString() + startY.ToString());
                                }
                            }

                            else
                            {
                                openList.Add(new Astar.OpenList
                                {
                                    OpenStatus = Astar.AstarNodeStatus.Open,
                                    OpenFValue = newNodeF,
                                    OpenId = (newNodeX.ToString() + newNodeY.ToString()),
                                    OpenParentNode = (startX.ToString() + startY.ToString()),
                                    OpenX = newNodeX,
                                    OpenY = newNodeY
                                });
                                fList.Add(new Astar.FNodes()
                                {
                                    FValue = newNodeF,
                                    FId = (newNodeX.ToString() + newNodeY.ToString()),
                                    FX = newNodeX,
                                    FY = newNodeY
                                });
                            }
                        }
                        // SE, NE, NW, SW directions (diagonal paths)
                        // F = G + H; G = 1.4 = sqt(2)
                        else
                        {
                            double newNodeF = diagonalG + newNodeHeu;

                            if (openList.Exists(f => f.OpenId == newNodeId))
                            {
                                var prevIndex = openList[openList.FindIndex(f => f.OpenId == newNodeId)];
                                var prevFValue = prevIndex.OpenFValue;

                                if (newNodeF < prevFValue)
                                {
                                    prevIndex.OpenFValue = newNodeF;
                                    newNodeF = fList[fList.FindIndex(f => f.FId == newNodeId)].FValue;
                                    newParentNodeId = prevIndex.OpenId;
                                }
                            }

                            else
                            {
                                openList.Add(new Astar.OpenList
                                {
                                    OpenStatus = Astar.AstarNodeStatus.Open,
                                    OpenFValue = newNodeF,
                                    OpenId = (newNodeX.ToString() + newNodeY.ToString()),
                                    OpenParentNode = (startX.ToString() + startY.ToString()),
                                    OpenX = newNodeX,
                                    OpenY = newNodeY
                                });
                                fList.Add(new Astar.FNodes()
                                {
                                    FValue = newNodeF,
                                    FId = (newNodeX.ToString() + newNodeY.ToString()),
                                    FX = newNodeX,
                                    FY = newNodeY
                                });
                            }
                        }
                    }

                }

                bool minValueFound = false;

                int n = -1;
                foreach (Astar.OpenList item in openList)
                {
                    double minValue = openList.Min(f => f.OpenFValue);

                    if (item.OpenFValue == minValue)
                        n = openList.IndexOf(item);
                }

                var itemIndex = openList[n];

                if (closedList.Exists(f => f.ClosedId == itemIndex.OpenId))
                {
                    int i = 0;
                    while (!minValueFound)
                    {

                        var fListSort = fList.OrderBy(f => f.FValue).ToList();

                        var minF = fListSort[i];

                        if (!closedList.Exists(f => f.ClosedId == minF.FId))
                        {
                            minValueFound = true;
                            n = openList.FindIndex(f => f.OpenId == fListSort[i].FId);
                            itemIndex = openList[n];
                        }

                        i++;
                    }
                }

                // if node is added on closed list 
                if (!(closedList.Exists(f => f.ClosedId == itemIndex.OpenId)))
                {

                    closedList.Add(new Astar.ClosedList
                    {
                        ClosedFValue = openList[n].OpenFValue,
                        ClosedStatus = Astar.AstarNodeStatus.Closed,
                        ClosedX = openList[n].OpenX,
                        ClosedY = openList[n].OpenY,
                        ClosedParentNode = openList[n].OpenParentNode,
                        ClosedId = (openList[n].OpenX.ToString() + openList[n].OpenY.ToString())
                    });

                    startX = itemIndex.OpenX;
                    startY = itemIndex.OpenY;
                    newHeu = Math.Abs(endX - startX) + Math.Abs(endY - startY);
                    heulist.Add(newHeu);

                }

                if (closedList.Last().ClosedX == endX && closedList.Last().ClosedY == endY)
                    pathFound = true;
                //}
            }


            var closedLastIndex = closedList.Count() - 1;

            // determining paths
            // adding closed nodes to the path list 
            // NOTE: closed nodes chosen based on parent node
            for (int i = closedLastIndex; i > 0; i--)
            {
                if (closedList[i].ClosedId != closedList.First().ClosedParentNode)
                {
                    var pathParent = closedList[i].ClosedParentNode;
                    var pathIndex = closedList[closedList.FindIndex(f => f.ClosedId == pathParent)];

                    if (pathIndex.ClosedId != closedList[0].ClosedId)
                    {
                        // stores all the nodes considered as paths
                        pathNode.Add(new Astar.PathNode { PathId = pathIndex.ClosedId, PathStatus = Astar.AstarNodeStatus.Path, PathNodeX = pathIndex.ClosedX, PathNodeY = pathIndex.ClosedY });

                        System.Windows.Controls.Primitives.ToggleButton b = new ToggleButton();
                        b.VerticalAlignment = VerticalAlignment.Stretch;
                        b.HorizontalAlignment = HorizontalAlignment.Stretch;
                        Grid.SetColumn(b, pathNode.Last().PathNodeX);
                        Grid.SetRow(b, pathNode.Last().PathNodeY);
                        DynamicGrid.Children.Add(b);

                        b.Background = Brushes.DodgerBlue;

                        i = closedList.FindIndex(f => f.ClosedId == pathParent) + 1;
                    }
                }

            }

        }

        #endregion
        public bool startClicked;
        public bool endClicked;
        int prevCount;
        List<Astar.AllGrid> btnList = new List<Astar.AllGrid>();
        List<Astar.AllGrid> startList = new List<Astar.AllGrid>();
        List<Astar.AllGrid> endList = new List<Astar.AllGrid>();

        private void rstrtBtn_Click(object sender, RoutedEventArgs e)
        {
            restartClicked = true;
            blocks.Clear();
            ShowGrid();
            prevCount = 0;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            endClicked = false;
            startClicked = true;
            
        }

        private void endBtn_Click(object sender, RoutedEventArgs e)
        {
            startClicked = false;
            endClicked = true;
            
        }

        private void DynamicGrid_Click(object sender, RoutedEventArgs e)
        {
            var btnClicked = allGrid[allGrid.FindIndex(f=>f.AllGridId == ((System.Windows.UIElement)e.Source).Uid)];

            //btnList.Add(btnClicked);

            //var lastInd = btnList.Last();

            //if (btnList.Count >= 2)
            //{
            //    if ((!endClicked && startClicked) || (endClicked && !startClicked))
            //    {
            //        var prevInd = btnList[btnList.Count - 2];

            //        System.Windows.Controls.Primitives.ToggleButton tb = new ToggleButton();
            //        tb.VerticalAlignment = VerticalAlignment.Stretch;
            //        tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            //        Grid.SetColumn(tb, prevInd.AllGridX);
            //        Grid.SetRow(tb, prevInd.AllGridY);
            //        DynamicGrid.Children.Add(tb);
            //    }

            //}

            System.Windows.Controls.Primitives.ToggleButton tb = new ToggleButton();
            tb.VerticalAlignment = VerticalAlignment.Stretch;
            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            Grid.SetColumn(tb, btnClicked.AllGridX);
            Grid.SetRow(tb, btnClicked.AllGridY);
            DynamicGrid.Children.Add(tb);

            
            if (startClicked)
            {
                startList.Add(btnClicked);
                tb.Background = Brushes.Red;
                tb.Uid = btnClicked.AllGridId;

                if (startList.Count >= 2)
                {
                    var prevInd = startList[startList.Count - 2];

                    System.Windows.Controls.Primitives.ToggleButton b = new ToggleButton();
                    b.VerticalAlignment = VerticalAlignment.Stretch;
                    b.HorizontalAlignment = HorizontalAlignment.Stretch;
                    Grid.SetColumn(b, prevInd.AllGridX);
                    Grid.SetRow(b, prevInd.AllGridY);
                    DynamicGrid.Children.Add(b);

                    if (btnClicked.AllGridId == prevInd.AllGridId)
                    {
                        if (prevCount % 2 != 0)
                            b.Background = Brushes.Red;

                        b.Uid = btnClicked.AllGridId;
                        prevCount++;
                    }

                    b.Uid = prevInd.AllGridId;
                }
                
            }

            if (endClicked)
            {
                endList.Add(btnClicked);
                tb.Background = Brushes.ForestGreen;
                tb.Uid = btnClicked.AllGridId;

                if (endList.Count >= 2)
                {
                    var prevInd = endList[endList.Count - 2];

                    System.Windows.Controls.Primitives.ToggleButton b = new ToggleButton();
                    b.VerticalAlignment = VerticalAlignment.Stretch;
                    b.HorizontalAlignment = HorizontalAlignment.Stretch;
                    Grid.SetColumn(b, prevInd.AllGridX);
                    Grid.SetRow(b, prevInd.AllGridY);
                    DynamicGrid.Children.Add(b);

                    if (btnClicked.AllGridId == prevInd.AllGridId)
                    {
                        if (prevCount % 2 != 0)
                            b.Background = Brushes.Red;

                        b.Uid = btnClicked.AllGridId;
                        prevCount++;
                    }

                    b.Uid = prevInd.AllGridId;
                }
                
            }

            if (startList.Count != 0 && endList.Count != 0)
            {
                startX = startList.Last().AllGridX;
                startY = startList.Last().AllGridY;
                endX = endList.Last().AllGridX;
                endY = endList.Last().AllGridY;
            }



        }
    }
}
