using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;
internal class Program
{
    private static void Main(string[] args)
    {
        var synthesizer = new SpeechSynthesizer();

        using (var recognizer = new SpeechRecognitionEngine())
        {
            var choices = SpeechCommands.GetChoices(); // Use the method from the SpeechCommands class

            var grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(choices);

            var grammar = new Grammar(grammarBuilder);

            recognizer.LoadGrammar(grammar);
            bool exitProgram = false; // Initialize a flag to control program exit

            recognizer.SpeechRecognized +=  (s, e) =>
            {
                // Check the confidence level to filter out low-confidence recognitions
                if (e.Result.Confidence >= 0.7f)
                {
                    var question = e.Result.Text;

                    switch (question)
                    {
                        case "hola navi":
                            synthesizer.Speak("Hola! Cómo puedo ayudarte?");
                            break;
                        case "como estas":
                            synthesizer.Speak("Solo soy una inteligencia artificial, pero me siento bien.");
                            break;
                        case "como te llamas":
                            synthesizer.Speak(
                                "Yo soy una inteligencia artificial, soy tu asistente. Puedes llamarme navi.");
                            break;
                        case "dime un chiste":
                            synthesizer.Speak("Qué hace un perro con un taladro?. Taladrando.");
                            break;
                        case "quien es tu creador":
                            synthesizer.Speak("Mi creador se llama Carlos Hemkes.");
                            break;
                        case "cantame":
                            synthesizer.Speak(
                                "De la sierra morena, Cielito lindo, vienen bajando, Un par de ojitos negros, Cielito lindo, de contrabando. Ay, ay, ay, ay, Canta y no llores, Porque cantando se alegran, Cielito lindo, los corazones.");
                            break;
                        case "que hora es":
                            var horaActual = DateTime.Now.ToString("hh:mm tt");
                            synthesizer.Speak("La hora actual es " + horaActual);
                            break;
                        case "apagar la computadora":
                            synthesizer.Speak("Apagando la computadora en 5 segundos.");
                            System.Diagnostics.Process.Start("shutdown", "/s /t 10"); // Initiates shutdown in 10 seconds
                            break;
                        case "salir": // Add a new case for the exit command
                            exitProgram = true;
                            break;
                    }
                }
            };

            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            Console.WriteLine("Escuchando... Dí 'salir' para terminar.");

            // Continue running the loop until the user says "salir"
            while (!exitProgram)
            {
                // You can add any other background tasks or conditions here if needed
                // For example, you can perform other actions while waiting for the user's command
            }
        }
        
    }
    
}