using System;
using System.Diagnostics.Tracing;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_ICT_V3
{
    public partial class MainWindow : Window
    {
        private SerialPort _port = new SerialPort();  // De seriële poort voor communicatie met externe apparaten

        // Declaratie van strings
        private string output = "";                   // Output voor morsecode
        private string input = "";                    // Input van de gebruiker

        // Declaratie van integers
        private int klik = 0;                         // Klikstatus voor verwerking
        public int resetButtonClick = 0;             // Houdt bij hoeveel keer de resetknop is gebruikt

        // Declaratie van booleans
        private bool isProcessing = false;            // Geeft aan of een actie bezig is
        private bool COMtest = false;                 // Controleer of de COM-poort beschikbaar is
        private bool MagDoorDoen = true;              // Controleer of de conversie doorgaat
        public bool BuzzerChecked;

      

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += WindowClosedReset; // Event gekoppeld om de poort te resetten bij sluiting van het venster
            this.KeyDown += Window_KeyDown;  // Event gekoppeld voor toetsafhandeling

            


            if (BuzzerChecked)
            {
                if (_port.IsOpen)
                {
                    _port.WriteLine("BUZZER_UIT"); // Stuur commando naar Arduino
                }
            }
            else {

                if (_port.IsOpen)
                {
                    _port.WriteLine("BUZZER_AAN"); // Stuur commando naar Arduino

                }
            }
            


            // Detecteren van een beschikbare COM-poort
            for (int i = 1; i < 100; i++)
            {
                COMtest = true;

                while (COMtest)
                {
                    try
                    {
                        _port.PortName = "COM" + i;   // Probeer poorten vanaf COM1
                        _port.BaudRate = 9600;       // Stel de baudrate in
                        _port.Open();               // Open de poort
                    }
                    catch
                    {
                        COMtest = false;             // Indien fout, probeer de volgende poort
                    }

                    if (COMtest == true)
                    {
                        i = 99;                      // Als poort beschikbaar is, beëindig de lus
                    }
                }

                // Als geen poort gevonden wordt, beëindig programma
                if (i == 90)
                {
                    MessageBox.Show("Geen COM poort aangesloten, het programma wordt beëindigd");
                    System.Environment.Exit(1);     // Sluit programma
                }
            }

            // Toon de actieve COM-poort in de interface
            COMPOORToutput.Text = _port.PortName;

            // Voeg de KeyDown event handler toe voor de invoerbox
            invoerBox.KeyDown += EnterKeyPress;
            invoerBox.KeyDown += CTRLr; // Voeg de CTRLe method toe om Ctrl+R te detecteren
        }

        /// <summary>
        /// Reset de seriële poort en stuurt een RESET-commando voordat het venster sluit.
        /// </summary>
        public void WindowClosedReset(object? sender, EventArgs e)
        {
            if (_port != null && _port.IsOpen)
            {
                try
                {
                    _port.WriteLine("RESET"); // Verstuur RESET-commando naar de externe hardware
                    _port.Close();            // Sluit de seriële poort
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fout bij het sluiten van de seriële poort: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Verwerkt de invoer van de gebruiker naar morsecode en stuurt dit naar de seriële poort.
        /// </summary>
        public async Task Morsconverteren()
        {
            if (_port.IsOpen)
            {
                klik++;

                while (klik < 2)
                {
                    MagDoorDoen = true;

                    MorsConverter converter = new MorsConverter(); // Initialiseer de converter
                    input = invoerBox.Text; // Verkrijg de invoer van de gebruiker

                    if (!string.IsNullOrEmpty(input))
                    {
                        output = converter.CheckOutput(input); // Converteer naar morse
                        antwoord.Text = output;               // Toon output in de interface
                    }
                    else
                    {
                        antwoord.Text = "Voer tekst in om te converteren."; // Geen invoer ontvangen
                        return;
                    }

                    foreach (char character in output)
                    {
                        if (character == '.' && MagDoorDoen)
                        {
                            _port.WriteLine(".");
                            DOTcirkel.Fill = Brushes.Blue; // Blauw voor punt
                            await Task.Delay(200);
                            DOTcirkel.Fill = Brushes.White;
                            await Task.Delay(200);
                        }
                        else if (character == '-' && MagDoorDoen)
                        {
                            _port.WriteLine("-");
                            DOTcirkel.Fill = Brushes.Green; // Groen voor streep
                            await Task.Delay(600);
                            DOTcirkel.Fill = Brushes.White;
                            await Task.Delay(200);
                        }
                        else if (character == '|' && MagDoorDoen)
                        {
                            _port.WriteLine("|");
                            Linecirkel.Fill = Brushes.Red; // Rood voor scheiding
                            await Task.Delay(700);
                            Linecirkel.Fill = Brushes.White;
                            await Task.Delay(400);
                        }
                        else if (character == '?' && MagDoorDoen)
                        {
                            MessageBox.Show("Geen geldig getal/letter."); // Ongeldige karakters
                        }
                    }

                    MagDoorDoen = false;
                    klik++;
                }
                invoerBox.Focus();

                MagDoorDoen = true;
                klik = 0;
            }
            else
            {
                MessageBox.Show("Geen COM poort aangesloten, het programma wordt beëindigd in 5 seconden");
                System.Environment.Exit(1);
            }
        }

        public void ResetOut_Input(string content, bool magOFniet, string txt, bool stuurLichtenUit = false)
        {
            antwoord.Text = content;
            MagDoorDoen = magOFniet;
            invoerBox.Text = txt;
            input = "";
            output = "";
        }

        /// <summary>
        /// Deze methode simuleert de klik van de "Converteer" knop, zonder parameters.
        /// </summary>
        private async Task SimuleerRekenOmButtonClick()
        {
            if (BuzzerChecked)
            {
                _port.WriteLine("BUZZER_UIT");
            }

            if (isProcessing)
            {
                MessageBox.Show("Het proces is al bezig. Wacht tot het is voltooid.");
                return;
            }
            
            isProcessing = true;
            await Morsconverteren(); // Start de conversie
            _port.WriteLine("#");
            Linecirkel.Fill = Brushes.Red; // Rood voor scheiding
            await Task.Delay(1400);
            Linecirkel.Fill = Brushes.White;
            isProcessing = false;
            ResetOut_Input("", false, string.Empty, stuurLichtenUit: false);

        }

       
        private async void EnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuzzerCheckbox.Visibility = Visibility.Hidden;
                await SimuleerRekenOmButtonClick();
                await Task.Delay(250);
                BuzzerCheckbox.Visibility = Visibility.Visible;
            }
        }

       
        private void resetButton_click(object sender, RoutedEventArgs e)
        {
            resetButtonClick++;

            if (_port.IsOpen)
            {
                _port.WriteLine("RESET");
            }

            ResetOut_Input("", false, string.Empty, stuurLichtenUit: true);

            if (!isProcessing)
            {
                resetButtonClick = 0;
            }
        }

  
        private void SimuleerResetButtonClick()
        {
            resetButtonClick++;

            if (_port.IsOpen)
            {
                _port.WriteLine("RESET");
            }

            ResetOut_Input("", false, string.Empty, stuurLichtenUit: true);

            if (!isProcessing)
            {
                resetButtonClick = 0;
            }
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowClosedReset(this, EventArgs.Empty); // Reset poort bij afsluiten
            System.Environment.Exit(0); // Sluit applicatie
        }

        /// <summary>
        /// Converter knop functionaliteit.
        /// </summary>
        private async void ConverterButton_Click(object sender, RoutedEventArgs e)
        {
            BuzzerCheckbox.Visibility = Visibility.Hidden;
            await SimuleerRekenOmButtonClick();
            await Task.Delay(250);
            BuzzerCheckbox.Visibility=Visibility.Visible;
            
        }

        /// <summary>
        /// Methode die de Ctrl+R toetsencombinatie opvangt om de resetknop te simuleren.
        /// </summary>
        private void CTRLr(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.R)
            {
                SimuleerResetButtonClick(); // Simuleer reset knop bij Ctrl+R
            }
        }

        /// <summary>
        /// Escape zodat programma sluit.
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowClosedReset(this, EventArgs.Empty); // Reset de seriële poort
                System.Environment.Exit(0); // Sluit het programma
            }
        }


  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            invoerBox.Focus(); // Geef de focus aan de invoerbox
            
        }
                                
        private void BuzzerCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            BuzzerChecked = true; // Update lokale status
          
        }

        private void BuzzerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            BuzzerChecked = false; // Update lokale status
            
        }


    }
}
