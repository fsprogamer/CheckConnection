﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Media;

namespace IPmaskedtextbox
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IPMaskedTextBox : UserControl
    {
        #region class variables and properties

        #region public variables and properties
        public TextBox FirstBox { get { return firstBox; } }
        public TextBox SecondBox { get { return secondBox; } }
        public TextBox ThirdBox { get { return thirdBox; } }
        public TextBox FourthBox { get { return fourthBox; } }
        #endregion

        #region private variables and properties
        private const string errorMessage = "Укажите значение в интервале от 0 до 255.";
        #endregion

        #endregion


        #region constructors
        public IPMaskedTextBox()
        {
            InitializeComponent();
        }

        public IPMaskedTextBox(byte[] bytesToFill)
        {
            InitializeComponent();

            firstBox.Text = Convert.ToString(bytesToFill[0]);
            secondBox.Text = Convert.ToString(bytesToFill[1]);
            thirdBox.Text = Convert.ToString(bytesToFill[2]);
            fourthBox.Text = Convert.ToString(bytesToFill[3]);
        }
        #endregion


        #region methods
        
        #region public methods
        public byte[] GetByteArray()
        {
            byte[] userInput = new byte[4];

            userInput[0] = Convert.ToByte(firstBox.Text);
            userInput[1] = Convert.ToByte(secondBox.Text);
            userInput[2] = Convert.ToByte(thirdBox.Text);
            userInput[3] = Convert.ToByte(fourthBox.Text);

            return userInput;
        }

        //public string Text
        //{
        //    get
        //    {
        //        string[] userInput = new string[4];

        //        userInput[0] = firstBox.Text;
        //        userInput[1] = secondBox.Text;
        //        userInput[2] = thirdBox.Text;
        //        userInput[3] = fourthBox.Text;
        //        return string.Join(".", userInput);
        //    }
        //    set
        //    {
        //        string[] userInput = value.Split('.');

        //        firstBox.Text = userInput[0];
        //        secondBox.Text = userInput[1];
        //        thirdBox.Text = userInput[2];
        //        fourthBox.Text = userInput[3];
        //    }
        //}


        /// <summary>
        /// Gets or sets the Value which is being displayed
        /// </summary>
        /// 
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object),
              typeof(IPMaskedTextBox), new PropertyMetadata(null));

        //public object firstValue
        //{
        //    get { return (object)GetValue(firstValueProperty); }
        //    set { SetValue(firstValueProperty, value); }
        //}

        ///// <summary>
        ///// Identified the Label dependency property
        ///// </summary>
        //public static readonly DependencyProperty firstValueProperty =
        //    DependencyProperty.Register("firstValue", typeof(object),
        //      typeof(IPMaskedTextBox), new PropertyMetadata(null));

        //public object secondValue
        //{
        //    get { return (object)GetValue(secondValueProperty); }
        //    set { SetValue(secondValueProperty, value); }
        //}

        ///// <summary>
        ///// Identified the Label dependency property
        ///// </summary>
        //public static readonly DependencyProperty secondValueProperty =
        //    DependencyProperty.Register("secondValue", typeof(object),
        //      typeof(IPMaskedTextBox), new PropertyMetadata(null));

        //public object thirdValue
        //{
        //    get { return (object)GetValue(thirdValueProperty); }
        //    set { SetValue(thirdValueProperty, value); }
        //}

        ///// <summary>
        ///// Identified the Label dependency property
        ///// </summary>
        //public static readonly DependencyProperty thirdValueProperty =
        //    DependencyProperty.Register("thirdValue", typeof(object),
        //      typeof(IPMaskedTextBox), new PropertyMetadata(null));

        //public object forthValue
        //{
        //    get { return (object)GetValue(forthValueProperty); }
        //    set { SetValue(forthValueProperty, value); }
        //}

        ///// <summary>
        ///// Identified the Label dependency property
        ///// </summary>
        //public static readonly DependencyProperty forthValueProperty =
        //    DependencyProperty.Register("forthValue", typeof(object),
        //      typeof(IPMaskedTextBox), new PropertyMetadata(null));
        #endregion

        #endregion

        #region private methods
        private void jumpRight(TextBox rightNeighborBox, KeyEventArgs e)
        {
                rightNeighborBox.Focus();
                rightNeighborBox.CaretIndex = 0;
                e.Handled = true;
        }

        private void jumpLeft(TextBox leftNeighborBox, KeyEventArgs e)
        {
            leftNeighborBox.Focus();
            if (leftNeighborBox.Text != "")
            {
                leftNeighborBox.CaretIndex = leftNeighborBox.Text.Length;
            }
            e.Handled = true;
        }

        //checks for backspace, arrow and decimal key presses and jumps boxes if needed.
        //returns true when key was matched, false if not.
        private bool checkJumpRight(TextBox currentBox, TextBox rightNeighborBox, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    if (currentBox.CaretIndex == currentBox.Text.Length || currentBox.Text == "")
                    {
                        jumpRight(rightNeighborBox, e);
                    }
                    return true;
                case Key.OemPeriod:
                case Key.Decimal:
                case Key.Space:
                    jumpRight(rightNeighborBox, e);
                    rightNeighborBox.SelectAll();
                    return true;
                default:
                    return false;
            }
        }

        private bool checkJumpLeft(TextBox currentBox, TextBox leftNeighborBox, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (currentBox.CaretIndex == 0 || currentBox.Text == "")
                    {
                        jumpLeft(leftNeighborBox, e);
                    }
                    return true;
                case Key.Back:
                    if ((currentBox.CaretIndex == 0 || currentBox.Text == "") && currentBox.SelectionLength == 0)
                    {
                        jumpLeft(leftNeighborBox, e);
                    }
                    return true;
                default:
                    return false;
            }
        }

        //discards non digits, prepares IPMaskedBox for textchange.
        private void handleTextInput(TextBox currentBox, TextBox rightNeighborBox, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(Convert.ToChar(e.Text)))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
                return;
            }

            if (currentBox.Text.Length == 3 && currentBox.SelectionLength == 0)
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
                if (currentBox != fourthBox)
                {
                    rightNeighborBox.Focus();
                    rightNeighborBox.SelectAll();
                }
            }
        }

        //checks whether textbox content > 255 when 3 characters have been entered.
        //clears if > 255, switches to next textbox otherwise 
        private void handleTextChange(TextBox currentBox, TextBox rightNeighborBox)
        {
            if (currentBox.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(currentBox.Text);

                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    currentBox.Clear();
                    currentBox.Focus();
                    SystemSounds.Beep.Play();
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (currentBox.CaretIndex != 2 && currentBox != fourthBox)
                {
                    rightNeighborBox.CaretIndex = rightNeighborBox.Text.Length;
                    rightNeighborBox.SelectAll();
                    rightNeighborBox.Focus();
                }
            }
        }
        #endregion      

        #region Events
        //jump right, left or stay. 
        private void firstByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkJumpRight(firstBox, secondBox, e);
        }

        private void secondByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (checkJumpRight(secondBox, thirdBox, e))
                return;

            checkJumpLeft(secondBox, firstBox, e);
        }

        private void thirdByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (checkJumpRight(thirdBox, fourthBox, e))
                return;

            checkJumpLeft(thirdBox, secondBox, e);
        }

        private void fourthByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkJumpLeft(fourthBox, thirdBox, e);

            if (e.Key == Key.Space)
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }


        //discards non digits, prepares IPMaskedBox for textchange.
        private void firstByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handleTextInput(firstBox, secondBox, e);
        }

        private void secondByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handleTextInput(secondBox, thirdBox, e);
        }

        private void thirdByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handleTextInput(thirdBox, fourthBox, e);
        }

        private void fourthByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            handleTextInput(fourthBox, fourthBox, e); //pass fourthbyte twice because no right neighboring box.
        }


        //checks whether textbox content > 255 when 3 characters have been entered.
        //clears if > 255, switches to next textbox otherwise 
        private void firstByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            handleTextChange(firstBox, secondBox);
        }

        private void secondByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            handleTextChange(secondBox, thirdBox);
        }

        private void thirdByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            handleTextChange(thirdBox, fourthBox);
        }

        private void fourthByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            handleTextChange(fourthBox, fourthBox);
        }
        #endregion
    }
}
