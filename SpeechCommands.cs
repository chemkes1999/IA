using System.Speech.Recognition;

public static class SpeechCommands
{
    public static Choices GetChoices()
    {
        return new Choices(
            "hola navi",
            "como estas",
            "como te llamas",
            "dime un chiste",
            "quien es tu creador",
            "cantame",
            "que hora es",
            "apagar la computadora",
            "salir"
        );
    }
}