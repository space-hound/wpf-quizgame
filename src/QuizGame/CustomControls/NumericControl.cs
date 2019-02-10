using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace QuizGame.CustomControls
{
    public class NumericControl : Control, INotifyPropertyChanged
    {
        //un event handler un fel de delgate mai special
        //care se creaza in afara clasei
        public event PropertyChangedEventHandler PropertyChanged;

        //eventul care ruleaza PropertyChanged de mai sus
        //se ruleaza cand se schimba o proprietate a acestei clase(acestui obiect)
        private void RaiseProperty(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private RepeatButton UpButton;
        private RepeatButton DownButton;
        private TextBox NumberDisplay;

        /**********************************************************/
            //---> DEPENDENCY PROPERTIES
        /**********************************************************/
        //proprietati dependent, pot sa le schimb din XAML
        //proprietati mai sepciale
        //ele trebuiesc inregistarte
        public readonly static DependencyProperty MaximumProperty;
        public readonly static DependencyProperty MinimumProperty;
        public readonly static DependencyProperty ValueProperty;
        public readonly static DependencyProperty StepProperty;

        //declararea stiluii
        //inregistrarea proprietatilor depndente
        static NumericControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericControl), new FrameworkPropertyMetadata(typeof(NumericControl)));
            MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(NumericControl), new UIPropertyMetadata(10));
            MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(NumericControl), new UIPropertyMetadata(0));
            StepProperty = DependencyProperty.Register("StepValue", typeof(int), typeof(NumericControl), new FrameworkPropertyMetadata(1));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericControl), new FrameworkPropertyMetadata(0));
        }

        //va fi suprascris la inceput din Bridge cu nr de intrebari existente
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                
                SetCurrentValue(ValueProperty, value);
                this.updateDisplay();
            }
        }
        public int StepValue
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpButton = Template.FindName("Part_UpButton", this) as RepeatButton;
            DownButton = Template.FindName("Part_DownButton", this) as RepeatButton;
            NumberDisplay = Template.FindName("Part_Display", this) as TextBox;
            UpButton.Click += _UpButton_Click;
            DownButton.Click += _DownButton_Click;
            NumberDisplay.Loaded += NumberDisplay_Loaded;
        }

        //cand se schimba valoarea o afiseaza in textbox
        private void updateDisplay()
        {
            NumberDisplay.Text = Value.ToString();
        }

        //event pentru cand e incarcat 
        private void NumberDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            NumberDisplay.Text = Value.ToString();
        }

        //event pentru click pe butonul de down schimba valoarea daca poate cu -1
        void _DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > Minimum)
            {
                Value -= StepValue;
                if (Value < Minimum)
                    Value = Minimum;
            }

            NumberDisplay.Text = Value.ToString();
            this.RaiseProperty("Value");
        }
        //event pentru click pe butonul de up schimba valoarea daca poate cu +1
        void _UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < Maximum)
            {
                Value += StepValue;
                if (Value > Maximum)
                    Value = Maximum;
            }

            NumberDisplay.Text = Value.ToString();
            this.RaiseProperty("Value");
        }
    }
}
