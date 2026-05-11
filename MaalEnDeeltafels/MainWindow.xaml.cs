using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MaalEnDeeltafels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _correctResult;
        Random rng = new Random();
        private int _exercicesCount;
        private string _exercicesChoice;
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick (object sender, EventArgs e)
        {
            dateLabel.Content = DateTime.Now.ToLongTimeString();
        }

        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wil je afsluiten?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private string PracticeMultiply()
        {          
            int number1 = rng.Next(1, 11);
            int number2 = rng.Next(1, 11);
            int result = number1 * number2;
            _correctResult = result;
            return ($"{number1} x {number2} =");            
        }        

        private void OnMultiplicationButtonClicked(object sender, RoutedEventArgs e)
        {
            _exercicesChoice = "Multiplication";
            inputTextBlock.Text = PracticeMultiply();
            solutionTextBox.Focus();
        }

        private string PracticeDivision()
        {
            int number1 = rng.Next(1, 11);
            int number2 = rng.Next(1, 11);
            int result = (number1 * number2) / number2; // of gewoon 'number1'
            _correctResult = result;
            return ($"{number1 * number2} : {number2}");
        }

        private void OnDivisionButtonClicked(object sender, RoutedEventArgs e)
        {
            _exercicesChoice = "Division";
            inputTextBlock.Text = PracticeDivision();
            solutionTextBox.Focus();
        }

        private string PracticeMixedExercices()
        {
            int choice = rng.Next(1, 3);
            if (choice == 1)
            {
                return PracticeMultiply();
            }
            else 
            {
                return PracticeDivision();
            }
        }

        private void OnMixButtonClicked(object sender, RoutedEventArgs e)
        {
            _exercicesChoice = "Mix";
            inputTextBlock.Text = PracticeMixedExercices();
            solutionTextBox.Focus();
        }

        private void OnKeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                warningTextBlock.Text = "";
                
                bool isValidResult = int.TryParse(solutionTextBox.Text, out int result);
                if (!isValidResult)
                {
                    warningTextBlock.Foreground = Brushes.Red;
                    warningTextBlock.Text = "Geef een getal in.";
                    solutionTextBox.Clear();
                    return;
                }

                if (result == _correctResult)
                {
                    _exercicesCount++;
                    if (_exercicesCount == 5)
                    {
                        warningTextBlock.Foreground = Brushes.Pink;
                        warningTextBlock.Text = "Goed bezig! Doe zo voort!";
                    }

                    if (_exercicesCount == 10)
                    {
                        warningTextBlock.Foreground = Brushes.Pink;
                        warningTextBlock.Text = "Heel sterk! Oefenen maar!";
                    }

                    if (_exercicesCount >= 5 && _exercicesCount < 10)
                    {
                        awardImage.Source = new BitmapImage(new Uri("Images/zeepaardje.jpeg", UriKind.Relative));
                        awardImage.Stretch = Stretch.Uniform;
                    }
                    else if (_exercicesCount >= 10)
                    {
                        awardImage.Source = new BitmapImage(new Uri("Images/eenhoorn.jpeg", UriKind.Relative));
                        awardImage.Stretch = Stretch.Uniform;
                    }

                        switch (_exercicesChoice)
                        {
                            case "Multiplication":
                                inputTextBlock.Text = PracticeMultiply();
                                break;

                            case "Division":
                                inputTextBlock.Text = PracticeDivision();
                                break;

                            case "Mix":
                                inputTextBlock.Text = PracticeMixedExercices();
                                break;
                        }                    
                    solutionTextBox.Clear();
                }
                else
                {
                    warningTextBlock.Foreground = Brushes.Red;
                    warningTextBlock.Text = "Probeer nog eens!";
                    solutionTextBox.Clear();
                }
            }
        }       
    }
}