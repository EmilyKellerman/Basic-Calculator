using System.Xml;

public class CCalculator
{
    //Properties
    public int Result { get; set; }

    //Enums
    public enum Operations
    {
        Add, Subtract, Multiply, Divide
    }

    //Fields
    private string sName;

    public string Name
    {
        get
        {
            if ( string.IsNullOrWhiteSpace(sName))
            {
                Name = "Default Calculator";
            }
            else
            {
                Name = sName;
            }
            return sName;
        }
        set
        {
            sName = value;
        }
    }

    public CCalculator()
    {
        Name = "default calculator";
    }//default ctor

    public CCalculator ( string Name )
    {
        this.Name = Name;
    }//ctor

    public string Add ( int a, int b )
    {
        int output = a + b ;
        return ( a.ToString() + " + " + b.ToString() + " = " + output.ToString()) ;
    }

    public string Subtract ( int a, int b )
    {
        int output = a - b ;
        return ( a.ToString() + " - " + b.ToString() + " = " + output.ToString()) ;
    }

    public string Multiply ( int a, int b )
    {
        int output = a * b ;
        return ( a.ToString() + " * " + b.ToString() + " = " + output.ToString()) ;
    }

    public string Divide ( int a, int b )
    {
        int output = a - b ;
        if (b == 0)
        {
            return "Denominator cannot be zero";
        }
        else
        {
            return ( a.ToString() + " / " + b.ToString() + " = " + output.ToString()) ;
        }
    }
}