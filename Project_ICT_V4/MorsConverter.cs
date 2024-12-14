using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace Project_ICT_V3
{
    internal class MorsConverter
    {
        // Gecombineerde Morse-code mapping
        private static readonly Dictionary<char, string> MorseCodeMapping;

        // Statische constructor om de dictionary te initialiseren
        static MorsConverter()
        {
            MorseCodeMapping = new Dictionary<char, string>();

            // Voeg Engelse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'A', ".-"     }, { 'B', "-..."   }, { 'C', "-.-."   }, { 'D', "-.."    }, { 'E', "."      },
                { 'F', "..-."   }, { 'G', "--."    }, { 'H', "...."   }, { 'I', ".."     }, { 'J', ".---"   },
                { 'K', "-.-"    }, { 'L', ".-.."   }, { 'M', "--"     }, { 'N', "-."     }, { 'O', "---"    },
                { 'P', ".--."   }, { 'Q', "--.-"   }, { 'R', ".-."    }, { 'S', "..."    }, { 'T', "-"      },
                { 'U', "..-"    }, { 'V', "...-"   }, { 'W', ".--"    }, { 'X', "-..-"   }, { 'Y', "-.--"   },
                { 'Z', "--.."   }, { '1', ".----"  }, { '2', "..---"  }, { '3', "...--"  }, { '4', "....-"  },
                { '5', "....."  }, { '6', "-...."  }, { '7', "--..."  }, { '8', "---.."  }, { '9', "----."  },
                { '0', "-----"  }, { ' ', "/"      }, { '.', ".-.-.-" }, { ',', "--..--" }, { '?', "..--.." }, 
                { '!', "-.-.--" }, { ':', "---..." }, { ';', "-.-.-." }, { '-', "-....-" }, { '"', ".-..-." },
                { '(', "-.--."  }, { ')', "-.--.-" }, { '@', ".--.-." }, { '=', "-...-"  }
            });


            //Voeg Russische tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'Б', "-..."   }, { 'В', ".--"  }, { 'Г', "--."    }, { 'Д', "-.."   }, { 'Е', "."     },
                { 'Ё', "."      }, { 'Ж', "...-" }, { 'З', "--.."   }, { 'И', ".."    }, { 'Й', ".---"  }, 
                { 'К', "-.-"    }, { 'Л', ".-.." }, { 'М', "--"     }, { 'Н', "-."    }, { 'П', ".--."  },
                { 'Р', ".-."    }, { 'С', "..."  }, { 'Т', "-"      }, { 'У', "..-"   }, { 'Ф', "..-."  },
                { 'Х', "...."   }, { 'Ц', "-.-." }, { 'Ч', "---."   }, { 'Ш', "----"  }, { 'Щ', "--.-"  }, 
                { 'Ъ', ".--.-." }, { 'Ы', "-.--" }, { 'Ь', "-..-"   }, { 'Э', "..-.." }, { 'Ю', "..--"  }, 
                { 'Я', ".-.-"   }
            });
            // Voeg Franse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'À', ".--.-" }, { 'Â', ".-.."  }, { 'Ç', "-.-.." }, { 'È', ".-.." },
                { 'É', "..-.." }, { 'Ê', "-..."  }, { 'Ë', "....." }, { 'Ô', "---." },
                { 'Ù', "..--"  }, { 'Û', "..-.." }, { 'Ü', "..--"  }
            });

            // Voeg Spaanse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'Á', ".--.-" }, { 'Í', ".."   }, { 'Ñ', "--.--" },
                { 'Ó', "---."  }, { 'Ú', "..--" }, { 'Ü', "..--"  }
            });

            // Voeg Duitse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'Ä', ".-.-" }, { 'Ö', "---." }, { 'Ü', "..--" }, { 'ß', "..." }
            });

            // Voeg Italiaanse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'è', ".-.."  }, { 'é', "..-.." }, 
                { 'ì', ".."    }, { 'ù', "..--"  }
            });

            // Voeg Turkse tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { 'ç', "-.-.." }, { 'ı', ".."   }, { 'ğ', "--." }, 
                { 'ş', "..."   }, { 'ü', "..--" }, { 'ö', "---."},
            });

            // Voeg Chinese tekens toe
            AddToMorseCodeMapping(new Dictionary<char, string>
            {
                { '你', "-.--." }, { '好', ".-.." }, { '是', "...-." }, { '我', "--.." }, { '的', "-.-"  },
                { '不', "..-.." }, { '了', ".-.-" }, { '在', "--.-"  }, { '人', ".-.." }, { '有', "---"  },
                { '这', "--.."  }, { '中', "-.."  }, { '大', "-..."  }, { '来', ".--." }, { '上', "..."  },
                { '国', "-.-."  }, { '个', "--.." }, { '说', "-.."   }, { '们', "-.--" }, { '到', ".-.." }
            });
        }

        // Methode om unieke tekens aan de Morse-code mapping toe te voegen.
        // Controleert of een teken al bestaat om duplicaten te voorkomen.
        private static void AddToMorseCodeMapping(Dictionary<char, string> newMappings)
        {
            foreach (var alf in newMappings)
            {
                if (!MorseCodeMapping.ContainsKey(alf.Key))
                {
                    MorseCodeMapping.Add(alf.Key, alf.Value);
                }
            }
        }

        public static string GetMorseCode(char letter)
        {
            char Hoofdletter_letter = char.ToUpper(letter);

            if (MorseCodeMapping.TryGetValue(Hoofdletter_letter, out var morseCode))
            {
                return morseCode;
            }

            return $"Ongeldig teken: {letter}";
        }

        public string CheckOutput(string input)
        {
            if (input.Length > 25)
            {
                MessageBox.Show("Te veel, of ongeldige gegevens");

                return "Te veel gegevens";

            }

            if (string.IsNullOrWhiteSpace(input))
            {
                return "Lege invoer of geen tekst opgegeven.";
            }

            StringBuilder result = new StringBuilder();

            foreach (char letter in input)
            {
                result.Append(GetMorseCode(letter) + " | ");
            }

            return result.ToString().TrimEnd(' ', '|');
        }
    }
}
