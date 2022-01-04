using ItechArt.Survey.DomainModel.Abstractions;

namespace ItechArt.Survey.DomainModel;

public class Counter : BaseEntity
    {
        [Key]
        public int Value { get; set; }
    }