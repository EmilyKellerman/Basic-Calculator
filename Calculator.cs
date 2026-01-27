namespace CalculatorDomainDemo;

/// <summary>
/// This class represents the DOMAIN BEHAVIOUR.
/// 
/// In real systems:
/// - This is where rules live
/// - This is where decisions are made
/// 
/// In the booking system, this is similar to:
/// - Booking management logic
/// </summary>
public class Calculator
{
    private readonly List<CalculationRequest> _History = new List<CalculationRequest>();

    //Properties
    public IReadOnlyList<CalculationRequest> History
    {
        get
        {
            return _History;
        }
    }

    /// <summary>
    /// This property stores state INSIDE the object.
    /// 
    /// Notice:
    /// - Public getter
    /// - Private setter
    /// 
    /// This means:
    /// - Other code can read the value
    /// - Only the Calculator can change it
    /// 
    /// This protects the object from invalid changes.
    /// </summary>
   
    public int LastResult { get; private set; }

    /// <summary>
    /// Every calculator must have a name.
    /// 
    /// Constructors define what MUST exist
    /// for an object to be valid.
    /// </summary>
    public string Name { get; }

    //Methods
    public Calculator(string name)
    {
        // Guard clause:
        // We do NOT allow invalid objects to exist.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name");

        Name = name;
    }

    /// <summary>
    /// This method applies business rules.
    /// 
    /// It does NOT:
    /// - Read from the console
    /// - Print output
    /// 
    /// This separation is important because:
    /// - Console apps are temporary
    /// - Business logic must survive
    /// 
    /// In the booking system:
    /// - This would decide if a booking is allowed
    /// - This would enforce capacity rules
    /// </summary>
    
    //Methods
    public IReadOnlyList<CalculationRequest> GetCopyofHistory()
    {
        return _History.ToList();
    }
    
    public int Calculate(int a, int b, OperationType operation)
    {
        // Switch expression ensures ALL enum cases are handled
        LastResult = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,

            // This should never happen if enums are used correctly
            _ => throw new InvalidOperationException("Invalid operation")
        };

        CalculationRequest request = new CalculationRequest(a,b,operation);//saves a record of the calculation every time the calculate method is called
        _History.Add(request);

        return LastResult;
    }

    //Get the addition operations were done on this calculator:
    public List<CalculationRequest> ReturnAdditions ( )
    {
        List<CalculationRequest> Additions = new List<CalculationRequest>();
        foreach ( CalculationRequest req in _History )
        {
            if ( req.Operation == OperationType.Add )
            {
                Additions.Add( req );
            }
        }
        return Additions;
    }

    //same, but use linq
    public List<CalculationRequest> ReturnDivide ( )
    {
        if( _History.Any(r => r.B == 0))
        {
            ///under maintenance - trying to avoid long code
        }
        else
        {
            List<CalculationRequest> reqs = _History.
            Where(i => i.Operation == OperationType.Divide).ToList();
        }
        List<CalculationRequest> req = _History.
        Where(i => i.Operation == OperationType.Divide).
        ToList();
        return req;
    }

    //Has Division ever been used?
    public bool WasDivisionsUsed ()
    {
        bool HasDivision = _History.Any(r => r.Operation == OperationType.Divide);//more correct

        List<CalculationRequest> divs = _History.Where(s => s.Operation == OperationType.Divide).ToList();
        bool DivsDone = false;
        if( divs.Count != 0 )
        {
            DivsDone = true;
        }
        else
        {
            DivsDone = false;
        }
        return HasDivision;
    }

    //Writing Custom Exceptions
    public double Divide( CalculationRequest request )
    {
        if( request.B == 0 )
        {
            throw new InvalidOperationException("Division cannot be done because the denominator equals zero.");
        }
        else
        {
            return request.A/request.B;
        }
    }

    //Group our calculation requests
    public Dictionary<OperationType, List<CalculationRequest>> Grouped()
    {
        Dictionary<OperationType, List<CalculationRequest>> Groupings = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach ( CalculationRequest req in _History )
        {
            if(!Groupings.ContainsKey(req.Operation))
            {
                Groupings[req.Operation] = new List<CalculationRequest>();;

            }
            Groupings[req.Operation].Add(req);
        }

        return Groupings;
    }

}
