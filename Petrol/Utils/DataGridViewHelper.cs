using System.Threading;
using System.Windows.Forms;

namespace Petrol.Utils
{
    public static class DataGridViewHelper
    {
        /// <summary>
        /// Fixes the index column sorting issue by making the index column (م) non-sortable
        /// This prevents the sequential numbering from being disrupted when sorting other columns
        /// </summary>
        /// <param name="dataGridView">The DataGridView to fix</param>
        public static void FixIndexColumnSorting(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            // Find the index column (م) and make it non-sortable
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText == "م")
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    break;
                }
            }

            // Attach event handler to reorder index column after sorting
            dataGridView.ColumnHeaderMouseClick += (sender, e) =>
            {
                // Small delay to ensure sorting is complete
                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer(_ =>
                {
                    dataGridView.Invoke(new System.Action(() =>
                    {
                        ReorderIndexColumn(dataGridView);
                        timer?.Dispose();
                    }));
                }, null, 100, Timeout.Infinite);
            };
        }

        /// <summary>
        /// Reorders the index column values to maintain sequential numbering after sorting
        /// Call this method after any sorting operation
        /// </summary>
        /// <param name="dataGridView">The DataGridView to fix</param>
        public static void ReorderIndexColumn(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            // Find the index column
            DataGridViewColumn indexColumn = null;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText == "م")
                {
                    indexColumn = column;
                    break;
                }
            }

            if (indexColumn == null) return;

            // Reorder the index values to maintain sequential numbering
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!dataGridView.Rows[i].IsNewRow)
                {
                    dataGridView.Rows[i].Cells[indexColumn.Index].Value = i + 1;
                }
            }
        }
    }
} 