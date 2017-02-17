using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Common;

namespace CheckConnection
{
    public partial class CompareConnections : BaseForm
    {
        public CompareConnections()
        {
            InitializeComponent();
        }

        private int formSize=0;

        public void NewDataTable(DataGridView dgv1, DataGridViewRow dgv2Row)
        {
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn()
            {
                Name = "CurrentConnection",
                HeaderText = "Текущее соединение",
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            resultDataGridView.Columns.Add(colName);
            colName = new DataGridViewTextBoxColumn()
            {
                Name = "HistoryConnection",
                HeaderText = "Выбранное соединение: "+dgv2Row.Cells["Date"].Value,
                AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
            };
            resultDataGridView.Columns.Add(colName);
            resultDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            resultDataGridView.RowCount = dgv1.ColumnCount - 1;
            for (int i = 0; i < resultDataGridView.RowCount; i++)
            {
                resultDataGridView.Rows[i].HeaderCell.Value = dgv1.Columns[i + 1].HeaderCell.Value;
                InputTable(dgv1.Rows[dgv1.SelectedRows[0].Index], i, 0);
                InputTable(dgv2Row, i, 1);
                if (resultDataGridView.Rows[i].Cells[1].Value != null)
                {
                    if ((resultDataGridView.Rows[i].Cells[1].Value != null )&&(resultDataGridView.Rows[i].Cells[0].Value != null))
                    {
                        if (!resultDataGridView.Rows[i].Cells[0].Value.ToString().Equals(resultDataGridView.Rows[i].Cells[1].Value.ToString()))
                        {
                            resultDataGridView.Rows[i].Cells[0].Style.BackColor = Color.LightGray;
                            resultDataGridView.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                            resultDataGridView.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            resultDataGridView.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        }
                    }
                }
            }
            ResizeDataGridAndForm();
        }

        void ResizeDataGridAndForm()
        {
            int width = 0;
            foreach (DataGridViewColumn column in resultDataGridView.Columns)
            {
                width += column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
            }
            resultDataGridView.Width = width + resultDataGridView.RowHeadersWidth+145;
            int height = 0;
            foreach (DataGridViewRow row in resultDataGridView.Rows)
            {
                height += row.Height;
            }
            resultDataGridView.Height = height + resultDataGridView.ColumnHeadersHeight+40;
            ClientSize=new Size(resultDataGridView.Width, resultDataGridView.Height);
            this.MinimumSize=new Size(0, resultDataGridView.Height+5);
            this.MaximumSize=new Size(100000000, resultDataGridView.Height+5);
            resultDataGridView.Anchor|=AnchorStyles.Bottom|AnchorStyles.Right;
            formSize = resultDataGridView.Width;
            int colWidth = resultDataGridView.Columns[1].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
            resultDataGridView.Columns[1].AutoSizeMode=DataGridViewAutoSizeColumnMode.NotSet;
            resultDataGridView.Columns[1].Width = colWidth;
            resultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            resultDataGridView.ColumnHeadersHeight = 38;
        }

        void InputTable(DataGridViewRow dgvRow, int index, int cellIndex)
        {
            if(dgvRow.Cells[index+1].Value!=null)
            if (dgvRow.Cells[index + 1].Value.ToString().Contains(';'))
            {
                string[] textString = dgvRow.Cells[index + 1].Value.ToString().Split(';');
                string resultText = "";
                foreach (var text in textString)
                {
                    resultText += text.Trim() + '\n';
                }
                resultDataGridView.Rows[index].Cells[cellIndex].Value = resultText.TrimEnd();
                resultDataGridView.Rows[index].DefaultCellStyle.WrapMode=DataGridViewTriState.True;
                resultDataGridView.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            }
            else
            {
                resultDataGridView.Rows[index].Cells[cellIndex].Value = dgvRow.Cells[index + 1].Value;
            }
            resultDataGridView.Rows[index].Cells[cellIndex].ReadOnly = true;
        }

        private void resultDataGridView_SizeChanged(object sender, EventArgs e)
        {
            if (resultDataGridView.Columns.Count > 0)
            {
                if (resultDataGridView.Columns[1].AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
                {
                    resultDataGridView.Columns[1].Width += (resultDataGridView.Width - formSize);
                    formSize = resultDataGridView.Width;
                }
            }
        }
    }
}
