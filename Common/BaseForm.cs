using System.Drawing;
using System.Windows.Forms;

namespace Common
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }
    }
}
