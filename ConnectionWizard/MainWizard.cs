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
            //db.InitSteps();
            List<Step> step_list = db.ReadWizardSteps();
            int i = 0;
            
            foreach (Step step in step_list)
            {
               WizardPage page = new WizardPage();

               page.Name = step.Name;
               page.Text = step.Text;
                
               wizardControl.Pages.Add(page);

               FlowLayoutPanel flpanel = new FlowLayoutPanel();

               Label labelText = new Label();
               //labelText.Location = new Point(25, 25);
               labelText.Text = step.Text;

               CheckBox box = new CheckBox();
               box.Tag = i.ToString();
               box.Text = "a";
                //box.Location = new Point(25, 45);

                flpanel.Controls.Add(labelText);
                flpanel.Controls.Add(box);

                //wizardControl.Pages[i].Controls.Add(labelText);
                //wizardControl.Pages[i].Controls.Add(box);

                wizardControl.Pages[i].Controls.Add(flpanel);

                if (i > 0)
                   wizardControl.Pages[i-1].NextPage = page;
               i++;
            }
            wizardControl.RestartPages();


            //WizardPage page = new WizardPage();
            //WizardPage lastPage = page;

            //if (step_list.Count > 0)
            //{
            //    foreach (Step step in step_list)
            //    {

            //        WizardPage nextpage = new WizardPage();
            //        page.NextPage = nextpage;
        
            //        page.Name = step.Name;

            //        Label labelText = new Label();
            //        labelText.Location = new Point(25, 25);
            //        labelText.Text = step.Text;
            //        this.Controls.Add(labelText);

            //        wizardControl.Pages.Add(page);

            //        lastPage = page;
            //        page = nextpage;
            //        break;
            //    }
            //    //lastPage.NextPage = finishPage;
            //    wizardControl.RestartPages();
            //}
        }
    }
}
