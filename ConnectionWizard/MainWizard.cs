using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AeroWizard;
using System.ComponentModel;
using System.Text;

using ConnectionWizard.Methods;
using ConnectionWizard.Model;

using CheckConnection.Methods;
using CheckConnection.Model;

using PingForm.Methods;

namespace ConnectionWizard
{
    public partial class MainWizard : Form
    {
        //int page_index = 0;
        const string FlowPanelName = "FlowPanel";
        private ConnectionWizard.Methods.DBInterface db;
        private WMIInterface wmi;
        private PingInterface png;
        private int forms_visit_id = 0;

        public MainWizard(ConnectionWizard.Methods.DBInterface dbparam, 
                                                  WMIInterface wmiparam,
                                                  PingInterface pngparam)
        {
            db = dbparam;
            wmi = wmiparam;
            png = pngparam;
            InitializeComponent();
        }

        private void MainWizard_Load(object sender, EventArgs e)
        {
            Forms forms = db.GetFormsById(271);
            Wizard_Init(forms);
            forms_visit_id = db.SetFormVisit(forms.Id_Form);
        }

        void wizardPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

            FlowLayoutPanel flpanel = (FlowLayoutPanel)e.Page.Controls[FlowPanelName];
            if (flpanel != null)
            {

                #region Save_results
                RadioButton rb = new RadioButton();
                foreach (Control cntrl in flpanel.Controls)
                {                   
                    if (cntrl is RadioButton) {
                        rb = (RadioButton)cntrl;

                        if (rb.Checked == true)
                        {
                            db.SetFormAnsAbo(new Form_Ans_Abo() { Id_Query = ((Form_Ans)rb.Tag).Id_Query, Id_Ans = ((Form_Ans)rb.Tag).Id_Ans, Id_Visit = forms_visit_id });
                            break;
                        }
                    }
                }
                #endregion

                #region GetNext
                Form_Query form_query = new Form_Query();
                if (rb.Tag != null)
                {
                    form_query = db.GetNextQuery(forms_visit_id, ((Form_Ans)rb.Tag).Id_Query);
                }

                if (form_query.Num_Query != 0)
                {
                    e.Page.NextPage = wizardControl.Pages.Find(delegate (WizardPage pg) { return ((Form_Query)pg.Tag).Id_Query == form_query.Id_Query; });
                    
                    AddControlByAction(form_query, e);
                }
                else
                {
                    e.Page.NextPage = wizardControl.Pages[wizardControl.Pages.Count - 1];
                }
                #endregion
            }
            else
            {
                e.Page.NextPage = wizardControl.Pages[wizardControl.Pages.Count - 1];
            }
        }

        void Wizard_Init(Forms form)
        {
            List<Form_Query> query_list = db.GetQueryTable();

            wizardControl.Pages.Capacity = query_list.Count+1;
            wizardControl.Text = form.Name;

            #region Find first question
            foreach (Form_Query query in query_list)
            {
                if (form.Id_Query_First == query.Id_Query)
                {
                    List<Form_Ans> form_answer_list = db.GetQueryAnswer(form.Id_Query_First);
                    WizardPage page = FillPage(query);
                    wizardControl.Pages.Add(page);
                    query_list.Remove(query);
                    break;
                }
            }
            #endregion

            foreach (Form_Query query in query_list)
            {
                WizardPage page = FillPage(query);
                wizardControl.Pages.Add(page);
            }

            #region Finish page            
            wizardControl.Pages[wizardControl.Pages.Count - 1].IsFinishPage = true;
            #endregion

            wizardControl.RestartPages();
        }

        FlowLayoutPanel AddAnswerToPanel(int queryid)
        {
            List<Form_Ans> form_answer_list = db.GetQueryAnswer(queryid);
            FlowLayoutPanel flpanel = new FlowLayoutPanel();

            #region Panel                
             flpanel.FlowDirection = FlowDirection.TopDown;
             flpanel.AutoSize = true;
             flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
             flpanel.WrapContents = true;
             flpanel.Dock = DockStyle.Fill;
             flpanel.Name = FlowPanelName;
            #endregion

            foreach (Form_Ans form_answer in form_answer_list)
            {
                RadioButton box = new RadioButton();
                box.Text = form_answer.Answer;
                box.AutoSize = true;
                box.Dock = System.Windows.Forms.DockStyle.Fill;
                box.Tag = form_answer;
                flpanel.Controls.Add(box);
            }
            return flpanel;
        }

        WizardPage FillPage(Form_Query query)
        {
            WizardPage page = new WizardPage();

            page.Tag = query;
            page.Text = query.Query;
            page.Commit += wizardPage_Commit;
       
            FlowLayoutPanel flpanel = AddAnswerToPanel(query.Id_Query);       
            page.Controls.Add(flpanel);            

            return page;
        }

        void AddControlByAction(Form_Query form_query, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (!String.IsNullOrEmpty(form_query.Action)) {
                if (form_query.Action.Contains("GetNetworkDevices"))
                {
                    FlowLayoutPanel nextflpanel = (FlowLayoutPanel)e.Page.NextPage.Controls[FlowPanelName];
                    AddConnGridToPanel(ref nextflpanel);
                }
                if ( form_query.Action.Contains("GetPingLocal"))
                {
                    FlowLayoutPanel nextflpanel = (FlowLayoutPanel)e.Page.NextPage.Controls[FlowPanelName];

                    WizardPage wzpage = wizardControl.Pages.Find(delegate (WizardPage pg) { return ((Form_Query)pg.Tag).Action == "GetNetworkDevices"; });
                    FlowLayoutPanel currflpanel = (FlowLayoutPanel)wzpage.Controls[FlowPanelName];
                    DataGridView dgv = (DataGridView)currflpanel.Controls[WinObjMethods.ConnGridName];
                    if (dgv != null && dgv.Rows.Count > 0) {
                        string destination = dgv.Rows[0].Cells["Ip_Address_v4"].Value.ToString();
                        if (destination != null)
                            AddPingResultToPanel(destination, ref nextflpanel);
                        else
                            destination = "localhost";
                    }
                }
                if (!String.IsNullOrEmpty(form_query.Action) && form_query.Action.Contains("GetPingGate"))
                {
                    FlowLayoutPanel nextflpanel = (FlowLayoutPanel)e.Page.NextPage.Controls[FlowPanelName];

                    WizardPage wzpage = wizardControl.Pages.Find(delegate (WizardPage pg) { return ((Form_Query)pg.Tag).Action == "GetNetworkDevices"; });
                    FlowLayoutPanel currflpanel = (FlowLayoutPanel)wzpage.Controls[FlowPanelName];
                    DataGridView dgv = (DataGridView)currflpanel.Controls[WinObjMethods.ConnGridName];
                    if (dgv != null && dgv.Rows.Count > 0)
                    {
                        string destination = dgv.Rows[0].Cells["IPGateway"].Value.ToString();
                        destination = destination.Substring(0, (destination.IndexOf(";") > 0) ? destination.IndexOf(";") : destination.Length);
                        if (destination != null)
                            AddPingResultToPanel(destination, ref nextflpanel);
                        else
                            destination = "localhost";
                    }
                }
                if (!String.IsNullOrEmpty(form_query.Action) && form_query.Action.Contains("GetTrace"))
                {

                }
            }
        }

        void AddConnGridToPanel(ref FlowLayoutPanel flpanel)
        {
            var ctrl = flpanel.Controls.Find(WinObjMethods.ConnGridName, false);
            if (ctrl.Length == 0)
            {
                DataGridView dgv = WinObjMethods.GetConnectionGrid();                
                List<Connection> connlist = wmi.GetNetworkDevices();
                if (connlist.Count > 0)
                {
                    var bindsList = new BindingList<Connection>(connlist);
                    //Bind BindingList directly to the DataGrid
                    var source = new BindingSource(bindsList, null);
                    dgv.DataSource = source;
                }
                flpanel.Controls.Add(dgv);                
            }
        }

        void AddPingResultToPanel(string destination, ref FlowLayoutPanel flpanel)
        {
            const string pingresultTB = "pingresultTextBox";
            //var ctrl = flpanel.Controls.Find(pingresultTB, false);
            //if (ctrl.Length == 0)
            //{
                Label pingresult = new Label();
                pingresult.Name = pingresultTB;
                pingresult.BorderStyle = BorderStyle.None;
                pingresult.AutoSize = true;
                pingresult.Dock = DockStyle.Left | DockStyle.Top;

                System.Net.NetworkInformation.PingReply pngreply = png.GetPing(destination);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("IP-адрес: " + pngreply.Address.ToString() );
                sb.AppendLine("Время отклика: " + (pngreply.Status == System.Net.NetworkInformation.IPStatus.Success ? pngreply.RoundtripTime.ToString() : "*" ));

                pingresult.Text = sb.ToString();

                flpanel.Controls.Add(pingresult);
            //}
        }

    }
}
