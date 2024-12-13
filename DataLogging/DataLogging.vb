'Rahiel Rodriguez
'RCET 3371
'Fall 2024
'Data Logger
'https://github.com/rahielrodriguez/DataLogging.git

Option Strict On
Option Explicit On
Imports System.IO.Ports
Imports System.Threading.Thread
Imports System.Windows.Forms.ComponentModel.Com2Interop
Imports System.Runtime.InteropServices
Imports System.Runtime.Remoting.Lifetime

Public Class DataLogging

    ' Queue to hold incoming data
    Dim dataQ As New Queue
    Dim xMax% = 1000, yMax% = 255 ' Maximum values for x (time) and y (data) axes
    Dim sx! = 2, sy! = 2 ' Scaling factors for plotting
    Dim sampleCount As Integer ' Counter for the number of samples
    Dim fileDataList As New List(Of String) ' List to store data read from a file

    ' Connects to a serial port with the specified name
    Sub SerialConnect(portName As String)
        SerialPort.Close()
        SerialPort.PortName = portName
        SerialPort.BaudRate = 9600
        SerialPort.Open()
    End Sub

    ' Populates the PortComboBox with available COM ports
    Sub GetPorts()
        PortComboBox.Items.Clear()
        For Each s As String In SerialPort.GetPortNames()
            PortComboBox.Items.Add($"{s}")
        Next
        PortComboBox.SelectedIndex = 0
    End Sub

    ' Sends a command to read analog input A1 from the QY@ board
    Sub Qy_ReadAnalogInPutA1()
        Dim data(0) As Byte
        data(0) = &B1010001 ' Command byte
        SerialPort.Write(data, 0, 1)
    End Sub

    ' Gets or sets the background color for the LogPictureBox
    Function GetBackColor(Optional newBackColor As Color = Nothing) As Color
        Static _backColor As Color
        If newBackColor <> Nothing Then
            _backColor = newBackColor
        End If
        Return _backColor
    End Function

    ' Gets or sets the foreground color for the graph lines
    Function GetForeColor(Optional newForeColor As Color = Nothing) As Color
        Static _foreColor As Color
        If newForeColor <> Nothing Then
            _foreColor = newForeColor
        End If
        Return _foreColor
    End Function

    ' Initializes default settings for the graph and controls
    Sub SetDefaults()
        LogPictureBox.BackColor = GetBackColor(Color.Black)
        GetForeColor(Color.Lime)

        If RadioButton30Seconds.Checked Then
            xMax = (SampleRateTrackBar.Value * 30)
        ElseIf AllDataRadioButton.Checked Then
            xMax = 36000
        Else
            xMax = (SampleRateTrackBar.Value * 30)
        End If

        sx = CSng(LogPictureBox.Width / xMax) ' Scaling x-axis
        sy = CSng(LogPictureBox.Height / yMax) ' Scaling y-axis
    End Sub

    ' Updates the graph by plotting the data in the queue
    Sub Updategraph()
        Dim plotdata(dataQ.Count - 1) As Integer
        Dim qTrack As Integer = dataQ.Count
        dataQ.CopyTo(plotdata, 0)
        Plot(plotdata)
    End Sub

    ' Plots the given data array on the graph
    Sub Plot(plotData() As Integer)
        Dim g As Graphics = LogPictureBox.CreateGraphics
        Dim pen As New Pen(GetForeColor())
        Dim oldX%, oldY%

        g.ScaleTransform(Me.sx, Me.sy)
        g.TranslateTransform(0, Me.yMax)
        oldY = plotData(0)

        For x = 0 To UBound(plotData) - 1
            pen.Color = GetBackColor()
            pen.Width = 1.25
            pen.Alignment = Drawing2D.PenAlignment.Outset
            g.DrawLine(pen, x, 0, x, -Me.yMax) ' Clears previous line

            pen.Color = GetForeColor()
            pen.Width = 1
            pen.Alignment = Drawing2D.PenAlignment.Inset
            g.DrawLine(pen, oldX, oldY, x, plotData(x) * -1) ' Draws new line

            oldX = x
            oldY = plotData(x) * -1
        Next
    End Sub

    ' Reads new data from either the serial port or a file
    Sub GetNewData()
        Dim newData As Integer
        Dim data(1) As Byte
        Dim temp() As String
        Dim fileData As String

        If RadioButton30Seconds.Checked Then
            Qy_ReadAnalogInPutA1()
            Sleep(5)
            Dim serialData(SerialPort.BytesToRead) As Byte
            SerialPort.Read(serialData, 0, SerialPort.BytesToRead)

            newData = CInt(serialData(0))
            Me.dataQ.Enqueue(newData)
            data(0) = CByte(newData)
        ElseIf AllDataRadioButton.Checked Then
            Try
                FileOpen(1, $"{FileStatusLabel.Text}", OpenMode.Input)
                Do Until EOF(1)
                    fileData = LineInput(1)
                    Me.fileDataList.Add($"{fileData}")
                Loop
                For Each item In fileDataList
                    temp = Split(fileData, ",")
                    newData = CInt(temp(1))
                    Me.dataQ.Enqueue(newData)
                    data(0) = CByte(newData)
                Next
                FileClose()
            Catch ex As Exception
                newData = 0
                Me.Text = "Please Select a .log file to read"
            End Try
        End If

        If RadioButton30Seconds.Checked Then
            StoreData("AN1", data)
        End If

        ' Limits the size of the queue to xMax
        If Me.dataQ.Count > Me.xMax Then
            Me.dataQ.Dequeue()
        End If
    End Sub

    ' Stores data in a log file with a timestamp
    Sub StoreData(prefix As String, data As Byte())
        Dim filename As String = $"..\..\..\log_{DateTime.Now.ToString("yyMMddhh")}.log"

        FileOpen(1, filename, OpenMode.Append)
        Write(1, $"$${prefix}")
        Write(1, data(0))
        Write(1, data(1))
        WriteLine(1, $"{DateTime.Now.ToString("yyMMddhhmmss")}{DateTime.Now.Millisecond}")
        FileClose(1)
    End Sub

    ' Event triggered when the form loads
    Private Sub LoggingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetPorts()
        For i = 0 To xMax
            GetNewData()
        Next
        SetDefaults()
        Timer1.Interval = CInt(1000 / SampleRateTrackBar.Value)
        FileStatusLabel.Text = ""
    End Sub

    ' Event triggered to close the application
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Me.Close()
    End Sub

    ' Event triggered to open a log file
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenFileDialog.Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
        OpenFileDialog.FileName = ""
        OpenFileDialog.ShowDialog()
        FileStatusLabel.Text = OpenFileDialog.FileName
    End Sub

    ' Event triggered to start or stop logging
    Private Sub LogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click
        If Timer1.Enabled Then
            Timer1.Enabled = False
            Timer2.Enabled = False
        Else
            Timer1.Enabled = True
            Timer2.Enabled = True
            Timer1.Start()
            Timer2.Start()
        End If
    End Sub

    ' Event triggered when the sample rate track bar is adjusted
    Private Sub SampleRateTrackBar_Scroll(sender As Object, e As EventArgs) Handles SampleRateTrackBar.Scroll
        Timer1.Stop()
        Timer1.Interval = CInt(1000 / SampleRateTrackBar.Value)
        CurrentSampleRateLabel.Text = $"{SampleRateTrackBar.Value} samples / sec"

        If RadioButton30Seconds.Checked Then
            xMax = (SampleRateTrackBar.Value * 30)
        ElseIf AllDataRadioButton.Checked Then
            xMax = 36000
        End If

        dataQ.Clear()
        For i = 0 To xMax
            Me.dataQ.Enqueue(0)
        Next
        SetDefaults()
        LogPictureBox.Refresh()
        Timer1.Start()
    End Sub

    ' Event triggered to update the graph on Timer2 tick
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Updategraph()
        Me.Text = $"XMax = {xMax}, Timer = {Me.Timer1.Interval}, Queue = {dataQ.Count}, Samples = {sampleCount}"
        sampleCount = 0
    End Sub

    ' Event triggered to refresh the COM port list
    Private Sub ComButton_Click(sender As Object, e As EventArgs) Handles ComButton.Click
        GetPorts()
    End Sub

    ' Event triggered when a COM port is selected
    Private Sub PortComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PortComboBox.SelectedIndexChanged
        SerialConnect($"{PortComboBox.SelectedItem}")
    End Sub

    ' Event triggered when "All Data" mode is selected
    Private Sub AllDataRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AllDataRadioButton.CheckedChanged
        dataQ.Clear()
    End Sub

    ' Event triggered when "30 Seconds" mode is selected
    Private Sub RadioButton30Seconds_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton30Seconds.CheckedChanged
        dataQ.Clear()
    End Sub

    ' Event triggered to read new data on Timer1 tick
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        GetNewData()
        sampleCount += 1
    End Sub

    ' Event triggered when the LogPictureBox is resized
    Private Sub LogPictureBox_Resize(sender As Object, e As EventArgs) Handles LogPictureBox.Resize
        LogPictureBox.Refresh()
    End Sub

End Class
