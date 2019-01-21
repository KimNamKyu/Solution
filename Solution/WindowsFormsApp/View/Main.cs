using System.Windows.Forms;
using WindowsFormsApp.Modules;

namespace WindowsFormsApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += load.GetHandler("main");
        }
    }
}
