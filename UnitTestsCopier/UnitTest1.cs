using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTestCopier
{
    [TestMethod]
    public void TestPowerOn()
    {
        var copier = new Copier();
        copier.PowerOn();
        Assert.AreEqual(IDevice.State.on, copier.GetState());
        Assert.AreEqual(1, copier.Counter);
    }

    [TestMethod]
    public void TestPowerOff()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.PowerOff();
        Assert.AreEqual(IDevice.State.off, copier.GetState());
    }

    [TestMethod]
    public void TestPrint()
    {
        var copier = new Copier();
        copier.PowerOn();
        var pdfDocument = new PDFDocument("test.pdf");
        copier.Print(in pdfDocument);
        Assert.AreEqual(1, copier.PrintCounter);
    }

    [TestMethod]
    public void TestScan()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.Scan(out IDocument scannedDocument);
        Assert.AreEqual(1, copier.ScanCounter);
        Assert.IsNotNull(scannedDocument);
        Assert.AreEqual("ImageScan1.jpg", scannedDocument.GetFileName());
    }

    [TestMethod]
    public void TestScanAndPrint()
    {
        var copier = new Copier();
        copier.PowerOn();
        copier.ScanAndPrint();
        Assert.AreEqual(1, copier.PrintCounter);
        Assert.AreEqual(1, copier.ScanCounter);
    }
}