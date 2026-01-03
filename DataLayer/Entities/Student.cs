namespace DataLayer.Entities;

public record StudentEntity
{
    public int Id { get; init; }
    public byte[] Document { get; set; } = null!;
}

public record Student
{
    public string Name { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
}