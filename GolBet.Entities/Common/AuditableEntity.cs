using System.ComponentModel.DataAnnotations.Schema;

namespace GolBet.Entities.Common;

public abstract class AuditableEntity
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    // Compatibility properties for existing code:
    [NotMapped]
    public DateTime CreatedAt
    {
        get => CreatedDate;
        set => CreatedDate = value;
    }

    [NotMapped]
    public DateTime? UpdatedAt
    {
        get => ModifiedDate;
        set => ModifiedDate = value;
    }
}
