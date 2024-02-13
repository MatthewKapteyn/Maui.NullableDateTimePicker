using System.ComponentModel;

namespace Maui.NullableDateTimePicker.Samples.Net8
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        DateTime? myDateTime = DateTime.Now;
        public DateTime? MyDateTime
        {
            get => myDateTime;
            set
            {
                myDateTime = value;
                OnPropertyChanged(nameof(MyDateTime));
            }
        }

        public MainPage()
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                var dateStoreTest = new CDateStore { MyDefaultText = "Pick me #" + i };

                if (i % 2 == 0)
                {
                    var TimePickerView = new NullableDateTimePicker
                    {
                        Mode = PickerModes.DateTime,
                        Format = "h:mm tt",
                        MilitaryTime = false,
                        CornerRadius = 5,
                        Padding = new Thickness(5, 0, 5, 0),
                        ShowIcons = false
                    };

                    TimePickerView.SetBinding(NullableDateTimePicker.NullableDateTimeProperty, new Binding() { Path = "MyDateTime", Source = dateStoreTest, Mode = BindingMode.TwoWay });
                    TimePickerView.SetBinding(NullableDateTimePicker.PlaceholderProperty, new Binding() { Path = "MyDefaultText", Source = dateStoreTest, Mode = BindingMode.TwoWay });
                    DateListGrid.Add(TimePickerView, 0, i);
                }
                else
                {
                    var DatePickerView = new NullableDateTimePicker
                    {
                        Mode = PickerModes.Date,
                        Format = "yyyy-MM-dd",
                        CornerRadius = 5,
                        Padding = new Thickness(5, 0, 5, 0),
                        ShowIcons = true
                    };

                    DatePickerView.SetBinding(NullableDateTimePicker.NullableDateTimeProperty, new Binding() { Path = "MyDateTime", Source = dateStoreTest, Mode = BindingMode.TwoWay });
                    DatePickerView.SetBinding(NullableDateTimePicker.PlaceholderProperty, new Binding() { Path = "MyDefaultText", Source = dateStoreTest, Mode = BindingMode.TwoWay });
                    DateListGrid.Add(DatePickerView, 0, i);
                }
            }

            BindingContext = this;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            await Navigation.PushModalAsync(new NewPage());
        }
    }

    public class CDateStore : INotifyPropertyChanged
    {
        public string MyDefaultText { get; set; } = "What datetime is it?";

        DateTime? myDateTime { get; set; }
        public DateTime? MyDateTime
        {
            get => myDateTime;
            set
            {
                myDateTime = value;
                RaisePropertyChanged("MyDateTime");
            }
        }

        // Notifies any consuming UIs that some data has changed and they need to refresh accordingly
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
