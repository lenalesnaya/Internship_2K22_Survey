using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.DomainModel
{
    public class Counter
    {
        [Key]
        public int Value { get; set; }
    }
}