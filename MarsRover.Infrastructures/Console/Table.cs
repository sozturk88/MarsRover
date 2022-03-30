using MarsRover.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Infrastructures.Console
{
    /// <summary>
    /// Creating Table At Console App
    /// </summary>
    public class Table
    {
        private const string TopLeftJoint = "┌";
        private const string TopRightJoint = "┐";
        private const string BottomLeftJoint = "└";
        private const string BottomRightJoint = "┘";
        private const string TopJoint = "┬";
        private const string BottomJoint = "┴";
        private const string LeftJoint = "├";
        private const string MiddleJoint = "┼";
        private const string RightJoint = "┤";
        private const char HorizontalLine = '─';
        private const string VerticalLine = "│";

        private readonly List<string[]> _rows = new List<string[]>();

        public int Padding { get; set; } = 1;
        public bool RowTextAlignRight { get; set; }

        #region Public
        /// <summary>
        /// Drawing Table On Console
        /// </summary>
        public void DrawTable()
        {
            System.Console.Write(this.ToString());
            System.Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Is Position Empty On Working Area ?
        /// </summary>
        /// <param name="roverPosition"></param>
        /// <returns></returns>
        public bool IsPositionEmpty(Vector2Int roverPosition)
        {
            return string.IsNullOrEmpty(_rows[roverPosition.Y - 1][roverPosition.X - 1]);
        }

        /// <summary>
        /// Move Rover Point To Another Position
        /// </summary>
        /// <param name="roverPoint">New Location For Working Area Table</param>
        /// <param name="clearRoverPoint">Clear Loocation For Working Area Table</param>
        public void SetRoverPointTable(Vector2Rover roverPoint, Vector2Rover clearRoverPoint = null)
        {
            _rows[roverPoint.Position.Y - 1][roverPoint.Position.X - 1] = roverPoint.Direction.ToString();
            if(clearRoverPoint is not null)
                _rows[clearRoverPoint.Position.Y - 1][clearRoverPoint.Position.X - 1] = string.Empty;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaSize"></param>
        public void SetAreaSize(Vector2Int areaSize)
        {            
            for (int i = 0; i < areaSize.Y; i++)
            {
                var horizontalSize = new string[areaSize.X];
                for (int j = 0; j < areaSize.X; j++)
                {
                    horizontalSize[j] = string.Empty;
                }

                AddRow(horizontalSize);
            }
        }

        /// <summary>
        /// Clear Rows
        /// </summary>
        public void ClearRows()
        {
            _rows.Clear();
        }

        /// <summary>
        /// Table String To Draw
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var table = new List<string[]>();

            if (_rows?.Any() == true)
            {
                for (var x = 0; x < _rows.Count; x++)
                {
                    table.Add(new string[_rows[0].Length]);
                    for (var y = 0; y < _rows[0].Length; y++)
                    {
                        table[x][y] = string.Empty;
                    }
                }
            }

            if (_rows?[0].Length > 0)
            {
                for (var x = 0; x < _rows[0].Length; x++)
                {
                    for (var y = 0; y < _rows.Count; y++)
                    {
                        table[_rows.Count - y - 1][x] = _rows[y][x];
                    }
                }
            }

            if (!table.Any())
                return string.Empty;

            var formattedTable = new StringBuilder();

            var previousRow = table.FirstOrDefault();
            var nextRow = table.FirstOrDefault();

            var maximumCellWidths = GetMaxCellWidths(table);

            formattedTable = CreateTopLine(maximumCellWidths, nextRow.Count(), formattedTable);

            var rowIndex = 0;
            var lastRowIndex = table.Count - 1;

            for (var i = 0; i < table.Count; i++)
            {
                var row = table[i];

                var align = RowTextAlignRight;

                formattedTable = CreateValueLine(maximumCellWidths, row, align, formattedTable);

                previousRow = row;

                if (rowIndex != lastRowIndex)
                {
                    nextRow = table[rowIndex + 1];
                    formattedTable = CreateSeperatorLine(maximumCellWidths, previousRow.Count(), nextRow.Count(), formattedTable);
                }

                rowIndex++;
            }

            formattedTable = CreateBottomLine(maximumCellWidths, previousRow.Count(), formattedTable);

            return formattedTable.ToString();
        }
        #endregion Public

        #region Private
        private void AddRow(params string[] row)
        {
            _rows.Add(row);
        }

        private int[] GetMaxCellWidths(List<string[]> table)
        {
            var maximumColumns = 0;
            foreach (var row in table)
            {
                if (row.Length > maximumColumns)
                    maximumColumns = row.Length;
            }

            var maximumCellWidths = new int[maximumColumns];
            for (int i = 0; i < maximumCellWidths.Count(); i++)
                maximumCellWidths[i] = 0;

            var paddingCount = 0;
            if (Padding > 0)
            {
                //Padding is left and right
                paddingCount = Padding * 2;
            }

            foreach (var row in table)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    var maxWidth = row[i].Length + paddingCount;

                    if (maxWidth > maximumCellWidths[i])
                        maximumCellWidths[i] = maxWidth;
                }
            }

            return maximumCellWidths;
        }

        private StringBuilder CreateTopLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (var i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TopLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), TopRightJoint));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", TopLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TopJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), TopRightJoint));
                else
                    formattedTable.Append(string.Format("{0}{1}", TopJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
            }

            return formattedTable;
        }

        private StringBuilder CreateBottomLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (var i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), BottomRightJoint));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", BottomLeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), BottomRightJoint));
                else
                    formattedTable.Append(string.Format("{0}{1}", BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
            }

            return formattedTable;
        }

        private StringBuilder CreateValueLine(int[] maximumCellWidths, string[] row, bool alignRight, StringBuilder formattedTable)
        {
            var cellIndex = 0;
            var lastCellIndex = row.Length - 1;

            var paddingString = string.Empty;
            if (Padding > 0)
                paddingString = string.Concat(Enumerable.Repeat(' ', Padding));

            foreach (var column in row)
            {
                var restWidth = maximumCellWidths[cellIndex];
                if (Padding > 0)
                    restWidth -= Padding * 2;

                var cellValue = alignRight ? column.PadLeft(restWidth, ' ') : column.PadRight(restWidth, ' ');

                if (cellIndex == 0 && cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", VerticalLine, paddingString, cellValue, paddingString, VerticalLine));
                else if (cellIndex == 0)
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", VerticalLine, paddingString, cellValue, paddingString));
                else if (cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", VerticalLine, paddingString, cellValue, paddingString, VerticalLine));
                else
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", VerticalLine, paddingString, cellValue, paddingString));

                cellIndex++;
            }

            return formattedTable;
        }

        private StringBuilder CreateSeperatorLine(int[] maximumCellWidths, int previousRowColumnCount, int rowColumnCount, StringBuilder formattedTable)
        {
            var maximumCells = Math.Max(previousRowColumnCount, rowColumnCount);

            for (var i = 0; i < maximumCells; i++)
            {
                if (i == 0 && i == maximumCells - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), RightJoint));
                }
                else if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", LeftJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                }
                else if (i == maximumCells - 1)
                {
                    if (i > previousRowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TopJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), TopRightJoint));
                    else if (i > rowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), BottomRightJoint));
                    else if (i > previousRowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), TopRightJoint));
                    else if (i > rowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), BottomRightJoint));
                    else
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine), RightJoint));
                }
                else
                {
                    if (i > previousRowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TopJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                    else if (i > rowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", BottomJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                    else
                        formattedTable.Append(string.Format("{0}{1}", MiddleJoint, string.Empty.PadLeft(maximumCellWidths[i], HorizontalLine)));
                }
            }

            return formattedTable;
        }
                
        #endregion Private

    }
}
