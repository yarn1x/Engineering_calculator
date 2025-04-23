
using Math2;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;



namespace equation_calculator
{
	public partial class F_MainWindow : Form
	{
		bool expectation = false; // Animation key (Eliminates the conflict of pressing the panel call button quickly)
		bool enable_hotkeys = true; // Gb_MainButtons & Panel_SE key (Eliminates the possibility of hotkeys in equation solving mode)
		public F_MainWindow()
		{
			InitializeComponent();
            LoadTheme();

            this.MaximumSize = new System.Drawing.Size(300, 492);
			Panel_modes.Visible = false;
			Panel_trigonometry.Visible = false;
			Panel_SE.Visible = false;
			Panel_settings.Visible = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

        #region Calculator region
        private void Btn_calculate_Click(object sender, EventArgs e)
		{
			StringToFormula stf = new StringToFormula();
			try
			{
				if (Tb_mainfileld.Text == "")
				{
					Tb_response_output.Text = "";
					return;
				}
				Tb_response_output.Text = $"{stf.Eval(Tb_mainfileld.Text)}";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void Btn_1_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "1"; }
		private void Btn_2_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "2"; }
		private void Btn_3_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "3"; }
		private void Btn_4_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "4"; }
		private void Btn_5_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "5"; }
		private void Btn_6_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "6"; }
		private void Btn_7_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "7"; }
		private void Btn_8_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "8"; }
		private void Btn_9_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "9"; }
		private void Btn_0_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "0"; }
		private void Btn_openingParenthesis_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "("; }
		private void Btn_closingParenthesis_Click(object sender, EventArgs e) { Tb_mainfileld.Text += ")"; }
		private void Btn_comma_Click(object sender, EventArgs e) { Tb_mainfileld.Text += ","; }
		private void Btn_power_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "^"; }
		private void Btn_sqrt_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "sqrt"; }
		private void Btn_e_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "2,7182818284590452"; }
		private void Btn_PI_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "3,1415926535897932"; }
		private void Btn_factorial_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "!"; }
		private void Btn_mod_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "%"; }
		private void Btn_add_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "+"; }
		private void Btn_sub_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "-"; }
		private void Btn_multi_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "*"; }
		private void Btn_divide_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "/"; }
		private void Btn_clear_Click(object sender, EventArgs e) { if (Tb_mainfileld.Text == "") Tb_response_output.Clear(); Tb_mainfileld.Clear(); }
		private void Btn_sin_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "sin"; Btn_trigonometry_Click(Btn_trigonometry, null); }
		private void Btn_cos_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "cos"; Btn_trigonometry_Click(Btn_trigonometry, null); }
		private void Btn_tan_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "tan"; Btn_trigonometry_Click(Btn_trigonometry, null); }
		private void Btn_ctg_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "ctg"; Btn_trigonometry_Click(Btn_trigonometry, null); }
		private void Btn_log_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "log"; }
        private void Btn_ln_Click(object sender, EventArgs e) { Tb_mainfileld.Text += "ln"; }
        private void Btn_trigonometry_Click(object sender, EventArgs e)
        {
            Btn_trigonometry.Text = Btn_trigonometry.Text == "Trigonometry⋁"
                ? Btn_trigonometry.Text = "Trigonometry⋀" : "Trigonometry⋁";

            Panel_trigonometry.Location = Panel_trigonometry.Location == new Point(17, 243)
                ? new Point(319, 243) : new Point(17, 243);
            if (Panel_trigonometry.Location == new Point(17, 243)) Panel_trigonometry.Visible = true;
            else Panel_trigonometry.Visible = false;

        }
        #endregion



        #region Solving equations region
       
        private void Btn_SEmode_solve_Click(object sender, EventArgs e)
        {
            double[] inputRatios = new double[7];
            try
            {
                inputRatios[0] = Tb_SEmode_x3.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_x3.Text);
                inputRatios[1] = Tb_SEmode_x2.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_x2.Text);
                inputRatios[2] = Tb_SEmode_x.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_x.Text);
                inputRatios[3] = Tb_SEmode_sin.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_sin.Text);
                inputRatios[4] = Tb_SEmode_cos.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_cos.Text);
                inputRatios[5] = Tb_SEmode_ln.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_ln.Text);
                inputRatios[6] = Tb_SEmode_free.Text == "" ? 0 : Convert.ToDouble(Tb_SEmode_free.Text);

                List<double> intervals = new List<double>();
                double step = 0.3;
                int minX = Convert.ToInt32(Tb_SEmode_left.Text);

                for (double i = minX; i <= Convert.ToDouble(Tb_SEmode_right.Text); i += step)
                {
                    if (formula(inputRatios, i - step) * formula(inputRatios, i) < 0)
                    {
                        intervals.Add(i - step);
                        intervals.Add(i);
                    }
                }
                for (int i = 1; i < intervals.Count; i += 2)
                {

                    Tb_mainfileld.Text += $"{dichotomy(inputRatios, Convert.ToDouble(Tb_SEmode_E.Text), intervals[i - 1], intervals[i])}; ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_SEmode_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вычисление корней, подстановкой в текстовые поля коэффициентов\n(оставить пустым, если не требуется)\nВыражение, в которое подставляются коэффициенты имеет общий вид:\na*x^3  +  b*x^2  +  c*x  +  d*sin(x)  +  e*cos(x)  +  f*ln(x)  +  g\n\nAccuracy - Погрешность вычисления\nScan start - Минимальное значение вычисления корня\nScan finish - Максимальное значение\n\nstart и finish являются границами оси Х.");
        }
        private void Btn_SEmode_clear_Click(object sender, EventArgs e)
        {
            Clear_Panel_SE();
        }
        private void Clear_Panel_SE()
        {
            Tb_mainfileld.Clear();
            Tb_SEmode_x3.Clear();
            Tb_SEmode_x2.Clear();
            Tb_SEmode_x.Clear();
            Tb_SEmode_sin.Clear();
            Tb_SEmode_cos.Clear();
            Tb_SEmode_ln.Clear();
            Tb_SEmode_free.Clear();
            Tb_SEmode_E.Clear();
            Tb_SEmode_left.Clear();
            Tb_SEmode_right.Clear();
        }


        static double dichotomy(double[] ratios, double E, double leftBorder, double rightBorder, bool ShowSteps = false)
        {
            double middle_x = 0;
            double delta = 1;

            while (delta > E)
            {
                double first_func = formula(ratios, leftBorder);
                double last_func = formula(ratios, rightBorder);
                middle_x = (rightBorder + leftBorder) / 2;
                double middle_func = formula(ratios, middle_x);

                if (ShowSteps)
                {
                    Console.WriteLine($"\nF({leftBorder}) = {first_func}\nF({middle_x}) = {middle_func}\nF({rightBorder}) = {last_func}");
                }

                if (middle_func == 0) break;
                if (first_func * middle_func < 0)
                {
                    delta = Abs(rightBorder - middle_x);
                    rightBorder = middle_x;
                }
                else
                {
                    delta = Abs(leftBorder - middle_x);
                    leftBorder = middle_x;
                }
            }
            return middle_x;
        }
        static double formula(double[] inputRatios, double x)
        {
            double result = 0;
            if (inputRatios[0] != 0) result += inputRatios[0] * (x * x * x);
            if (inputRatios[1] != 0) result += inputRatios[1] * (x * x);
            if (inputRatios[2] != 0) result += inputRatios[2] * x;
            if (inputRatios[3] != 0) result += inputRatios[3] * Sin(x);
            if (inputRatios[4] != 0) result += inputRatios[4] * Cos(x);
            if (inputRatios[5] != 0) result += inputRatios[5] * Log(x);
            if (inputRatios[6] != 0) result += inputRatios[6];
            return result;
        }
        #endregion



        #region Panel modes
        private async void Btn_modes_Click(object sender, EventArgs e)
        {

            Panel_modes.Visible = true;
            if (Panel_modes.Location.X > 19)
            {

                while (!expectation && Panel_modes.Location.X > 19)
                {
                    expectation = true;
                    int move = Panel_modes.Location.X / 8;
                    await Task.Delay(1);
                    Panel_modes.Location = new Point(Panel_modes.Location.X - move, 61);
                    expectation = false;
                }
            }
            else
            {
                while (!expectation && Panel_modes.Location.X < 305)
                {
                    expectation = true;
                    int move = (316 - Panel_modes.Location.X) / 6;
                    await Task.Delay(1);
                    Panel_modes.Location = new Point(Panel_modes.Location.X + move, 61);
                    expectation = false;
                }
            }
        }
        private void Btn_mode_Calculator_Click(object sender, EventArgs e)
        {
            lbl_modeDisplay.Text = Btn_mode_Calculator.Text;
            if (Panel_settings.Location.Y <= 59) Btn_settings_Click(Btn_settings, null);

            enable_hotkeys = true;

            Gb_MainButtons.Enabled = true;
            Gb_MainButtons.Visible = true;

            Clear_Panel_SE();
            Panel_SE.Visible = false;
            Panel_SE.Location = new Point(412, 210);
            
            Btn_modes_Click(Btn_modes, null);
        }
        private void Btn_mode_solvingEquations_Click(object sender, EventArgs e)
        {
            lbl_modeDisplay.Text = Btn_mode_solvingEquations.Text;
            if (Panel_settings.Location.Y <= 59) Btn_settings_Click(Btn_settings, null);

            Panel_SE.Visible = true;
            Tb_SEmode_E.Text = "0,001";
            Tb_SEmode_left.Text = "-10";
            Tb_SEmode_right.Text = "10";

            enable_hotkeys = false;
            Gb_MainButtons.Visible = false;

            Panel_SE.Location = new Point(12, 210);
            Btn_modes_Click(Btn_modes, null);
        }
        private async void Btn_settings_Click(object sender, EventArgs e)
        {
            lbl_modeDisplay.Text = Btn_settings.Text;
            Panel_SE.Visible = false;
            Panel_settings.Visible = true;
            if (Panel_settings.Location.Y > 59)
            {
                while (!expectation && Panel_settings.Location.Y > 59)
                {
                    expectation = true;
                    int move = Panel_settings.Location.Y / 10;
                    await Task.Delay(3);
                    Panel_settings.Location = new Point(8, Panel_settings.Location.Y - move);
                    expectation = false;
                }
            }
            
            else
            {
                while (!expectation && Panel_settings.Location.Y < 500)
                {
                    expectation = true;
                    int move = (526 - Panel_settings.Location.Y) / 10;
                    await Task.Delay(2);
                    Panel_settings.Location = new Point(8, Panel_settings.Location.Y + move);
                    expectation = false;
                }
            }
            Btn_modes_Click(Btn_modes, null);
        }
        #endregion


        
        private void Tb_mainfield_GotFocus(object sender, EventArgs e)
		{
			enable_hotkeys = !enable_hotkeys;
        }//if cursor on Tb_mainfield then disable hotkeys

        //HOTKEYS
        private void F_MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (enable_hotkeys && Ts_settings_hotkeys.Checked)
			{
				//Numbers
				if (e.KeyValue == (char)Keys.D1) Btn_1_Click(Btn_1, null);
				else if (e.KeyValue == (char)Keys.D2) Btn_2_Click(Btn_2, null);
				else if (e.KeyValue == (char)Keys.D3) Btn_3_Click(Btn_3, null);
				else if (e.KeyValue == (char)Keys.D4) Btn_4_Click(Btn_4, null);
				else if (e.KeyValue == (char)Keys.D5) Btn_5_Click(Btn_5, null);
				else if (e.KeyValue == (char)Keys.D6) Btn_6_Click(Btn_6, null);
				else if (e.KeyValue == (char)Keys.D7) Btn_7_Click(Btn_7, null);
				else if (e.KeyValue == (char)Keys.D8) Btn_8_Click(Btn_8, null);
				else if (e.KeyValue == (char)Keys.D9) Btn_9_Click(Btn_9, null);
				else if (e.KeyValue == (char)Keys.D0) Btn_0_Click(Btn_0, null);

				// Operators and Special Characters
				else if (e.KeyValue == (char)Keys.Add || e.KeyValue == (char)Keys.Oemplus && e.Shift) Tb_mainfileld.Text += "+";
				else if (e.KeyValue == (char)Keys.Subtract || e.KeyValue == (char)Keys.OemMinus && e.Shift) Tb_mainfileld.Text += "-";
				else if (e.KeyValue == (int)Keys.Multiply || e.KeyValue == (int)Keys.OemPeriod && e.Shift) Btn_multi_Click(Btn_multi, null);
				else if (e.KeyValue == (int)Keys.Divide || e.KeyValue == (int)Keys.Oem2) Btn_divide_Click(Btn_divide, null);
				else if (e.KeyValue == (int)Keys.Decimal || e.KeyValue == (int)Keys.Oemcomma) Tb_mainfileld.Text += ",";

				//Other hotkeys
				else if (e.KeyValue == (char)Keys.C) Tb_mainfileld.Clear();
				else if (e.KeyValue == (char)Keys.Enter) Btn_calculate_Click(Btn_calculate, null);
				else if (e.KeyValue == (int)Keys.Back)
				{
					if (Tb_mainfileld.Text.Length > 0)
					{
						Tb_mainfileld.Text = Tb_mainfileld.Text.Substring(0, Tb_mainfileld.Text.Length - 1);
					}
				}
			}
		}



        #region Set theme & theme memory

        private void Ts_settings_darkmode_CheckedChanged(object sender, EventArgs e)
        {
            if (!Ts_settings_darkmode.Checked)
            {
                Enable_Light_theme();
                SaveTheme("Light");
            }
            else
            {
                Enable_Dark_theme();
                SaveTheme("Dark");
            }
            
        }
        private void LoadTheme()
        {
            string theme = Properties.Settings.Default.AppTheme; // Get the theme string

            if (theme == "Dark")
            {
                Enable_Dark_theme();
            }
            else
            {
                Ts_settings_darkmode.Checked = false;
                Enable_Light_theme();
            }
        }
        private void SaveTheme(string theme)
        {
            Properties.Settings.Default.AppTheme = theme; // Set the theme string
            Properties.Settings.Default.Save(); // Save the settings
        }
        private void Enable_Light_theme()
        {
            this.BackColor = SystemColors.Control;
            this.Tb_mainfileld.BackColor = SystemColors.ScrollBar;
            this.Tb_response_output.BackColor = SystemColors.Control;
            //Calculator back
            this.Btn_0.BackColor = SystemColors.ScrollBar;
            this.Btn_1.BackColor = SystemColors.ScrollBar;
            this.Btn_2.BackColor = SystemColors.ScrollBar;
            this.Btn_3.BackColor = SystemColors.ScrollBar;
            this.Btn_4.BackColor = SystemColors.ScrollBar;
            this.Btn_5.BackColor = SystemColors.ScrollBar;
            this.Btn_6.BackColor = SystemColors.ScrollBar;
            this.Btn_7.BackColor = SystemColors.ScrollBar;
            this.Btn_8.BackColor = SystemColors.ScrollBar;
            this.Btn_9.BackColor = SystemColors.ScrollBar;
            this.Btn_sin.BackColor = SystemColors.ScrollBar;
            this.Btn_cos.BackColor = SystemColors.ScrollBar;
            this.Btn_tan.BackColor = SystemColors.ScrollBar;
            this.Btn_ctg.BackColor = SystemColors.ScrollBar;
            this.Btn_PI.BackColor = SystemColors.ScrollBar;
            this.Btn_e.BackColor = SystemColors.ScrollBar;
            this.Btn_sqrt.BackColor = SystemColors.ScrollBar;
            this.Btn_ln.BackColor = SystemColors.ScrollBar;
            this.Btn_log.BackColor = SystemColors.ScrollBar;
            this.Btn_clear.BackColor = SystemColors.ScrollBar;
            this.Btn_add.BackColor = SystemColors.ScrollBar;
            this.Btn_sub.BackColor = SystemColors.ScrollBar;
            this.Btn_multi.BackColor = SystemColors.ScrollBar;
            this.Btn_divide.BackColor = SystemColors.ScrollBar;
            this.Btn_comma.BackColor = SystemColors.ScrollBar;
            this.Btn_openingParenthesis.BackColor = SystemColors.ScrollBar;
            this.Btn_closingParenthesis.BackColor = SystemColors.ScrollBar;
            this.Btn_factorial.BackColor = SystemColors.ScrollBar;
            this.Btn_power.BackColor = SystemColors.ScrollBar;
            this.Btn_mod.BackColor = SystemColors.ScrollBar;
            this.Btn_trigonometry.BackColor = SystemColors.ScrollBar;
            //Solving equations back
            this.Btn_SEmode_clear.BackColor = SystemColors.ScrollBar;
            this.Btn_SEmode_help.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_x3.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_x2.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_x.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_sin.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_cos.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_ln.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_free.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_E.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_left.BackColor = SystemColors.ScrollBar;
            this.Tb_SEmode_right.BackColor = SystemColors.ScrollBar;
            //Mode panel back
            this.Btn_modes.BackColor = SystemColors.Control;
            this.Panel_modes.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_mode_Calculator.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_mode_solvingEquations.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_settings.BackColor = Color.FromArgb(222, 222, 222);



            //FORECOLOR


            this.ForeColor = SystemColors.ControlText;
            this.Tb_mainfileld.ForeColor = SystemColors.ControlText;
            this.Tb_response_output.ForeColor = SystemColors.ControlText;
            this.Btn_modes.ForeColor = SystemColors.ControlText;
            //Calculator fore
            this.Btn_0.ForeColor = SystemColors.ControlText;
            this.Btn_1.ForeColor = SystemColors.ControlText;
            this.Btn_2.ForeColor = SystemColors.ControlText;
            this.Btn_3.ForeColor = SystemColors.ControlText;
            this.Btn_4.ForeColor = SystemColors.ControlText;
            this.Btn_5.ForeColor = SystemColors.ControlText;
            this.Btn_6.ForeColor = SystemColors.ControlText;
            this.Btn_7.ForeColor = SystemColors.ControlText;
            this.Btn_8.ForeColor = SystemColors.ControlText;
            this.Btn_9.ForeColor = SystemColors.ControlText;
            this.Btn_sin.ForeColor = SystemColors.ControlText;
            this.Btn_cos.ForeColor = SystemColors.ControlText;
            this.Btn_tan.ForeColor = SystemColors.ControlText;
            this.Btn_ctg.ForeColor = SystemColors.ControlText;
            this.Btn_PI.ForeColor = SystemColors.ControlText;
            this.Btn_e.ForeColor = SystemColors.ControlText;
            this.Btn_sqrt.ForeColor = SystemColors.ControlText;
            this.Btn_ln.ForeColor = SystemColors.ControlText;
            this.Btn_log.ForeColor = SystemColors.ControlText;
            this.Btn_clear.ForeColor = SystemColors.ControlText;
            this.Btn_add.ForeColor = SystemColors.ControlText;
            this.Btn_sub.ForeColor = SystemColors.ControlText;
            this.Btn_multi.ForeColor = SystemColors.ControlText;
            this.Btn_divide.ForeColor = SystemColors.ControlText;
            this.Btn_comma.ForeColor = SystemColors.ControlText;
            this.Btn_openingParenthesis.ForeColor = SystemColors.ControlText;
            this.Btn_closingParenthesis.ForeColor = SystemColors.ControlText;
            this.Btn_factorial.ForeColor = SystemColors.ControlText;
            this.Btn_power.ForeColor = SystemColors.ControlText;
            this.Btn_mod.ForeColor = SystemColors.ControlText;
            this.Btn_trigonometry.ForeColor = SystemColors.ControlText;
            //Solving equations fore
            this.Btn_SEmode_clear.ForeColor = SystemColors.ControlText;
            this.Btn_SEmode_help.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_x3.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_x2.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_x.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_sin.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_cos.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_ln.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_free.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_E.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_left.ForeColor = SystemColors.ControlText;
            this.Tb_SEmode_right.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_row1.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_row2.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_Accuracy.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_freenum.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_Scanstart.ForeColor = SystemColors.ControlText;
            this.lbl_SEmode_Scanfinish.ForeColor = SystemColors.ControlText;
            //Settings panel fore
            this.lbl_Info.ForeColor = SystemColors.ControlText;
            this.lbl_Info2.ForeColor = SystemColors.ControlText;
            this.lbl_Hotkeys.ForeColor = SystemColors.ControlText;
            this.lbl_DarkMode.ForeColor = SystemColors.ControlText;
            //Mode panel fore
            this.lbl_modeDisplay.ForeColor = SystemColors.ControlText;
            this.Panel_modes.ForeColor = SystemColors.ControlText;
            this.Btn_mode_Calculator.ForeColor = SystemColors.ControlText;
            this.Btn_mode_solvingEquations.ForeColor = SystemColors.ControlText;
            this.Btn_settings.ForeColor = SystemColors.ControlText;
            this.Ib_calculator.Image = Properties.Resources.calculator_ico_black;
            this.Ib_SE.Image = Properties.Resources.SEmode_ico_black;
            this.Ib_settings.Image = Properties.Resources.settings_ico_black;
        }
        private void Enable_Dark_theme()
        {
            this.BackColor = Color.FromArgb(50, 50, 53);
            this.Tb_mainfileld.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_response_output.BackColor = Color.FromArgb(50, 50, 53);
            //Calculator back
            this.Btn_0.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_1.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_2.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_3.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_4.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_5.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_6.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_7.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_8.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_9.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_sin.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_cos.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_tan.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_ctg.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_PI.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_e.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_sqrt.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_ln.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_log.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_clear.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_add.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_sub.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_multi.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_divide.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_comma.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_openingParenthesis.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_closingParenthesis.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_factorial.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_power.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_mod.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_trigonometry.BackColor = Color.FromArgb(64, 64, 64);
            //Solving equations back
            this.Btn_SEmode_clear.BackColor = Color.FromArgb(84, 84, 84);
            this.Btn_SEmode_help.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_x3.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_x2.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_x.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_sin.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_cos.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_ln.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_free.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_E.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_left.BackColor = Color.FromArgb(84, 84, 84);
            this.Tb_SEmode_right.BackColor = Color.FromArgb(84, 84, 84);
            //Mode panel back
            this.Btn_modes.BackColor = Color.FromArgb(50, 50, 53);
            this.Panel_modes.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_mode_Calculator.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_mode_solvingEquations.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_settings.BackColor = Color.FromArgb(60, 60, 63);



            //FORECOLOR


            this.ForeColor = SystemColors.ButtonFace;
            this.Tb_mainfileld.ForeColor = SystemColors.ButtonFace;
            this.Tb_response_output.ForeColor = SystemColors.ButtonFace;
            this.Btn_modes.ForeColor = SystemColors.ButtonFace;
            //Calculator fore
            this.Btn_0.ForeColor = SystemColors.ButtonFace;
            this.Btn_1.ForeColor = SystemColors.ButtonFace;
            this.Btn_2.ForeColor = SystemColors.ButtonFace;
            this.Btn_3.ForeColor = SystemColors.ButtonFace;
            this.Btn_4.ForeColor = SystemColors.ButtonFace;
            this.Btn_5.ForeColor = SystemColors.ButtonFace;
            this.Btn_6.ForeColor = SystemColors.ButtonFace;
            this.Btn_7.ForeColor = SystemColors.ButtonFace;
            this.Btn_8.ForeColor = SystemColors.ButtonFace;
            this.Btn_9.ForeColor = SystemColors.ButtonFace;
            this.Btn_sin.ForeColor = SystemColors.ButtonFace;
            this.Btn_cos.ForeColor = SystemColors.ButtonFace;
            this.Btn_tan.ForeColor = SystemColors.ButtonFace;
            this.Btn_ctg.ForeColor = SystemColors.ButtonFace;
            this.Btn_PI.ForeColor = SystemColors.ButtonFace;
            this.Btn_e.ForeColor = SystemColors.ButtonFace;
            this.Btn_sqrt.ForeColor = SystemColors.ButtonFace;
            this.Btn_ln.ForeColor = SystemColors.ButtonFace;
            this.Btn_log.ForeColor = SystemColors.ButtonFace;
            this.Btn_clear.ForeColor = SystemColors.ButtonFace;
            this.Btn_add.ForeColor = SystemColors.ButtonFace;
            this.Btn_sub.ForeColor = SystemColors.ButtonFace;
            this.Btn_multi.ForeColor = SystemColors.ButtonFace;
            this.Btn_divide.ForeColor = SystemColors.ButtonFace;
            this.Btn_comma.ForeColor = SystemColors.ButtonFace;
            this.Btn_openingParenthesis.ForeColor = SystemColors.ButtonFace;
            this.Btn_closingParenthesis.ForeColor = SystemColors.ButtonFace;
            this.Btn_factorial.ForeColor = SystemColors.ButtonFace;
            this.Btn_power.ForeColor = SystemColors.ButtonFace;
            this.Btn_mod.ForeColor = SystemColors.ButtonFace;
            this.Btn_trigonometry.ForeColor = SystemColors.ButtonFace;
            //Solving equations fore
            this.Btn_SEmode_clear.ForeColor = SystemColors.ButtonFace;
            this.Btn_SEmode_help.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_x3.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_x2.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_x.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_sin.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_cos.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_ln.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_free.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_E.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_left.ForeColor = SystemColors.ButtonFace;
            this.Tb_SEmode_right.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_row1.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_row2.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_Accuracy.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_freenum.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_Scanstart.ForeColor = SystemColors.ButtonFace;
            this.lbl_SEmode_Scanfinish.ForeColor = SystemColors.ButtonFace;
            //Settings panel fore
            this.lbl_Info.ForeColor = SystemColors.ButtonFace;
            this.lbl_Info2.ForeColor = SystemColors.ButtonFace;
            this.lbl_Hotkeys.ForeColor = SystemColors.ButtonFace;
            this.lbl_DarkMode.ForeColor = SystemColors.ButtonFace;
            //Mode panel fore
            this.lbl_modeDisplay.ForeColor = SystemColors.ButtonFace;
            this.Panel_modes.ForeColor = SystemColors.ButtonFace;
            this.Btn_mode_Calculator.ForeColor = SystemColors.ButtonFace;
            this.Btn_mode_solvingEquations.ForeColor = SystemColors.ButtonFace;
            this.Btn_settings.ForeColor = SystemColors.ButtonFace;
            this.Ib_calculator.Image = Properties.Resources.calculator_ico;
            this.Ib_SE.Image = Properties.Resources.SEmode_ico;
            this.Ib_settings.Image = Properties.Resources.settings_ico;
        }

        #endregion

    }
}
