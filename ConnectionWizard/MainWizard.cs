using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AeroWizard;

using ConnectionWizard.Methods;
using ConnectionWizard.Model;

namespace ConnectionWizard
{
    public partial class MainWizard : Form
    {
        //int page_index = 0;
        const string FlowPanelName = "FlowPanel";
        private DBInterface db;
        private int forms_visit_id = 0;

        public MainWizard(DBInterface dbparam)
        {
            db = dbparam;
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
                    rb = (RadioButton)cntrl;

                    if (rb.Checked == true)
                    {
                        db.SetFormAnsAbo(new Form_Ans_Abo() { Id_Query = ((Form_Ans)rb.Tag).Id_Query, Id_Ans = ((Form_Ans)rb.Tag).Id_Ans, Id_Visit = forms_visit_id });
                        break;
                    }
                }
                #endregion

                #region GetNext
                Form_Query form_query = db.GetNextQuery(forms_visit_id, ((Form_Ans)rb.Tag).Id_Query);
                if (form_query.Num_Query != 0)
                {
                    e.Page.NextPage = wizardControl.Pages.Find(delegate (WizardPage pg) { return ((Form_Query)pg.Tag).Id_Query == form_query.Id_Query; });
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

            foreach (Form_Ans form_answer in form_answer_list)
            {
                #region Panel                
                flpanel.FlowDirection = FlowDirection.TopDown;
                flpanel.AutoSize = true;
                flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                flpanel.WrapContents = true;
                flpanel.Dock = DockStyle.Fill;
                flpanel.Name = FlowPanelName;
                #endregion

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
            page.Controls.Add(AddAnswerToPanel(query.Id_Query));

            return page;
        }
    }
}
