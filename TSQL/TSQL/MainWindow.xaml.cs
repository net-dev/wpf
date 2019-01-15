using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        const int INTERVAL_COUNT = 5;

        int CurrentInterval = 0;

        public MainWindow() {
            InitializeComponent();

            BindDataGrid();

            DispatcherTimer dtClockTime = new DispatcherTimer();

            dtClockTime.Interval = new TimeSpan(0, 0, 1);
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            if (Percent.Value == 0) {
                CurrentInterval++;
                if (CurrentInterval == INTERVAL_COUNT)
                {
                    CurrentInterval = 0;
                    // get bitmap from web url (database) from web getting while setting the date (caching)
                    BitmapImage bmi = new BitmapImage();
                    bmi.BeginInit();
                    bmi.StreamSource = Application.GetResourceStream(new Uri("/ship.jpg", UriKind.Relative)).Stream;
                    bmi.EndInit();
                    // testure is identifier from xaml
                    texture.Brush = new ImageBrush(bmi)
                    { ViewportUnits = BrushMappingMode.Absolute };
                    DayBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
            } if (Percent.Value == 90) {
                CurrentInterval++;
                if (CurrentInterval == INTERVAL_COUNT)
                {
                    CurrentInterval = 0;
                    NightBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
            }
        }

        private void BindDataGrid() {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //con.Open();
            //if (con.State == ConnectionState.Open) {
            //    SqlCommand cmd = new SqlCommand("select * from [Nodes]", con);
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable("Nodes");
            //    da.Fill(dt);
            //    g1.ItemsSource = dt.DefaultView;
            //}
        }

    }

    public class LeftMarginConverter : IValueConverter
    {

        const double HEURISTIC_OFFSET = 4.3;

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double sliderValue = (double)value;
            return new Thickness(HEURISTIC_OFFSET * sliderValue, 0, -HEURISTIC_OFFSET * sliderValue, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    public class OpacityConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double sliderValue = (double)value;
            if (sliderValue <= 45)
            {
                return ((45 - sliderValue) / 45);
            } else
            {
                return ((sliderValue - 45) / 45);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

}
