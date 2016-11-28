﻿using System.Windows.Forms;
using CheckConnection.Model;
using System.Linq;
using System.IO;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace CheckConnection.Methods
{
    public static class WinObjMethods
    {
        public const string ConnGridName = "ConnectionsdataGridView";        
        public static int CountGridWidth(DataGridView dgv)
        {
            int width = 0;
            foreach (DataGridViewColumn column in dgv.Columns)
                if (column.Visible == true)
                    width += column.Width;
            //width += 100;
            return width/* += 20*/;
        }

        public static void ResizeGrid(ref DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count - 1; i++)
            {
                int colw = dgv.Columns[i].Width;
                dgv.Colum‌​ns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = colw;
            }
        }

        public static int GetSelectedRow(DataGridView dgv)
        {
            int selectedrow = 0;
            if (dgv.SelectedRows.Count > 0)
                selectedrow = dgv.SelectedRows[0].Index;
            return selectedrow;
        }

        public static void AddColumn(ref DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewCell cell = new DataGridViewTextBoxCell();

            var properties = typeof(Connection).GetProperties()
                                           .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                                           .Select(p => new
                                           {
                                               PropertyName = p.Name,
                                               p.GetCustomAttributes(typeof(DisplayAttribute),
                                              false).Cast<DisplayAttribute>().Single().Name
                                           });
            foreach (var propinfo in properties)
            {
                DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn()
                {
                    CellTemplate = cell,
                    Name = propinfo.PropertyName,
                    HeaderText = propinfo.Name.ToString(),
                    DataPropertyName = propinfo.PropertyName//,
                    //AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells
                };
                dgv.Columns.Add(colName);
            }
            dgv.Columns[dgv.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.Fill;
        }

        public static void AddColumnForWizard(ref DataGridView dgv)
        {
            AddColumn(ref dgv);
            dgv.Columns["DNSServer"].AutoSizeMode = DataGridViewAutoSize‌​ColumnMode.AllCells;
        }

        public static DataGridView GetConnectionGrid()
        {
            DataGridView dgv = new DataGridView();
            dgv.Name = ConnGridName;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            dgv.AutoSize = true;

            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            AddColumnForWizard(ref dgv);
            return dgv;
        }

        public static bool HasWritePermission(string dir)
        {
            bool Allow = false;
            bool Deny = false;
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            DirectorySecurity acl = null;
            try
            {
                acl = Directory.GetAccessControl(dir);
            }
            catch (System.IO.DirectoryNotFoundException)
            {                               
            }
            if (acl == null)
                return false;
            AuthorizationRuleCollection arc = acl.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (arc == null)
                return false;
            foreach (FileSystemAccessRule rule in arc)
            {
                log.Info(rule.FileSystemRights.ToString());
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;
                if (rule.AccessControlType == AccessControlType.Allow)
                    Allow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    Deny = true;
            }
            return Allow && !Deny;
        }
    }
}
