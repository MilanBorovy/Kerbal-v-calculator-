using System;
using System.Collections.Generic;
using System.Resources;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace KSPdeltaVCalc
{
    public partial class Engines : Form
    {
        ResourceManager RM = new ResourceManager("KSPdeltaVCalc.Engines", typeof(Engines).Assembly);
        static public int stage_count = 1;
        static public List<int> deltav_results = new List<int>();
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
        public Engines()
        {
            InitializeComponent();
            stage.Text = String.Format("{0} {1}", RM.GetString("stage_string"), stage_count);
        }

        private void position_changed(object sender, EventArgs e)
        {
            if (pos_planet.Checked == true)
                pos_planet_choice.Enabled = true;
            else
                pos_planet_choice.Enabled = false;
            pos_planet_choice.SelectedIndex = 3;
            checkall(null, null);
        }

        private void number_of_types_changed(object sender, EventArgs e)
        {
            switch ((int)eng_count_value.Value)
            {
                case 1:
                    eng_type2_field.Enabled = false;
                    eng_type3_field.Enabled = false;
                    break;
                case 2:
                    eng_type2_field.Enabled = true;
                    eng_type3_field.Enabled = false;
                    break;
                case 3:
                    eng_type2_field.Enabled = true;
                    eng_type3_field.Enabled = true;
                    break;
            }
            checkall(null, null);
        }

        private void total_mass_changed(object sender, EventArgs e)
        {
            float temp_mass_total_value;
            if (!float.TryParse(mass_total_value.Text, out temp_mass_total_value))
            {
                string temp_mass_total_string = "";
                for (int i = 0; i < mass_total_value.Text.Length - 1; i++)
                {
                    temp_mass_total_string += mass_total_value.Text.ToCharArray()[i];
                }
                mass_total_value.Text = temp_mass_total_string;
                mass_total_value.SelectionStart = mass_total_value.Text.Length;
                mass_total_value.SelectionLength = 0;
            }
            if (!float.TryParse(mass_total_value.Text, out temp_mass_total_value))
            {
                mass_fuel_value.Text = "";
                mass_oxid_value.Text = "";
            }
            else if (mass_fuel_value.Text != "" && mass_oxid_value.Text != "")
            {
                if (!(float.Parse(mass_fuel_value.Text) + float.Parse(mass_oxid_value.Text) <= float.Parse(mass_total_value.Text)))
                {
                    mass_fuel_value.Text = "";
                    mass_oxid_value.Text = "";
                }
            }
            checkall(null, null);
        }

        private void fuel_mass_changed(object sender, EventArgs e)
        {
            float temp_mass_fuel_value;
            if (!float.TryParse(mass_fuel_value.Text, out temp_mass_fuel_value) || mass_total_value.Text == "")
            {
                string temp_mass_fuel_string = "";
                for (int i = 0; i < mass_fuel_value.Text.Length - 1; i++)
                {
                    temp_mass_fuel_string += mass_fuel_value.Text.ToCharArray()[i];
                }
                mass_fuel_value.Text = temp_mass_fuel_string;
                mass_fuel_value.SelectionStart = mass_fuel_value.Text.Length;
                mass_fuel_value.SelectionLength = 0;
            }
            if (mass_fuel_value.Text != "" && mass_oxid_value.Text != "")
            {
                if (!(float.Parse(mass_fuel_value.Text) + float.Parse(mass_oxid_value.Text) < float.Parse(mass_total_value.Text)))
                {
                    string temp_mass_fuel_string = "";
                    for (int i = 0; i < mass_fuel_value.Text.Length - 1; i++)
                    {
                        temp_mass_fuel_string += mass_fuel_value.Text.ToCharArray()[i];
                    }
                    mass_fuel_value.Text = temp_mass_fuel_string;
                    mass_fuel_value.SelectionStart = mass_fuel_value.Text.Length;
                    mass_fuel_value.SelectionLength = 0;
                }
            }
            else if (mass_fuel_value.Text != "")
            {
                if (!(float.Parse(mass_fuel_value.Text) < float.Parse(mass_total_value.Text)))
                {
                    string temp_mass_fuel_string = "";
                    for (int i = 0; i < mass_fuel_value.Text.Length - 1; i++)
                    {
                        temp_mass_fuel_string += mass_fuel_value.Text.ToCharArray()[i];
                    }
                    mass_fuel_value.Text = temp_mass_fuel_string;
                    mass_fuel_value.SelectionStart = mass_fuel_value.Text.Length;
                    mass_fuel_value.SelectionLength = 0;
                }
            }
            checkall(null, null);
        }

        private void oxidizer_mass_changed(object sender, EventArgs e)
        {
            float temp_mass_oxid_value;
            if (!float.TryParse(mass_oxid_value.Text, out temp_mass_oxid_value) || mass_total_value.Text == "")
            {
                string temp_mass_oxid_string = "";
                for (int i = 0; i < mass_oxid_value.Text.Length - 1; i++)
                {
                    temp_mass_oxid_string += mass_oxid_value.Text.ToCharArray()[i];
                }
                mass_oxid_value.Text = temp_mass_oxid_string;
                mass_oxid_value.SelectionStart = mass_oxid_value.Text.Length;
                mass_oxid_value.SelectionLength = 0;
            }
            if (mass_fuel_value.Text != "" && mass_oxid_value.Text != "")
            {
                if (!(float.Parse(mass_fuel_value.Text) + float.Parse(mass_oxid_value.Text) < float.Parse(mass_total_value.Text)))
                {
                    string temp_mass_oxid_string = "";
                    for (int i = 0; i < mass_oxid_value.Text.Length - 1; i++)
                    {
                        temp_mass_oxid_string += mass_oxid_value.Text.ToCharArray()[i];
                    }
                    mass_oxid_value.Text = temp_mass_oxid_string;
                    mass_oxid_value.SelectionStart = mass_oxid_value.Text.Length;
                    mass_oxid_value.SelectionLength = 0;
                }
            }
            else if (mass_oxid_value.Text != "")
            {
                if (!(float.Parse(mass_oxid_value.Text) <= float.Parse(mass_total_value.Text)))
                {
                    string temp_mass_oxid_string = "";
                    for (int i = 0; i < mass_oxid_value.Text.Length - 1; i++)
                    {
                        temp_mass_oxid_string += mass_oxid_value.Text.ToCharArray()[i];
                    }
                    mass_oxid_value.Text = temp_mass_oxid_string;
                    mass_oxid_value.SelectionStart = mass_oxid_value.Text.Length;
                    mass_oxid_value.SelectionLength = 0;
                }
            }
            checkall(null, null);
        }

        private void atmospheric_thrust_one_changed(object sender, EventArgs e)
        {
            float temp_eng_type1_thrust_asl_value;
            if (!float.TryParse(eng_type1_thrust_asl_value.Text, out temp_eng_type1_thrust_asl_value))
            {
                string temp_eng_type1_thrust_asl_string = "";
                for (int i = 0; i < eng_type1_thrust_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type1_thrust_asl_string += eng_type1_thrust_asl_value.Text.ToCharArray()[i];
                }
                eng_type1_thrust_asl_value.Text = temp_eng_type1_thrust_asl_string;
                eng_type1_thrust_asl_value.SelectionStart = eng_type1_thrust_asl_value.Text.Length;
                eng_type1_thrust_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_thrust_one_changed(object sender, EventArgs e)
        {
            float temp_eng_type1_thrust_vac_value;
            if (!float.TryParse(eng_type1_thrust_vac_value.Text, out temp_eng_type1_thrust_vac_value))
            {
                string temp_eng_type1_thrust_vac_string = "";
                for (int i = 0; i < eng_type1_thrust_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type1_thrust_vac_string += eng_type1_thrust_vac_value.Text.ToCharArray()[i];
                }
                eng_type1_thrust_vac_value.Text = temp_eng_type1_thrust_vac_string;
                eng_type1_thrust_vac_value.SelectionStart = eng_type1_thrust_vac_value.Text.Length;
                eng_type1_thrust_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void atmospheric_specific_impulse_one_changed(object sender, EventArgs e)
        {
            float temp_eng_type1_impulse_asl_value;
            if (!float.TryParse(eng_type1_impulse_asl_value.Text, out temp_eng_type1_impulse_asl_value))
            {
                string temp_eng_type1_impulse_asl_string = "";
                for (int i = 0; i < eng_type1_impulse_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type1_impulse_asl_string += eng_type1_impulse_asl_value.Text.ToCharArray()[i];
                }
                eng_type1_impulse_asl_value.Text = temp_eng_type1_impulse_asl_string;
                eng_type1_impulse_asl_value.SelectionStart = eng_type1_impulse_asl_value.Text.Length;
                eng_type1_impulse_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_specific_impulse_one_changed(object sender, EventArgs e)
        {
            float temp_eng_type1_impulse_vac_value;
            if (!float.TryParse(eng_type1_impulse_vac_value.Text, out temp_eng_type1_impulse_vac_value))
            {
                string temp_eng_type1_impulse_vac_string = "";
                for (int i = 0; i < eng_type1_impulse_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type1_impulse_vac_string += eng_type1_impulse_vac_value.Text.ToCharArray()[i];
                }
                eng_type1_impulse_vac_value.Text = temp_eng_type1_impulse_vac_string;
                eng_type1_impulse_vac_value.SelectionStart = eng_type1_impulse_vac_value.Text.Length;
                eng_type1_impulse_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void atmospheric_thrust_two_changed(object sender, EventArgs e)
        {
            float temp_eng_type2_thrust_asl_value;
            if (!float.TryParse(eng_type2_thrust_asl_value.Text, out temp_eng_type2_thrust_asl_value))
            {
                string temp_eng_type2_thrust_asl_string = "";
                for (int i = 0; i < eng_type2_thrust_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type2_thrust_asl_string += eng_type2_thrust_asl_value.Text.ToCharArray()[i];
                }
                eng_type2_thrust_asl_value.Text = temp_eng_type2_thrust_asl_string;
                eng_type2_thrust_asl_value.SelectionStart = eng_type2_thrust_asl_value.Text.Length;
                eng_type2_thrust_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_thrust_two_changed(object sender, EventArgs e)
        {
            float temp_eng_type2_thrust_vac_value;
            if (!float.TryParse(eng_type2_thrust_vac_value.Text, out temp_eng_type2_thrust_vac_value))
            {
                string temp_eng_type2_thrust_vac_string = "";
                for (int i = 0; i < eng_type2_thrust_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type2_thrust_vac_string += eng_type2_thrust_vac_value.Text.ToCharArray()[i];
                }
                eng_type2_thrust_vac_value.Text = temp_eng_type2_thrust_vac_string;
                eng_type2_thrust_vac_value.SelectionStart = eng_type2_thrust_vac_value.Text.Length;
                eng_type2_thrust_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void atmospheric_specific_impulse_two_changed(object sender, EventArgs e)
        {
            float temp_eng_type2_impulse_asl_value;
            if (!float.TryParse(eng_type2_impulse_asl_value.Text, out temp_eng_type2_impulse_asl_value))
            {
                string temp_eng_type2_impulse_asl_string = "";
                for (int i = 0; i < eng_type2_impulse_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type2_impulse_asl_string += eng_type2_impulse_asl_value.Text.ToCharArray()[i];
                }
                eng_type2_impulse_asl_value.Text = temp_eng_type2_impulse_asl_string;
                eng_type2_impulse_asl_value.SelectionStart = eng_type2_impulse_asl_value.Text.Length;
                eng_type2_impulse_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_specific_impulse_two_changed(object sender, EventArgs e)
        {
            float temp_eng_type2_impulse_vac_value;
            if (!float.TryParse(eng_type2_impulse_vac_value.Text, out temp_eng_type2_impulse_vac_value))
            {
                string temp_eng_type2_impulse_vac_string = "";
                for (int i = 0; i < eng_type2_impulse_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type2_impulse_vac_string += eng_type2_impulse_vac_value.Text.ToCharArray()[i];
                }
                eng_type2_impulse_vac_value.Text = temp_eng_type2_impulse_vac_string;
                eng_type2_impulse_vac_value.SelectionStart = eng_type2_impulse_vac_value.Text.Length;
                eng_type2_impulse_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void atmospheric_thrust_three_changed(object sender, EventArgs e)
        {
            float temp_eng_type3_thrust_asl_value;
            if (!float.TryParse(eng_type3_thrust_asl_value.Text, out temp_eng_type3_thrust_asl_value))
            {
                string temp_eng_type3_thrust_asl_string = "";
                for (int i = 0; i < eng_type3_thrust_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type3_thrust_asl_string += eng_type3_thrust_asl_value.Text.ToCharArray()[i];
                }
                eng_type3_thrust_asl_value.Text = temp_eng_type3_thrust_asl_string;
                eng_type3_thrust_asl_value.SelectionStart = eng_type3_thrust_asl_value.Text.Length;
                eng_type3_thrust_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_thrust_three_changed(object sender, EventArgs e)
        {
            float temp_eng_type3_thrust_vac_value;
            if (!float.TryParse(eng_type3_thrust_vac_value.Text, out temp_eng_type3_thrust_vac_value))
            {
                string temp_eng_type3_thrust_vac_string = "";
                for (int i = 0; i < eng_type3_thrust_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type3_thrust_vac_string += eng_type3_thrust_vac_value.Text.ToCharArray()[i];
                }
                eng_type3_thrust_vac_value.Text = temp_eng_type3_thrust_vac_string;
                eng_type3_thrust_vac_value.SelectionStart = eng_type3_thrust_vac_value.Text.Length;
                eng_type3_thrust_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void atmospheric_specific_impulse_three_changed(object sender, EventArgs e)
        {
            float temp_eng_type3_impulse_asl_value;
            if (!float.TryParse(eng_type3_impulse_asl_value.Text, out temp_eng_type3_impulse_asl_value))
            {
                string temp_eng_type3_impulse_asl_string = "";
                for (int i = 0; i < eng_type3_impulse_asl_value.Text.Length - 1; i++)
                {
                    temp_eng_type3_impulse_asl_string += eng_type3_impulse_asl_value.Text.ToCharArray()[i];
                }
                eng_type3_impulse_asl_value.Text = temp_eng_type3_impulse_asl_string;
                eng_type3_impulse_asl_value.SelectionStart = eng_type3_impulse_asl_value.Text.Length;
                eng_type3_impulse_asl_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void vacuum_specific_impulse_three_changed(object sender, EventArgs e)
        {
            float temp_eng_type3_impulse_vac_value;
            if (!float.TryParse(eng_type3_impulse_vac_value.Text, out temp_eng_type3_impulse_vac_value))
            {
                string temp_eng_type3_impulse_vac_string = "";
                for (int i = 0; i < eng_type3_impulse_vac_value.Text.Length - 1; i++)
                {
                    temp_eng_type3_impulse_vac_string += eng_type3_impulse_vac_value.Text.ToCharArray()[i];
                }
                eng_type3_impulse_vac_value.Text = temp_eng_type3_impulse_vac_string;
                eng_type3_impulse_vac_value.SelectionStart = eng_type3_impulse_vac_value.Text.Length;
                eng_type3_impulse_vac_value.SelectionLength = 0;
            }
            checkall(null, null);
        }

        private void eng_type3_field_toggle(object sender, EventArgs e)
        {
            eng_type3_thrust_asl_value.Text = "";
            eng_type3_thrust_vac_value.Text = "";
            eng_type3_impulse_asl_value.Text = "";
            eng_type3_impulse_vac_value.Text = "";
            eng_type3_count_value.Value = 1;
        }

        private void eng_type2_field_toggle(object sender, EventArgs e)
        {
            eng_type2_thrust_asl_value.Text = "";
            eng_type2_thrust_vac_value.Text = "";
            eng_type2_impulse_asl_value.Text = "";
            eng_type2_impulse_vac_value.Text = "";
            eng_type2_count_value.Value = 1;
        }

        private void checkall(object sender, EventArgs e)
        {
            float eng_type1_thrust = 0, eng_type2_thrust = 0, eng_type3_thrust = 0, eng_type1_impulse = 0, eng_type2_impulse = 0, eng_type3_impulse = 0, grav_acceleration = 0;
            if (mass_total_value.Text != "" &&
                mass_fuel_value.Text != "" &&
                mass_oxid_value.Text != "" &&
                eng_type1_thrust_asl_value.Text != "" &&
                eng_type1_thrust_vac_value.Text != "" &&
                eng_type1_impulse_asl_value.Text != "" &&
                eng_type1_impulse_vac_value.Text != "")
            {
                if (pos_space.Checked)
                {
                    eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text);
                    eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text);
                }
                else
                {
                    switch (pos_planet_choice.SelectedIndex)
                    {
                        case 1:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text) - (float)5 * (float.Parse(eng_type1_impulse_vac_value.Text) - float.Parse(eng_type1_impulse_asl_value.Text));
                            eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text) - (float)5 * (float.Parse(eng_type1_thrust_vac_value.Text) - float.Parse(eng_type1_thrust_asl_value.Text));
                            break;
                        case 3:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_asl_value.Text);
                            eng_type1_thrust = float.Parse(eng_type1_thrust_asl_value.Text);
                            break;
                        case 6:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type1_impulse_vac_value.Text) - float.Parse(eng_type1_impulse_asl_value.Text));
                            eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type1_thrust_vac_value.Text) - float.Parse(eng_type1_thrust_asl_value.Text));
                            break;
                        case 9:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text) - (float)15 * (float.Parse(eng_type1_impulse_vac_value.Text) - float.Parse(eng_type1_impulse_asl_value.Text));
                            eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text) - (float)15 * (float.Parse(eng_type1_thrust_vac_value.Text) - float.Parse(eng_type1_thrust_asl_value.Text));
                            break;
                        case 10:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text) - (float)0.6 * (float.Parse(eng_type1_impulse_vac_value.Text) - float.Parse(eng_type1_impulse_asl_value.Text));
                            eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text) - (float)0.6 * (float.Parse(eng_type1_thrust_vac_value.Text) - float.Parse(eng_type1_thrust_asl_value.Text));
                            break;
                        default:
                            eng_type1_impulse = float.Parse(eng_type1_impulse_vac_value.Text);
                            eng_type1_thrust = float.Parse(eng_type1_thrust_vac_value.Text);
                            break;
                    }
                }
                if (eng_count_value.Value > 1 &&
                    (eng_type2_thrust_asl_value.Text != "" &&
                    eng_type2_thrust_vac_value.Text != "" &&
                    eng_type2_impulse_asl_value.Text != "" &&
                    eng_type2_impulse_vac_value.Text != ""))
                {
                    if (pos_space.Checked)
                    {
                        eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text);
                        eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text);
                    }
                    else
                    {
                        switch (pos_planet_choice.SelectedIndex)
                        {
                            case 1:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text) - (float)5 * (float.Parse(eng_type2_impulse_vac_value.Text) - float.Parse(eng_type2_impulse_asl_value.Text));
                                eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text) - (float)5 * (float.Parse(eng_type2_thrust_vac_value.Text) - float.Parse(eng_type2_thrust_asl_value.Text));
                                break;
                            case 3:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_asl_value.Text);
                                eng_type2_thrust = float.Parse(eng_type2_thrust_asl_value.Text);
                                break;
                            case 6:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type2_impulse_vac_value.Text) - float.Parse(eng_type2_impulse_asl_value.Text));
                                eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type2_thrust_vac_value.Text) - float.Parse(eng_type2_thrust_asl_value.Text));
                                break;
                            case 9:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text) - (float)15 * (float.Parse(eng_type2_impulse_vac_value.Text) - float.Parse(eng_type2_impulse_asl_value.Text));
                                eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text) - (float)15 * (float.Parse(eng_type2_thrust_vac_value.Text) - float.Parse(eng_type2_thrust_asl_value.Text));
                                break;
                            case 10:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text) - (float)0.6 * (float.Parse(eng_type2_impulse_vac_value.Text) - float.Parse(eng_type2_impulse_asl_value.Text));
                                eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text) - (float)0.6 * (float.Parse(eng_type2_thrust_vac_value.Text) - float.Parse(eng_type2_thrust_asl_value.Text));
                                break;
                            default:
                                eng_type2_impulse = float.Parse(eng_type2_impulse_vac_value.Text);
                                eng_type2_thrust = float.Parse(eng_type2_thrust_vac_value.Text);
                                break;
                        }
                    }
                    if (eng_count_value.Value == 3 &&
                        (eng_type3_thrust_asl_value.Text != "" &&
                        eng_type3_thrust_vac_value.Text != "" &&
                        eng_type3_impulse_asl_value.Text != "" &&
                        eng_type3_impulse_vac_value.Text != ""))
                    {
                        if (pos_space.Checked)
                        {
                            eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text);
                            eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text);
                        }
                        else
                        {
                            switch (pos_planet_choice.SelectedIndex)
                            {
                                case 1:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text) - (float)5 * (float.Parse(eng_type3_impulse_vac_value.Text) - float.Parse(eng_type3_impulse_asl_value.Text));
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text) - (float)5 * (float.Parse(eng_type3_thrust_vac_value.Text) - float.Parse(eng_type3_thrust_asl_value.Text));
                                    break;
                                case 3:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_asl_value.Text);
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_asl_value.Text);
                                    break;
                                case 6:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type3_impulse_vac_value.Text) - float.Parse(eng_type3_impulse_asl_value.Text));
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text) - (float)0.0666667 * (float.Parse(eng_type3_thrust_vac_value.Text) - float.Parse(eng_type3_thrust_asl_value.Text));
                                    break;
                                case 9:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text) - (float)15 * (float.Parse(eng_type3_impulse_vac_value.Text) - float.Parse(eng_type3_impulse_asl_value.Text));
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text) - (float)15 * (float.Parse(eng_type3_thrust_vac_value.Text) - float.Parse(eng_type3_thrust_asl_value.Text));
                                    break;
                                case 10:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text) - (float)0.6 * (float.Parse(eng_type3_impulse_vac_value.Text) - float.Parse(eng_type3_impulse_asl_value.Text));
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text) - (float)0.6 * (float.Parse(eng_type3_thrust_vac_value.Text) - float.Parse(eng_type3_thrust_asl_value.Text));
                                    break;
                                default:
                                    eng_type3_impulse = float.Parse(eng_type3_impulse_vac_value.Text);
                                    eng_type3_thrust = float.Parse(eng_type3_thrust_vac_value.Text);
                                    break;
                            }
                        }
                    }
                }
                if (eng_type1_impulse > 0 || eng_type2_impulse > 0 || eng_type3_impulse > 0)
                {
                    float total_thrust = (eng_type1_thrust * (float)eng_type1_count_value.Value) + (eng_type2_thrust * (float)eng_type2_count_value.Value) + (eng_type3_thrust * (float)eng_type3_count_value.Value);
                    float total_impulse = 0;
                    switch ((int)eng_count_value.Value)
                    {
                        case 1:
                            total_impulse = (float)total_thrust / ((float)eng_type1_count_value.Value * eng_type1_thrust / eng_type1_impulse);
                            break;
                        case 2:
                            total_impulse = (float)total_thrust / (((float)eng_type1_count_value.Value * eng_type1_thrust) / eng_type1_impulse + (float)eng_type2_count_value.Value * eng_type2_thrust / eng_type2_impulse);
                            break;
                        case 3:
                            total_impulse = (float)total_thrust / ((float)eng_type1_count_value.Value * eng_type1_thrust / eng_type1_impulse + (float)eng_type2_count_value.Value * eng_type2_thrust / eng_type2_impulse + (float)eng_type3_count_value.Value * eng_type3_thrust / eng_type3_impulse);
                            break;
                    }
                    float total_deltav = (float)total_impulse * (float)9.82 * (float)Math.Log(float.Parse(mass_total_value.Text) / (float.Parse(mass_total_value.Text) - (float.Parse(mass_fuel_value.Text) + float.Parse(mass_oxid_value.Text))));
                    switch (pos_planet_choice.SelectedIndex)
                    {
                        case 0:
                            grav_acceleration = (float)2.7;
                            break;
                        case 1:
                            grav_acceleration = (float)16.7;
                            break;
                        case 2:
                            grav_acceleration = (float)0.049;
                            break;
                        case 3:
                            grav_acceleration = (float)9.81;
                            break;
                        case 4:
                            grav_acceleration = (float)1.63;
                            break;
                        case 5:
                            grav_acceleration = (float)0.491;
                            break;
                        case 6:
                            grav_acceleration = (float)2.94;
                            break;
                        case 7:
                            grav_acceleration = (float)1.10;
                            break;
                        case 8:
                            grav_acceleration = (float)1.13;
                            break;
                        case 9:
                            grav_acceleration = (float)7.85;
                            break;
                        case 10:
                            grav_acceleration = (float)7.85;
                            break;
                        case 11:
                            grav_acceleration = (float)2.31;
                            break;
                        case 12:
                            grav_acceleration = (float)7.85;
                            break;
                        case 13:
                            grav_acceleration = (float)0.589;
                            break;
                        case 14:
                            grav_acceleration = (float)0.373;
                            break;
                        case 15:
                            grav_acceleration = (float)1.69;
                            break;
                    }
                    float total_twr = total_thrust / (grav_acceleration * float.Parse(mass_total_value.Text));
                    result_deltav_value.Text = String.Format("{0:F0}", total_deltav);
                    result_impulse_value.Text = String.Format("{0:F0}", total_impulse);
                    if (pos_space.Checked)
                        result_twr_value.Text = "N/A";
                    else
                        result_twr_value.Text = String.Format("{0:F3}", total_twr);
                }
                else
                {
                    result_deltav_value.Text = "";
                    result_impulse_value.Text = "";
                    result_twr_value.Text = "";
                }
            }
            else
            {
                result_deltav_value.Text = "";
                result_impulse_value.Text = "";
                result_twr_value.Text = "";
            }
        }

        private void exit(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void new_calc(object sender, EventArgs e)
        {
            deltav_results = new List<int>();
            stage_count = 1;
            stage.Text = String.Format("{0} {1}", RM.GetString("stage_string"), stage_count);
            pos_space.Checked = true;
            pos_planet_choice.SelectedIndex = 3;
            mass_total_value.Text = "";
            eng_count_value.Value = 1;
            eng_type1_thrust_asl_value.Text = "";
            eng_type1_thrust_vac_value.Text = "";
            eng_type1_impulse_asl_value.Text = "";
            eng_type1_impulse_vac_value.Text = "";
            eng_type1_count_value.Value = 1;
        }

        private void result_deltav_value_changed(object sender, EventArgs e)
        {
            if (result_deltav_value.Text != "")
            {
                but_next.Visible = true;
                but_calculate.Visible = true;
            }
            else
            {
                but_next.Visible = false;
                but_calculate.Visible = false;
            }
        }

        private void next(object sender, EventArgs e)
        {
            stage_count++;
            deltav_results.Add(int.Parse(result_deltav_value.Text));
            but_back.Visible = true;
            stage.Text = String.Format("{0} {1}", RM.GetString("stage_string"), stage_count);
            pos_space.Checked = true;
            pos_planet_choice.SelectedIndex = 3;
            mass_total_value.Text = "";
            eng_count_value.Value = 1;
            eng_type1_thrust_asl_value.Text = "";
            eng_type1_thrust_vac_value.Text = "";
            eng_type1_impulse_asl_value.Text = "";
            eng_type1_impulse_vac_value.Text = "";
            eng_type1_count_value.Value = 1;
        }

        private void back(object sender, EventArgs e)
        {
            stage_count--;
            deltav_results.RemoveAt(deltav_results.Count - 1);
            if (stage_count == 1)
                but_back.Visible = false;
            stage.Text = String.Format("STAGE {0}", stage_count);
            pos_space.Checked = true;
            pos_planet_choice.SelectedIndex = 3;
            mass_total_value.Text = "";
            eng_count_value.Value = 1;
            eng_type1_thrust_asl_value.Text = "";
            eng_type1_thrust_vac_value.Text = "";
            eng_type1_impulse_asl_value.Text = "";
            eng_type1_impulse_vac_value.Text = "";
            eng_type1_count_value.Value = 1;

        }

        private void calculate(object sender, EventArgs e)
        {
            stage_count++;
            deltav_results.Add(int.Parse(result_deltav_value.Text));
            Result res = new Result(stage_count, deltav_results);
            this.Hide();
            res.ShowDialog();
            this.Show();
            new_calc(null, null);
        }

        private void language_changed(object sender, EventArgs e)
        {
            Program.language = language_select.SelectedIndex;
            this.Close();
        }
    }
}
