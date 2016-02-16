using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace KSPdeltaVCalc
{
    public partial class Result : Form
    {
        ResourceManager RM = new ResourceManager("KSPdeltaVCalc.Result", typeof(Result).Assembly);
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public Result(int stage_count, List<int> deltav_results)
        {
            InitializeComponent();
            int x = 0;
            foreach (int motor in deltav_results)
            {
                x++;
                deltav_list.Items.Add(String.Format("{0}. {1} {2}", x, motor, RM.GetString("ms")));
            }
            deltav_result.Text = String.Format("Δv: {0} {1}", deltav_results.Sum(), RM.GetString("ms"));
            count_of_stages.Text = String.Format(RM.GetString("stages_string"), stage_count - 1);
        }

        private void new_calc(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exit(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
