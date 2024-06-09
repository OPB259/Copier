using System;
using NUnit.Framework;

[TestFixture]
public class UnitTestCopier
{
    [Test]
    public void TestPowerOn()
    {
        var copier = new Copier();
        copier.PowerOn();
        Assert.AreEqual(IDevice.State.on, copier.GetState());
        Assert.AreEqual(1, copier.Counter);
    }

    [Test]
    public void TestPowerOff()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.PowerOff();
        Assert.AreEqual(IDevice.State.off, copier.GetState());
    }

    [Test]
    public void TestPrint()
    {
        var copier = new Copier();
        copier.PowerOn();
        var doc = new PDFDocument("test.pdf");
        copier.Print(in doc);
        Assert.AreEqual(1, copier.PrintCounter);
    }

    [Test]
    public void TestScan()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.Scan(out IDocument doc);
        Assert.AreEqual(1, copier.ScanCounter);
        Assert.IsNotNull(doc);
        Assert.AreEqual("ImageScan1.jpg", doc.GetFileName());
    }

    [Test]
    public void TestScanAndPrint()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.ScanAndPrint();
        Assert.AreEqual(1, copier.PrintCounter);
        Assert.AreEqual(1, copier.ScanCounter);
    }
}