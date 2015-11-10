Imports System.IO.Ports
Enum responseKey
    OK = 1
    writeSMS = 2
End Enum
Public Class GSMModem
    Private theModem As New SerialPort
    Public ModemResponseData As String
    Public SmsCenterNumber As String
    Public SmsReceiverNumber As String
    Private giveResponse As Boolean = False
    Private responseType As Integer

    Public Sub New(ByVal portName As String, ByVal baud As Integer)
        theModem.PortName = portName
        theModem.BaudRate = baud
    End Sub

    Public Sub StartCommunication()
        Try
            theModem.Open()
            AddHandler theModem.DataReceived, AddressOf ModemResponse
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub CustomCommand(ByVal theCommand As String)
        theModem.Write(theCommand)
        theModem.Write(vbCr)
    End Sub

    Public Sub PingModem()
        giveResponse = True
        responseType = responseKey.OK
        theModem.Write("AT")
        theModem.Write(vbCr)
    End Sub

    Private Sub ModemResponse()
        Dim rxData(theModem.BytesToRead) As Byte
        ModemResponseData = ""
        theModem.Read(rxData, 0, theModem.BytesToRead)
        ModemResponseData = System.Text.ASCIIEncoding.ASCII.GetString(rxData)
        If giveResponse = True Then
            giveResponse = False
            If responseType = responseKey.OK Then
                MsgBox(ModemResponseData, "Response from modem")
            End If
        End If
    End Sub

    Private Sub SendSms(ByVal sms_to_send As String)
        theModem.Write("AT+CMGS")
        theModem.Write(vbCr)
    End Sub
End Class