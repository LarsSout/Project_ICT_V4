using Project_ICT_V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_ICT_V4
{
    /// <summary>
    /// Interaction logic for alfabeth.xaml
    /// </summary>
    public partial class Alfabeth : Window
    {
        

        string morseCodeEnglish =
                "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
                "I: ..    J: .---    K: -.-     L: .-..   M: -- \r\n N: -.     O: ---    P: .--.   " +
                "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
                "Y: -.--  Z: --..  \r\n 1: .----   2: ..---  3: ...--  4: ....-  5: .....  6: -....  " +
                "7: --... 8: ---..   9: ----.   0: -----  .: .-.-.-    ,: --..--  \r\n  ?: ..--..   " +
                "!: -.-.--  : : ---...  ;: -.-.-.  -: -....-  ,: .-..-.   (: -.--.   ): -.--.-  " +
                "@: .--.-.  =: -...-";

        string morseCodeRussian =
            "А: .-    Б: -...    В: .--    Г: --.    Д: -..    Е: .    Ё: .    Ж: ...-    " +
            "З: --..   И: ..    Й: .---    К: -.-    Л: .-..  \r\n М: --    Н: -.     П: .--.   " +
            "Р: .-.    С: ...    Т: -     У: ..-    Ф: ..-.   Х: ....   Ц: -.-. \r\n  Ч: ---." +
            "Ш: ----   Щ: --.-   Ъ: .--.-.  Ы: -.--    Ь: -..-    Э: ..-..   Ю: ..--    " +
            "Я: .-.-";

        string morseCodeFrench =
            "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
            "I: ..    J: .---    K: -.-     L: .-..   M: --  \r\n N: -.     O: ---    P: .--.   " +
            "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
            "Y: -.--  Z: --.. \r\n   À: .--.-    Â: .-..    Ç: -.-..    È: .-..    É: ..-..   " +
            "Ê: -...    Ë: .....    Ô: ---    Ù: ..--    Û: ..-..   Ü: ..--";

        string morseCodeSpanish =
            "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
            "I: ..    J: .---    K: -.-     L: .-..   M: --  \r\n N: -.     O: ---    P: .--.   " +
            "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
            "Y: -.--  Z: --..   \r\n 1: .----   2: ..---  3: ...--  4: ....-  5: .....  6: -....  " +
            "7: --... 8: ---..   9: ----.   0: -----  Á: .--.-    Í: ..    Ñ: --.--    Ó: ---    Ú: ..--    Ü: ..--";

        string morseCodeGerman =
            "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
            "I: ..    J: .---    K: -.-     L: .-..   M: --  \r\n  N: -.     O: ---    P: .--.   " +
            "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
            "Y: -.--  Z: --..  \r\n   1: .----   2: ..---  3: ...--  4: ....-  5: .....  6: -....  " +
            "7: --... 8: ---..   9: ----.   0: -----  Ä: .-.-    Ö: ---    \r\n Ü: ..--    ß: ...";

        string morseCodeItalian =
            "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
            "I: ..    J: .---    K: -.-     L: .-..   M: --  \r\n  N: -.     O: ---    P: .--.   " +
            "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
            "Y: -.--  Z: --..   \r\n  1: .----   2: ..---  3: ...--  4: ....-  5: .....  6: -....  " +
            "7: --... 8: ---..   9: ----.   0: -----  è: .-..    é: ..-..    \r\n ì: ..    ù: ..--";

        string morseCodeTurkish =
            "A: .-    B: -...    C: -.-.    D: -..    E: .    F: ..-.   G: --.    H: ....    " +
            "I: ..    J: .---    K: -.-     L: .-..   M: --  \r\n  N: -.     O: ---    P: .--.   " +
            "Q: --.-  R: .-.     S: ...     T: -      U: ..-  V: ...-   W: .--    X: -..-   " +
            "Y: -.--  Z: --..   \r\n  1: .----   2: ..---  3: ...--  4: ....-  5: .....  6: -....  " +
            "7: --... 8: ---..   9: ----.   0: -----  ç: -.-..    ı: ..    \r\n ğ: --.    ş: ...    " +
            "ü: ..--    ö: ---.";

        string morseCodeChinese =
            "你: -.--.    好: .-..    是: ...-.    我: --..    的: -.-    不: ..-..    " +
            "了: .-.-    在: --.-    人: .-..    有: ---  \r\n   这: --..    中: -..    " +
            "大: -...    来: .--.    上: ...    国: -.-.    个: --..    说: -..    " +
            "们: -.--    到: .-..";

        public Alfabeth()
        {
            InitializeComponent();

            talenAlfabet.Items.Add("English");
            talenAlfabet.Items.Add("Francais");
            talenAlfabet.Items.Add("Español");
            talenAlfabet.Items.Add("Deutsch");
            talenAlfabet.Items.Add("Türkçe");
            talenAlfabet.Items.Add("Русский");
            talenAlfabet.Items.Add("Italiano");
            talenAlfabet.Items.Add("中國人");
        }

        public void GiveMors()
        {
            switch (talenAlfabet.SelectedItem)
            {
                case "English":
                    morscodes.Content = morseCodeEnglish;
                    break;

                case "Francais":
                    morscodes.Content = morseCodeFrench;
                    break;

                case "Русский":
                    morscodes.Content = morseCodeRussian;
                    break;

                case "Español":
                    morscodes.Content = morseCodeSpanish;
                    break;

                case "Deutsch":
                    morscodes.Content = morseCodeGerman;
                    break;

                case "Italiano":
                    morscodes.Content = morseCodeItalian;
                    break;

                case "Türkçe":
                    morscodes.Content = morseCodeTurkish;
                    break;

                case "中國人":
                    morscodes.Content = morseCodeChinese;
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GiveMors();
        }
    }
}
