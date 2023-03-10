using System.ComponentModel.DataAnnotations;


namespace TicTacToe.models
{
    public class DataGame
    {
        [Key]
        public string? idGame { get; set; } 
        public string? nameGame { get; set; }
        public sbyte idPlayer { get; set; }
        public sbyte idPlayerNext { get; set; }
        public sbyte idPlayerWin { get; set; }
        public sbyte[]? data { get; set; } 
    }
}
