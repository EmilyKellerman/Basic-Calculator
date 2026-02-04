using System.ComponentModel.DataAnnotations;
using Basic_Calculator;
using Basic_Calculator.Domain;

public class CreateCalculationDto
{
    [Required]
    public double left { get; set; }

    [Required]
    public double right { get; set; }

    [Required]
    public OperationType operand { get; set; }

}