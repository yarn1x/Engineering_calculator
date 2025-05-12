using Math2;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
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
            LoadLanguage();

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
            if (Properties.Settings.Default.Language == "English")
            {
                MessageBox.Show("Calculating roots by substituting coefficients into text fields\n(leave blank if not required)\nThe expression in which the coefficients are substituted has the general form:\na*x^3 + b*x^2 + c*x + d*sin(x) + e*cos(x) + f*ln(x)+ g\n\nAccuracy - Calculation error rate\nScan start - Minimum value of root calculation\nScan finish - Maximum value\n\nstart and finish are the boundaries of the X-axis.\n\n\nTranslated using Yandex Translator");
                return;
            }
            else if (Properties.Settings.Default.Language == "Русский")
            {
                MessageBox.Show("Вычисление корней, подстановкой в текстовые поля коэффициентов\n(оставить пустым, если не требуется)\nВыражение, в которое подставляются коэффициенты имеет общий вид:\na*x^3  +  b*x^2  +  c*x  +  d*sin(x)  +  e*cos(x)  +  f*ln(x)  +  g\n\nAccuracy - Погрешность вычисления\nScan start - Минимальное значение вычисления корня\nScan finish - Максимальное значение\n\nstart и finish являются границами оси Х.");
                return;
            }
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



        #region Panel modes region
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
            if (Panel_settings.Location.Y <= 59) Btn_settings_Click(Btn_settings, null);
            lbl_modeDisplay.Text = Btn_mode_Calculator.Text;
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
            if (Panel_settings.Location.Y <= 59) Btn_settings_Click(Btn_settings, null);
            lbl_modeDisplay.Text = Btn_mode_solvingEquations.Text;
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



        #region Settings region

        private void Btn_GitHubSource_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/yarn1x/Engineering_calculator");
        }

        private void Cb_settings_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_settings_language.Text == "English")
            {
                Set_Eng_language();
                SaveLanguage("English");

            }
            else if (Cb_settings_language.Text == "Русский")
            {
                Set_Rus_language();
                SaveLanguage("Русский");
            }
        }

        private void Set_Eng_language()
        {
            this.Text = "Calculator";
            lbl_modeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_mode_Calculator.Text = "Calculator";
            Btn_mode_Calculator.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_mode_solvingEquations.Text = "Solving equations";
            Btn_mode_solvingEquations.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_settings.Text = "Settings";
            Btn_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_Hotkeys.Text = "Hotkeys";
            lbl_DarkMode.Text = "Dark mode";
            lbl_Info.Text = "Info";
            lbl_Info2.Text = "Version: 0.0.2 alpha\n05.12.2025";
            Btn_GitHubSource.Text = "Source code";
            Btn_GitHubSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_trigonometry.Text = "Trigonometry";
            Btn_trigonometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_freenum.Text = "free num";
            lbl_SEmode_freenum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_Accuracy.Text = "Accuracy";
            lbl_SEmode_Accuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_Scanstart.Text = "Scan start";
            lbl_SEmode_Scanfinish.Text = "Scan finish";
            Btn_SEmode_clear.Text = "Clean";
            Btn_SEmode_help.Text = "Help";
            Btn_SEmode_solve.Text = "Solve";
        }

        private void Set_Rus_language()
        {
            this.Text = "Калькулятор";
            lbl_modeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_mode_Calculator.Text = "Калькулятор";
            Btn_mode_Calculator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_mode_solvingEquations.Text = "Вычисление корня";
            Btn_mode_solvingEquations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_settings.Text = "Настройки";
            Btn_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_Hotkeys.Text = "Горячие клавиши";
            lbl_DarkMode.Text = "Темная тема";
            lbl_Info.Text = "Информация";
            lbl_Info2.Text = "Версия: 0.0.2 alpha\n05.12.2025";
            Btn_GitHubSource.Text = "Исходный код";
            Btn_GitHubSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Btn_trigonometry.Text = "Тригонометрия";
            Btn_trigonometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_freenum.Text = "свободное";
            lbl_SEmode_freenum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_Accuracy.Text = "Точность";
            lbl_SEmode_Accuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl_SEmode_Scanstart.Text = "Старт";
            lbl_SEmode_Scanfinish.Text = "Финиш";
            Btn_SEmode_clear.Text = "Отчистить";
            Btn_SEmode_help.Text = "Помощь";
            Btn_SEmode_solve.Text = "Решить";

            Cb_settings_language.Text = "Русский";
        }

        private void LoadLanguage()
        {
            string language = Properties.Settings.Default.Language;

            if (language == "English")
            {
                lbl_modeDisplay.Text = "Calculator";
                Set_Eng_language();
            }
            else if (language == "Русский")
            {
                lbl_modeDisplay.Text = "Калькулятор";
                Set_Rus_language();
            }
        }

        private void SaveLanguage(string lang)
        {
            Properties.Settings.Default.Language = lang;
            Properties.Settings.Default.Save();
        }

        #endregion



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
            this.BackColor = Color.FromArgb(252, 252, 252); ;
            this.Tb_mainfileld.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_response_output.BackColor = Color.FromArgb(252, 252, 252); ;
            //Calculator back
            this.Btn_0.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_1.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_2.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_3.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_4.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_5.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_6.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_7.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_8.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_9.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_sin.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_cos.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_tan.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_ctg.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_PI.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_e.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_sqrt.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_ln.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_log.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_clear.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_add.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_sub.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_multi.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_divide.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_comma.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_openingParenthesis.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_closingParenthesis.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_factorial.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_power.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_mod.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_trigonometry.BackColor = Color.FromArgb(200, 200, 200);
            //Solving equations back
            this.Btn_SEmode_clear.BackColor = Color.FromArgb(200, 200, 200);
            this.Btn_SEmode_help.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_x3.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_x2.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_x.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_sin.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_cos.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_ln.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_free.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_E.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_left.BackColor = Color.FromArgb(200, 200, 200);
            this.Tb_SEmode_right.BackColor = Color.FromArgb(200, 200, 200);
            //Settings panel back
            this.Btn_GitHubSource.BackColor = Color.FromArgb(252, 252, 252);
            this.Cb_settings_language.FillColor = Color.FromArgb(200, 200, 200);
            //Mode panel back
            this.Btn_modes.BackColor = Color.FromArgb(252, 252, 252); ;
            this.Panel_modes.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_mode_Calculator.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_mode_solvingEquations.BackColor = Color.FromArgb(222, 222, 222);
            this.Btn_settings.BackColor = Color.FromArgb(222, 222, 222);



            //FORECOLOR


            this.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_mainfileld.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_response_output.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_modes.ForeColor = Color.FromArgb(0, 0, 0);
            //Calculator fore
            this.Btn_0.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_1.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_2.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_3.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_4.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_5.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_6.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_7.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_8.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_9.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_sin.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_cos.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_tan.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_ctg.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_PI.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_e.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_sqrt.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_ln.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_log.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_clear.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_add.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_sub.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_multi.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_divide.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_comma.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_openingParenthesis.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_closingParenthesis.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_factorial.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_power.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_mod.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_trigonometry.ForeColor = Color.FromArgb(0, 0, 0);
            //Solving equations fore
            this.Btn_SEmode_clear.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_SEmode_help.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_x3.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_x2.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_x.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_sin.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_cos.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_ln.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_free.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_E.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_left.ForeColor = Color.FromArgb(0, 0, 0);
            this.Tb_SEmode_right.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_row1.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_row2.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_Accuracy.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_freenum.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_Scanstart.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_SEmode_Scanfinish.ForeColor = Color.FromArgb(0, 0, 0);
            //Settings panel fore
            this.lbl_Info.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_Info2.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_Hotkeys.ForeColor = Color.FromArgb(0, 0, 0);
            this.lbl_DarkMode.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_GitHubSource.ForeColor = Color.FromArgb(0, 0, 0);
            this.Cb_settings_language.ForeColor = Color.FromArgb(0, 0, 0);
            //Mode panel fore
            this.lbl_modeDisplay.ForeColor = Color.FromArgb(0, 0, 0);
            this.Panel_modes.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_mode_Calculator.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_mode_solvingEquations.ForeColor = Color.FromArgb(0, 0, 0);
            this.Btn_settings.ForeColor = Color.FromArgb(0, 0, 0);
            //change images
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
            //Settings panel back
            this.Btn_GitHubSource.BackColor = Color.FromArgb(50, 50, 53);
            this.Cb_settings_language.FillColor = Color.FromArgb(84, 84, 84);
            //Mode panel back
            this.Btn_modes.BackColor = Color.FromArgb(50, 50, 53);
            this.Panel_modes.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_mode_Calculator.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_mode_solvingEquations.BackColor = Color.FromArgb(60, 60, 63);
            this.Btn_settings.BackColor = Color.FromArgb(60, 60, 63);


            //FORECOLOR


            this.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_mainfileld.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_response_output.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_modes.ForeColor = Color.FromArgb(240, 240, 240);
            //Calculator fore
            this.Btn_0.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_1.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_2.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_3.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_4.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_5.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_6.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_7.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_8.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_9.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_sin.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_cos.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_tan.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_ctg.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_PI.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_e.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_sqrt.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_ln.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_log.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_clear.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_add.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_sub.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_multi.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_divide.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_comma.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_openingParenthesis.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_closingParenthesis.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_factorial.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_power.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_mod.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_trigonometry.ForeColor = Color.FromArgb(240, 240, 240);
            //Solving equations fore
            this.Btn_SEmode_clear.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_SEmode_help.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_x3.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_x2.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_x.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_sin.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_cos.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_ln.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_free.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_E.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_left.ForeColor = Color.FromArgb(240, 240, 240);
            this.Tb_SEmode_right.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_row1.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_row2.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_Accuracy.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_freenum.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_Scanstart.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_SEmode_Scanfinish.ForeColor = Color.FromArgb(240, 240, 240);
            //Settings panel fore
            this.lbl_Info.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_Info2.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_Hotkeys.ForeColor = Color.FromArgb(240, 240, 240);
            this.lbl_DarkMode.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_GitHubSource.ForeColor = Color.FromArgb(240, 240, 240);
            this.Cb_settings_language.ForeColor = Color.FromArgb(240, 240, 240);
            //Mode panel fore
            this.lbl_modeDisplay.ForeColor = Color.FromArgb(240, 240, 240);
            this.Panel_modes.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_mode_Calculator.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_mode_solvingEquations.ForeColor = Color.FromArgb(240, 240, 240);
            this.Btn_settings.ForeColor = Color.FromArgb(240, 240, 240);
            //change images
            this.Ib_calculator.Image = Properties.Resources.calculator_ico;
            this.Ib_SE.Image = Properties.Resources.SEmode_ico;
            this.Ib_settings.Image = Properties.Resources.settings_ico;
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

    }
}
