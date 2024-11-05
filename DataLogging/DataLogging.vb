Option Strict On
Option Explicit On
Imports System.IO.Ports
Imports System.Threading.Thread
Imports System.Windows.Forms.ComponentModel.Com2Interop
Imports System.Runtime.InteropServices
Public Class DataLogging

    Dim dataQ As New Queue
    Sub SetDefaults()
        LogPictureBox.BackColor = Color.Black
        SampleRateTrackBar.Value = 10
        SampleRateTrackBar.Minimum = 1
        SampleRateTrackBar.Maximum = 100
    End Sub

    Sub Updategraph()
        Dim plotdata(100) As Integer
        LogPictureBox.Refresh()
        dataQ.CopyTo(plotdata, 0)
        Plot(plotdata)

    End Sub
    Sub Plot(plotData() As Integer)
        Dim g As Graphics = LogPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)
        Dim oldX%, oldY%
        g.ScaleTransform(CSng(LogPictureBox.Width / (255 + 30)), CSng(LogPictureBox.Height / 255))
        g.TranslateTransform(0, 255)
        oldY = plotData(0)
        For x = 0 To UBound(plotData) - 1
            g.DrawLine(pen, oldX, oldY, x, plotData(x) * -1)
            oldX = x
            oldY = plotData(x) * -1
        Next

    End Sub
    Sub GetNewData()
        Dim newData As Integer
        Dim coinToss As Integer
        Static lastData As Integer
        Dim data(1) As Byte

        Try
            Qy_ReadAnalogInPutA1()

            Sleep(5)
            Console.WriteLine(SerialPort.BytesToRead)
            Dim qyBoardData(SerialPort.BytesToRead) As Byte
            SerialPort.Read(qyBoardData, 0, SerialPort.BytesToRead)

            Console.WriteLine($"High: {Hex(qyBoardData(0))} | Jelly Beans: {qyBoardData(0)}")
            Console.WriteLine($"Low: {Hex(qyBoardData(1))}")

            newData = qyBoardData(0)

            'new data may be posative or negative
            If coinToss >= 5 Then
                newData = newData * -1
            End If

            lastData += newData
            If lastData > 255 Then
                lastData = 255
            ElseIf lastData < 0 Then
                lastData = 0
            End If

            Me.dataQ.Enqueue(lastData)
            data(0) = CByte(lastData)

            StoreData("RND", data)

            'this keeps the queue limited to size of 100
            If Me.dataQ.Count > 100 Then
                Me.dataQ.Dequeue()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Sub StoreData(prefix As String, data As Byte())
        Dim filename As String = $"log_{DateTime.Now.ToString("yyMMddhh")}.log"

        FileOpen(1, filename, OpenMode.Append)
        Write(1, $"$${prefix}")
        Write(1, data(0))
        Write(1, data(1))
        WriteLine(1, $"{DateTime.Now.ToString("yyMMddhhmmss")}{DateTime.Now.Millisecond}")

        FileClose(1)

    End Sub

    ''' <summary>
    ''' returns an integer from min to max inclusive.
    ''' <br/>
    ''' defaults:
    ''' <br/>
    ''' min = 0
    ''' <br/>
    ''' max = 10
    ''' </summary>
    ''' <param name="min%"></param>
    ''' <param name="max%"></param>
    ''' <returns></returns>
    Function RandomNumberFrom(Optional min% = 0, Optional max% = 10) As Integer
        Dim _random%
        Randomize(DateTime.Now.Millisecond)
        _random = CInt(Math.Floor(Rnd() * (max + 1 - min))) + min
        Return _random
    End Function
    Function Qy_ReadAnalogInPutA1() As Byte()
        Try
            Dim data(0) As Byte
            data(0) = &B1010001
            SerialPort.Write(data, 0, 1)
            Return data
        Catch ex As Exception

        End Try

    End Function
    Sub SerialConnect(portName As String)
        SerialPort.Close()
        SerialPort.PortName = portName
        SerialPort.BaudRate = 9600
        SerialPort.Open()
    End Sub
    Sub GetPorts()
        'add all available ports to the port combobox
        PortComboBox.Items.Clear()
        For Each s As String In SerialPort.GetPortNames()
            PortComboBox.Items.Add($"{s}")
        Next

        PortComboBox.SelectedIndex = 0
    End Sub
    Private Sub ComButton_Click(sender As Object, e As EventArgs) Handles ComButton.Click
        GetPorts()
    End Sub
    Private Sub PortComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PortComboBox.SelectedIndexChanged
        SerialConnect($"{PortComboBox.SelectedItem}")
    End Sub

    ' Events below here ---------------------------------------------------------------
    Private Sub LoggingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDefaults()
        For i = 0 To 100
            GetNewData()
        Next
        GetPorts()

    End Sub

    'Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
    '    'filter only for .log files with all files optional
    '    OpenFileDialog.Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
    '    OpenFileDialog.FileName = ""
    '    OpenFileDialog.ShowDialog()
    '    FileStatusLabel.Text = OpenFileDialog.FileName 'TODO log file function
    'End Sub

    'Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
    '    'filter only for .log files with all files optional
    '    SaveFileDialog.Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
    '    SaveFileDialog.FileName = $"data_{DateTime.Now.ToString("yyMMddhh")}.log"
    '    SaveFileDialog.ShowDialog()
    'End Sub


    Private Sub LogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click

        If Timer.Enabled Then
            Timer.Enabled = False
        Else
            Timer.Enabled = True
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        GetNewData()
        Updategraph()

    End Sub
    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Me.Close()
    End Sub

    Private Sub SampleRateTrackBar_Scroll(sender As Object, e As EventArgs) Handles SampleRateTrackBar.Scroll
        Timer.Interval = CInt(1000 / SampleRateTrackBar.Value)
        CurrentSampleRateLabel.Text = $"{SampleRateTrackBar.Value} samples / sec"
    End Sub
End Class
