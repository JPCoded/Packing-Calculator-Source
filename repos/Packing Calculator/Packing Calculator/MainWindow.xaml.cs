using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Packing_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BitmapFrame Mario = BitmapFrame.Create(Application.GetResourceStream(new Uri("Icons/Mario.ico", UriKind.RelativeOrAbsolute)).Stream);
        private readonly BitmapFrame ABC = BitmapFrame.Create(Application.GetResourceStream(new Uri("Icons/ABC.ico", UriKind.RelativeOrAbsolute)).Stream);

        public MainWindow()
        {
            InitializeComponent();
            UpdateTimes();
        }

        #region Get UPH

        public double GetUPHPri() => double.TryParse(txtUPHPri.Text, out double number) ? number : 1;

        public double GetUPHSec() => double.TryParse(txtUPHSec.Text, out double number) ? number : 1;

        #endregion Get UPH

        #region Get NOU

        public double GetNOUPri() => double.TryParse(txtNOUPri.Text, out double number) ? number : 1;

        public double GetNOUSec() => double.TryParse(txtNOUSec.Text, out double number) ? number : 1;

        #endregion Get NOU

        public double GetHWLP() => double.TryParse(txtHWLP.Text, out double number) ? number : 1;

        public double GetHWLS() => double.TryParse(txtHWLS.Text, out double number) ? number : 1;

        public double GetPackers() => double.TryParse(txtPackers.Text, out double number) ? number : 1;

        public void ClearFields()
        {
            txtHWLP.Text = "1";
            txtHWLS.Text = "1";
            txtNOUPri.Text = "1";
            txtNOUSec.Text = "1";
            txtPackers.Text = "1";
            txtUPHPri.Text = "1";
            txtUPHSec.Text = "1";
            CbB1.IsChecked = false;
            CbB2.IsChecked = false;
            CbL.IsChecked = false;
            CbRound.IsChecked = true;
            UpdateTimes();
        }

        #region Update NOU

        public void UpdateNOUPri() => txtUPHPri.Text = (GetPackers() * GetHWLP()).ToString();

        public void UpdateNOUSec() => txtUPHSec.Text = (GetPackers() * GetHWLS()).ToString();

        #endregion Update NOU

        public double GetMinutes()
        {
            double MTC = 60 * ((GetNOUPri() / GetUPHPri()) + (GetNOUSec() / GetUPHSec()));

            if (CbB1.IsChecked == false)
            {
                MTC -= 15;
            }
            if (CbB2.IsChecked == false)
            {
                MTC -= 15;
            }
            if (CbL.IsChecked == false)
            {
                MTC -= 30;
            }
            return MTC;
        }

        public void UpdateTimes()
        {
            double MTC = GetMinutes();
            double HTC = MTC / 60;

            DateTime dateTime = DateTime.Now;

            //Labels don't get initialized first so try catch required else produces error.
            try
            {
                lblMTC.Content = CbRound.IsChecked == false ? Math.Round(MTC, 2).ToString() : Math.Round(MTC, 0).ToString();
                lblHTC.Content = CbRound.IsChecked == false ? Math.Round(HTC, 2).ToString() : Math.Round(HTC, 0).ToString();
                lblTOC.Content = dateTime.AddHours(HTC).ToString();
            }
            catch { }
        }

        public double ConvertNegative(string inputNumber) => double.TryParse(inputNumber, out double number) && number < 0.0 ? number * -1.0 : number;

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            UpdateNOUPri();
            UpdateNOUSec();
            UpdateTimes();
        }

        #region Comboboxes Breaks Checked

        private void CbB1_Checked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbB2_Checked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbL_Checked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbL_Unchecked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbB2_Unchecked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbB1_Unchecked(object sender, RoutedEventArgs e) => UpdateTimes();

        #endregion Comboboxes Breaks Checked

        #region KeyUp Methods

        private void TxtPackers_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            txtPackers.Text = GetPackers().ToString();
            UpdateNOUPri();
            UpdateNOUSec();
            txtPackers.Text = ConvertNegative(txtPackers.Text).ToString();
            UpdateTimes();
        }

        private void TxtHWLP_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            txtHWLP.Text = GetHWLP().ToString();
            UpdateNOUPri();
            UpdateNOUSec();
            txtHWLP.Text = ConvertNegative(txtHWLP.Text).ToString();
            UpdateTimes();
        }

        private void TxtHWLS_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            txtHWLS.Text = GetHWLS().ToString();
            UpdateNOUPri();
            UpdateNOUSec();
            txtHWLS.Text = ConvertNegative(txtHWLS.Text).ToString();
            UpdateTimes();
        }

        private void TxtNOUPri_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            txtNOUPri.Text = GetNOUPri().ToString();
            txtNOUPri.Text = ConvertNegative(txtNOUPri.Text).ToString();
            UpdateTimes();
        }

        private void TxtNOUSec_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            txtNOUSec.Text = GetNOUSec().ToString();
            txtNOUSec.Text = ConvertNegative(txtNOUSec.Text).ToString();
            UpdateTimes();
        }

        #endregion KeyUp Methods

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        #region Combobox Round

        private void CbRound_Checked(object sender, RoutedEventArgs e) => UpdateTimes();

        private void CbRound_Unchecked(object sender, RoutedEventArgs e) => UpdateTimes();

        #endregion Combobox Round

        #region Got Focus Methods

        private void TxtNOUPri_GotFocus(object sender, RoutedEventArgs e) => txtNOUPri.SelectAll();

        private void TxtPackers_GotFocus(object sender, RoutedEventArgs e) => txtPackers.SelectAll();

        private void TxtNOUSec_GotFocus(object sender, RoutedEventArgs e) => txtNOUSec.SelectAll();

        private void TxtHWLS_GotFocus(object sender, RoutedEventArgs e) => txtHWLS.SelectAll();

        private void TxtHWLP_GotFocus(object sender, RoutedEventArgs e) => txtHWLP.SelectAll();

        #endregion Got Focus Methods

        #region GotMouseCapture Methods

        private void TxtHWLP_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e) => txtHWLP.SelectAll();

        private void TxtNOUSec_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e) => txtNOUSec.SelectAll();

        private void TxtNOUPri_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e) => txtNOUPri.SelectAll();

        private void TxtHWLS_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e) => txtHWLS.SelectAll();

        private void TxtPackers_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e) => txtPackers.SelectAll();

        #endregion GotMouseCapture Methods

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                ClearFields();
            }

            if (e.Key == System.Windows.Input.Key.F12)
            {
                WindowState = WindowState.Minimized;
            }

            if (e.Key == System.Windows.Input.Key.OemTilde)
            {
                Icon = Icon == ABC ? Mario : ABC;
            }
        }
    }
}