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
        public MainWizard()
        {
            InitializeComponent();
        }

        private void MainWizard_Load(object sender, EventArgs e)
        {
            DBMethods db = new DBMethods();
            int forms_id = 0;
            int box_index = 0;
            //db.InitSteps();
            //db.InitWizardDB();

            Forms forms = db.GetFormsByName(271);
            forms_id = db.SetFormVisit(forms.Id_Form);
            List<Form_Query> form_query_list = db.GetNextQuery(forms.Id_Form, forms.Id_Query_First);
            List<Form_Ans> form_answer_list = db.GetNextQueryAnswer(forms.Id_Query_First);

            WizardPage page = new WizardPage();
            page.Text = form_query_list[0].Query;
            wizardControl.Pages.Add(page);

            FlowLayoutPanel flpanel = new FlowLayoutPanel();

            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;

            wizardControl.Pages[0].Controls.Add(flpanel);

            foreach (Form_Ans form_answer in form_answer_list)
            {
                RadioButton box = new RadioButton();
                box.Text = form_answer.Answer;
                box.AutoSize = true;
                box.Dock = System.Windows.Forms.DockStyle.Fill;
                flpanel.Controls.Add(box);
          
            }
            wizardControl.RestartPages();


            #region step_test
            //List<Step> step_list = db.ReadWizardSteps();
            //int i = 0;

            //foreach (Step step in step_list)
            //{
            //   WizardPage page = new WizardPage();

            //   page.Name = step.Name;
            //   page.Text = step.Text;

            //   wizardControl.Pages.Add(page);

            //   FlowLayoutPanel flpanel = new FlowLayoutPanel();

            //   Label labelText = new Label();
            //   //labelText.Location = new Point(25, 25);
            //   labelText.Text = step.Text;

            //   CheckBox box = new CheckBox();
            //   box.Tag = i.ToString();
            //   box.Text = "a";
            //   //box.Location = new Point(25, 45);

            //   flpanel.Controls.Add(labelText);
            //   flpanel.Controls.Add(box);

            //   wizardControl.Pages[i].Controls.Add(flpanel);

            //   if (i > 0)
            //     wizardControl.Pages[i-1].NextPage = page;
            //   i++;
            //}
            //wizardControl.RestartPages();
            #endregion
        }
    }
}
