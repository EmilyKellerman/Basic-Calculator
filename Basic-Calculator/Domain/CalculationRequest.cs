namespace Basic_Calculator;
public record CalculationRequest(
    double left,
    double right,
    OperationType Operation
);

