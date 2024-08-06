



namespace SectionProject;

public interface IEcologicalFactor
{
    string EcologicalType { get; set; }
    int MinValue { get; set; }
    int MaxValue { get; set; }
    decimal CurrentValue { get; set; }
    DateTime DateTime { get; set; }
    bool ValueRange(decimal value);
}

public class EcologicalFactor : IEcologicalFactor
{
    public EcologicalFactor()
    {
        this._invalidEvent += EcologicalFactor_InvalidMinValue;
    }

    protected void EcologicalFactor_InvalidMinValue(object? sender, InvalidEventArgs e)
    {
        Console.WriteLine("Zu geringer MinValue " + e.Error + " " + e.InvalidValue + " " + e.EventTime);
    }

    public event EventHandler<InvalidEventArgs>? _invalidEvent;
    protected void OnInvalidEvent(InvalidEventArgs e)
    {
        if(_invalidEvent != null) _invalidEvent(this, e);
        else throw e.Error;
    }

    public virtual string EcologicalType { get; set; } = string.Empty;
    
    private int _minValue;
    public virtual int MinValue
    {
        get { return _minValue; }
        set {
            if(value <= 10)
            {
                OnInvalidEvent(FactoryContainer.EventArgsFactory.GetInvalidEventArgs(new Exception(), value, DateTime.Now));
            }
            else _minValue = value;
        }
    }
    public virtual int MaxValue { get; set; }
    private decimal _currentValue;

    public decimal CurrentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = value;
        }
    }
    public DateTime DateTime { get; set; }
    
    public bool ValueRange(decimal value)
    {
        if (value >= this.MinValue && value <= this.MaxValue)
            return true;
        return false;
    }
}

public class Pressure : EcologicalFactor
{
    public override string EcologicalType { get; set; } = "Pressure";
    public override int MinValue { get { return base.MinValue; } }
    public override int MaxValue { get; set; }
}

public class Pollution : EcologicalFactor
{
    public Pollution()
    {
        base._invalidEvent += base.EcologicalFactor_InvalidMinValue;
    }

    public override string EcologicalType { get; set; } = "Pollution";
    // public override int MinValue { get; set; }
    public override int MaxValue { get; set; }
}


//  ------------------- Event
//  Besser von EventArgs anstelle von Exception ableiten, da Ereignisse normalerweise nicht als Ausnahmen behandelt werden!

public class InvalidEventArgs : Exception
{
    private int _invalidValue;
    public int InvalidValue
    {
        get { return _invalidValue; }
    }

    private Exception _error;
    public Exception Error
    {
        get { return _error; }
    }

    private DateTime _eventTime;
    public DateTime EventTime
    {
        get { return _eventTime; }
    }

    public InvalidEventArgs(Exception error, int invalidValue, DateTime dateTime)
    {
        _error = error;
        _invalidValue = invalidValue;
        _eventTime = dateTime;
    }
}

