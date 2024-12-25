using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace SYA
{
    public class SearchPaginationHelper
    {
        public const int PageSize = 50; // Number of rows to load per page
        public int currentOffset = 0; // Tracks the current offset for lazy loading
        public bool isLoading = false; // Prevents multiple fetches during loading
        public DataTable loadedTable = new DataTable(); // Table to hold all loaded rows
        public DataTable LoadNextPage(string q)
        {
            isLoading = true;
            string query = q +  $" LIMIT {PageSize} OFFSET {currentOffset};";
            DataTable originalTable = helper.FetchDataTableFromSYADataBase(query);
            MapRowsToDataTable(originalTable, loadedTable);
            currentOffset += PageSize;
            isLoading = false;
            return loadedTable;
        }
        public void setCurrentOffset(int offset)
        {
            currentOffset = offset;
        }
        public void MapRowsToDataTable(DataTable source, DataTable destination)
        {
            foreach (DataRow row in source.Rows)
            {
                DataRow newRow = destination.NewRow();
                foreach (DataColumn column in source.Columns)
                {
                    if (column.ColumnName == "VCH_DATE" && row[column] != DBNull.Value)
                    {
                        if (row[column] is string dateString)
                        {
                            if (DateTime.TryParse(dateString, out DateTime parsedDate))
                            {
                                newRow[column.ColumnName] = parsedDate.ToString("dd-MM-yy");
                            }
                            else
                            {
                                newRow[column.ColumnName] = dateString;
                            }
                        }
                        else if (row[column] is DateTime dateValue)
                        {
                            newRow[column.ColumnName] = dateValue.ToString("dd-MM-yy");
                        }
                        else
                        {
                            newRow[column.ColumnName] = row[column];
                        }
                    }
                    else
                    {
                        newRow[column.ColumnName] = row[column];
                    }
                }
                destination.Rows.Add(newRow);
            }
        }
        public void DataGridView1_Scroll(object sender, ScrollEventArgs e,DataGridView dataGridView1,string query)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                int visibleRowCount = dataGridView1.DisplayedRowCount(false);
                int firstVisibleRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
                if (firstVisibleRowIndex >= 0)
                {
                    int lastVisibleRowIndex = firstVisibleRowIndex + visibleRowCount - 1;
                    if (lastVisibleRowIndex == loadedTable.Rows.Count - 1 && !isLoading)
                    {
                        LoadNextPage(query);
                    }
                }
            }
        }
    }
}
