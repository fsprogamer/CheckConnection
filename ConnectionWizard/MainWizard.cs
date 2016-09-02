using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AeroWizard;
using System.Drawing;

using ConnectionWizard.Methods;
using ConnectionWizard.Model;

namespace ConnectionWizard
{
    public partial class MainWizard : Form
    {
        int page_index = 0;
        public MainWizard()
        {            
            InitializeComponent();
        }

        private void MainWizard_Load(object sender, EventArgs e)
        {
            DBMethods db = new DBMethods();
            //db.InitWizardDB();
            page_index = 0;
            Forms forms = db.GetFormsById(271);
            //forms_id = db.SetFormVisit(forms.Id_Form);
            Form_Query form_query = db.GetQuery(forms.Id_Query_First);
            List<Form_Ans> form_answer_list = db.GetQueryAnswer(forms.Id_Query_First);

            WizardPage page = new WizardPage();
            page.Text = form_query.Query;
            page.Commit += wizardPage_Commit;
            page.Initialize += wizardPage_Initialize;          
            wizardControl.Pages.Add(page);

            FlowLayoutPanel flpanel = new FlowLayoutPanel();

            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;

            wizardControl.SelectedPage.Controls.Add(flpanel);

            foreach (Form_Ans form_answer in form_answer_list)
            {
                RadioButton box = new RadioButton();
                box.Text = form_answer.Answer;
                box.AutoSize = true;
                box.Dock = System.Windows.Forms.DockStyle.Fill;
                flpanel.Controls.Add(box);
            }

            page = new WizardPage();            
            wizardControl.Pages.Add(page);

            wizardControl.RestartPages();                      
        }

        private void wizardControl_SelectedPageChanged(object sender, EventArgs e)
        {
            //RadioButton box = (RadioButton)wizardControl.Pages[0].Controls[0];
        }

        void wizardPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            // If the user hasn't provided a sufficiently long zip code, don't allow the commit
            DBMethods db = new DBMethods();
            int forms_visit_id = 0;
             
            Forms forms = db.GetFormsById(271);
            forms_visit_id = db.SetFormVisit(forms.Id_Form);

            Form_Ans_Abo faa = new Form_Ans_Abo() { Id_Query = 2360, Id_Ans = 10082, Id_Visit = forms_visit_id };

            db.SetFormAnsAbo(faa);

            int next_query_id = db.GetNextQuery(forms_visit_id, 2360);

            Form_Query form_query_list = db.GetQuery(next_query_id);
            List<Form_Ans> form_answer_list = db.GetQueryAnswer(next_query_id);

            page_index++;
            wizardControl.Pages[page_index].Text = form_query_list.Query;
           
            FlowLayoutPanel flpanel = new FlowLayoutPanel();
            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;

            wizardControl.Pages[page_index].Controls.Add(flpanel);

            foreach (Form_Ans form_answer in form_answer_list)
            {
                RadioButton box = new RadioButton();
                box.Text = form_answer.Answer;
                box.AutoSize = true;
                box.Dock = System.Windows.Forms.DockStyle.Fill;
                flpanel.Controls.Add(box);
            }
            
            WizardPage page = new WizardPage();
            wizardControl.Pages.Add(page);

            wizardControl.RestartPages();
        }

        private void wizardPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {

        }
    }
}
