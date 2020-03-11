using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Text.RegularExpressions;


namespace Cryptology_1_lab
{

    public partial class MainWindow : Window
    {

        string filename = "";
        string alfabet = "";
        public int To = 0;
        int a,b,c = 0;
        bool buttin1Clicked, button2Clicked = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                FlowDocument flowDocument = new FlowDocument();
                foreach (string line in textbox.Text.Split('\n'))
                {
                    Paragraph myParagraph = new Paragraph();
                    myParagraph.Margin = new Thickness(0);
                    myParagraph.Inlines.Add(new Run(line));
                    flowDocument.Blocks.Add(myParagraph);
                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                printDialog.PrintDocument(paginator, this.textbox.Text);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                textbox.Text = File.ReadAllText(openFileDialog.FileName);
            textbox.Visibility = System.Windows.Visibility.Visible;

            getfilename(openFileDialog.FileName);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void SecondVectorclick(object sender, RoutedEventArgs e)
        {
            ALabel.Visibility = BLabel.Visibility =ATextBox.Visibility =BTextBox.Visibility=
                System.Windows.Visibility.Visible;
            buttin1Clicked = true;

        }
        private void EnterKeyButton(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (int.TryParse(ATextBox.Text, out i) && int.TryParse(BTextBox.Text, out i) && int.TryParse(ATextBox.Text, out i))
            {
                a = Convert.ToInt32(ATextBox.Text);
                b = Convert.ToInt32(BTextBox.Text);
                c = Convert.ToInt32(CTextBox.Text);
            }
            else { MessageBox.Show("Vaues are not valid"); }
        }
        private void ThirdVectorclick(object sender, RoutedEventArgs e)
        {
            ALabel.Visibility = BLabel.Visibility = ATextBox.Visibility = BTextBox.Visibility = CLabel.Visibility = CTextBox.Visibility=
               System.Windows.Visibility.Visible;
            button2Clicked = true;
        }
        private void getfilename(string path)
        {
            filename = path;
        }
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textbox.Text);
            getfilename(saveFileDialog.FileName);
            // textbox.Text = filename;
            MessageBox.Show("File saved there :" + filename);
        }
        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            textbox.Visibility = System.Windows.Visibility.Visible;
        }




        private void About_btn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developer:Nadiia Padalka.Year:2020");
        }

        private void Close_btn(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public class CaesarCipher
        {
            //символи української абетки
            //const string alfabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
            private string CodeEncode3d(string text,int a, int b, int c, string alfabet)
            {
                var fullAlfabet = alfabet + alfabet.ToLower();
                var letterQty = fullAlfabet.Length;
                var retVal = "";
                for (int i = 0; i < text.Length; i++)
                {
                    var k = text[i];
                    var index = fullAlfabet.IndexOf(k);
                    var letterindex = i + 1;

                    if (index < 0)
                    {
                        //якщо літеру не знайдено, додаємо її незмінною
                        retVal += k.ToString();
                    }
                    else
                    {
                        var codeIndex = ( index + a*a + letterindex * b + c) % letterQty;
                        retVal += fullAlfabet[codeIndex];
                    }
                }

                return retVal;
            }
            private string CodeEncode(string text, int a, int b, string alfabet)

            {
                var fullAlfabet = alfabet + alfabet.ToLower();
                var letterQty = fullAlfabet.Length;
                var retVal = "";
                for (int i = 0; i < text.Length; i++)
                {
                    var c = text[i];
                    var index = fullAlfabet.IndexOf(c);
                    var letterindex = i + 1;
                    int k = a * letterindex + b;

                    if (index < 0)
                    {
                        //якщо літеру не знайдено, додаємо її незмінною
                        retVal += c.ToString();
                    }
                    else
                    {
                        var codeIndex = ( index + a*letterindex+ b) % letterQty;
                        retVal += fullAlfabet[codeIndex];
                    }
                }
                return retVal;
            }

            private string Encode(string text, int a, int b, string alfabet)

            {
                var fullAlfabet = alfabet + alfabet.ToLower();
                var letterQty = fullAlfabet.Length;
                var retVal = "";
                for (int i = 0; i < text.Length; i++)
                {
                    var c = text[i];
                    var index = fullAlfabet.IndexOf(c);
                    //int k = a * index + b;
                    int letterindex = i + 1;
                    int k = a * letterindex + b;

                    if (index < 0)
                    {
                        //якщо літеру не знайдено, додаємо її незмінною
                        retVal += c.ToString();
                    }
                    else
                    {
                        var codeIndex = (index +letterQty - (k % letterQty)) % letterQty;
                        retVal += fullAlfabet[codeIndex];
                    }
                }
                return retVal;
            }
            private string Encode3d(string text, int a, int b,int c, string alfabet)

            {
                var fullAlfabet = alfabet + alfabet.ToLower();
                var letterQty = fullAlfabet.Length;
                var retVal = "";
                for (int i = 0; i < text.Length; i++)
                {
                    var ch = text[i];
                    var index = fullAlfabet.IndexOf(ch);
                    int letterindex = i + 1;
                    var k = a * a + letterindex * b + c;

                    if (index < 0)
                    {
                        //якщо літеру не знайдено, додаємо її незмінною
                        retVal += c.ToString();
                    }
                    else
                    {
                        var codeIndex = (index + letterQty - (k % letterQty)) % letterQty;
                        retVal += fullAlfabet[codeIndex];
                    }
                }
                return retVal;
            }

            public string Encrypt(string plainMessage, int a, int b,  string alfabet)
                => CodeEncode(plainMessage, a, b, alfabet);
            public string Encrypt3d(string plainMessage, int a, int b, int c, string alfabet)
                => CodeEncode3d(plainMessage, a, b, c, alfabet);

            public string Decrypt(string encryptedMessage, int a, int b,  string alfabet)
               => Encode(encryptedMessage, a,b, alfabet);

            public string Decrypt3d(string encryptedMessage, int a, int b, int c, string alfabet)
               => Encode3d(encryptedMessage, a, b,c, alfabet);
        }
        private void Guess_btn(object sender, RoutedEventArgs e)
        {
            //var cipher = new CaesarCipher();
            ////SaveFileDialog saveFileDialog = new SaveFileDialog();
            ////if (saveFileDialog.ShowDialog() == true)
            ////    File.WriteAllText(saveFileDialog.FileName, textbox.Text);
            ////for (int i =0; i< int.Parse(To_TextBox.Text); i++)
            ////{ cipher.Decrypt(Encrypted_text.Text, key, alfabet); }
            //using (System.IO.StreamWriter file =
            //new System.IO.StreamWriter(@"C:\Users\1\Desktop\unii\bruteforce.txt"))
            //{
            //    for (int i = 0; i < int.Parse(To_TextBox.Text); i++)
            //    {
            //        // If the line doesn't contain the word 'Second', write the line to the file.

            //        file.WriteLine(cipher.Decrypt(Encrypted_text.Text, i, alfabet));

            //    }
            //}

        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var cipher = new CaesarCipher();
            a = Int32.Parse(ATextBox.Text);
            b = Int32.Parse(BTextBox.Text);
            // textbox.Text = cipher.Decrypt(Encrypted_text.Text, key, alfabet);
            if (buttin1Clicked == true)
            {
                MessageBox.Show(cipher.Decrypt(Encrypted_text.Text, a, b, alfabet));
                textbox.Text = cipher.Decrypt(Encrypted_text.Text, a, b, alfabet);
            }
            else if (button2Clicked == true)
            {

                c = Int32.Parse(CTextBox.Text);
                MessageBox.Show(cipher.Decrypt3d(Encrypted_text.Text, a, b,c, alfabet));

                textbox.Text = cipher.Decrypt3d(Encrypted_text.Text, a, b,c, alfabet);

            }
            // Encrypted_text.Text = cipher.Encrypt3d(textbox.Text, a, b, c, alfabet); 
            // TextLabel.Content = "Decrypted text";
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            a = Int32.Parse(ATextBox.Text);
            b = Int32.Parse(BTextBox.Text);
            var fullAlfabet = alfabet + alfabet.ToLower();
            Dictionary<string, int> occurencies =
            new Dictionary<string, int>();
            foreach (char element in textbox.Text)
            {
                count++;
                MessageBox.Show($"Element #{count}: {element}");
            }
            var cipher = new CaesarCipher();
            //[А-ЩЬЮЯҐЄІЇа-щьюяґєії]
            if ((a >= 0) && (a < int.Parse(To_TextBox.Text))&& (b >= 0) && (b < int.Parse(To_TextBox.Text)))
            {
                Validation_Label.Content = "Valid";
                Validation_Label.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                Validation_Label.Content = "Not Valid";
                Validation_Label.Background = new SolidColorBrush(Colors.Red);
            }
            if (Language_Box.Text == "Ukrainian")
            {
                if (Regex.IsMatch(textbox.Text, "[А-ЩЬЮЯҐЄІЇа-щьюяґєії]"))
                {
                    Validation_Label_Message.Content = "Valid";
                    Validation_Label_Message.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Validation_Label_Message.Content = "Not Valid";
                    Validation_Label_Message.Background = new SolidColorBrush(Colors.Red);
                }

            }
            else if (Language_Box.Text == "English")
            {
                if (Regex.IsMatch(textbox.Text, "[a-zA-Z]"))
                {
                    Validation_Label_Message.Content = "Valid";
                    Validation_Label_Message.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Validation_Label_Message.Content = "Not Valid";
                    Validation_Label_Message.Background = new SolidColorBrush(Colors.Red);
                }
            }
            else
            {
                Validation_Label_Message.Content = "Not Valid";
                Validation_Label_Message.Background = new SolidColorBrush(Colors.Red);
            }
            if (Validation_Label.Content == "Not Valid" || Validation_Label_Message.Content == "Not Valid")
            { MessageBox.Show("Check your input again!"); }
            else if (Validation_Label.Content == "Valid" || Validation_Label_Message.Content == "Valid")
            {
                if (buttin1Clicked==true)
                { Encrypted_text.Text = cipher.Encrypt(textbox.Text, a, b, alfabet); }
                else if (button2Clicked==true)
                {
                    c = Int32.Parse(CTextBox.Text);

                    Encrypted_text.Text = cipher.Encrypt3d(textbox.Text, a, b, c, alfabet); }
            }
        }
        private void eng_btn_Click(object sender, RoutedEventArgs e)
        {
            Language_Box.Text = "English";
            To_TextBox.Text = "25";
            alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            From_TextBox.Text = "0";
            To = 26;

        }
        private void ukr_btn_Click(object sender, RoutedEventArgs e)
        {
            alfabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";

            Language_Box.Text = "Ukrainian";
            To_TextBox.Text = "32";
            From_TextBox.Text = "0";
            To = 33;
        }



        private void TextBox_TextChanged_2(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}


