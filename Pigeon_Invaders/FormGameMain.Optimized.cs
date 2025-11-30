using System.Windows.Forms;

namespace Pigeon_Invaders
{
    public partial class FormGameMain : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
