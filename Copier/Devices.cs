using System;

public interface IDevice
{
    enum State { on, off };
    State GetState();
    void PowerOn();
    void PowerOff();
    int Counter { get; }
}

public interface IPrinter : IDevice
{
    void Print(in IDocument document);
}

public interface IScanner : IDevice
{
    void Scan(out IDocument document);
}

public abstract class BaseDevice : IDevice
{
    private IDevice.State state = IDevice.State.off;
    public IDevice.State GetState() => state;
    public void PowerOn()
    {
        state = IDevice.State.on;
        Counter++;
    }
    public void PowerOff() => state = IDevice.State.off;
    public int Counter { get; private set; } = 0;
}

public class Copier : BaseDevice, IPrinter, IScanner
{
    public int PrintCounter { get; private set; } = 0;
    public int ScanCounter { get; private set; } = 0;

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        PrintCounter++;
        Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
    }

    public void Print(in PDFDocument pdfDocument)
    {
        throw new NotImplementedException();
    }

    public void Scan(out IDocument document)
    {
        document = new ImageDocument($"ImageScan{ScanCounter + 1}.jpg");
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        ScanCounter++;
        Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
    }

    public void ScanAndPrint()
    {
        Scan(out IDocument document);
        if (document != null)
        {
            Print(in document);
        }
    }
}