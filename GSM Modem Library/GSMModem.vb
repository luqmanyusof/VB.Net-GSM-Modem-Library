Imports System.IO.Ports
Public Class GSMModem
    Private theModem As New SerialPort
    Public Sub New(ByVal portName As String, ByVal baud As Integer)
        theModem.PortName = portName
        theModem.BaudRate = baud
    End Sub
End Class
