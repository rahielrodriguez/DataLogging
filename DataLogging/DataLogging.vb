Option Strict On
Option Explicit On
Imports System.IO.Ports
Imports System.Threading.Thread
Imports System.Windows.Forms.ComponentModel.Com2Interop
Imports System.Runtime.InteropServices
Public Class DataLogging

    Dim dataQ As New Queue
    Dim xMax% = 1000, yMax% = 255
    Dim sx! = 2, sy! = 2
    Dim sampleCount As Integer

    Function GetBackColor(Optional newBackColor As Color = Nothing) As Color
        Static _backColor As Color
        If newBackColor <> Nothing Then
            _backColor = newBackColor
        End If
        Return _backColor
    End Function
    Function GetForeColor(Optional newForeColor As Color = Nothing) As Color
        Static _foreColor As Color
        If newForeColor <> Nothing Then
            _foreColor = newForeColor
        End If
        Return _foreColor
    End Function

    Sub SetDefaults()
        LogPictureBox.BackColor = GetBackColor(Color.Black)
        GetForeColor(Color.Lime)
        xMax = (SampleRateTrackBar.Value * 30)

        sx = CSng(LogPictureBox.Width / xMax)
        sy = CSng(LogPictureBox.Height / yMax)
    End Sub

    Sub Updategraph()
        Dim plotdata(dataQ.Count - 1) As Integer
        Dim qTrack As Integer = dataQ.Count
        ' LogPictureBox.Refresh()
        dataQ.CopyTo(plotdata, 0)
        Plot(plotdata)

    End Sub
    Sub Plot(plotData() As Integer)
        Dim g As Graphics = LogPictureBox.CreateGraphics
        Dim pen As New Pen(GetForeColor())
        Dim oldX%, oldY%
        Dim y As Integer
        'g.ScaleTransform(CSng(LogPictureBox.Width / Me.xMax), CSng(LogPictureBox.Height / Me.yMax))
        g.ScaleTransform(Me.sx, Me.sy)
        g.TranslateTransform(0, Me.yMax)
        'pen.Width = 2
        'oldY = plotData(0)
        For x = 0 To UBound(plotData) - 1
            y = plotData(x) * -1
            pen.Color = GetBackColor(Color.Black)
            pen.Width = 2
            g.DrawLine(pen, x, 0, x, -LogPictureBox.Height)
            pen.Color = GetForeColor(Color.Lime)
            pen.Width = 1
            g.DrawLine(pen, oldX, oldY, x, y)
            If Math.Abs(oldY - y) > 21 Then
                Console.WriteLine()
            End If
            oldX = x
            oldY = y
        Next

    End Sub
    Sub GetNewData()
        Dim newData As Integer = RandomNumberFrom()
        Dim coinToss As Integer = RandomNumberFrom()
        Static lastData As Integer
        Dim data(1) As Byte

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

        'this keeps the queue limited to size of xMax
        If Me.dataQ.Count > Me.xMax Then
            Me.dataQ.Dequeue()
        End If

    End Sub

    Sub StoreData(prefix As String, data As Byte())
        Dim filename As String = $"log_{DateTime.Now.ToString("yyMMddhh")}.log"

        FileOpen(1, filename, OpenMode.Append)
        Write(1, $"$${prefix}")
        Write(1, data(0))
        Write(1, data(1))
        WriteLine(1, $"{DateTime.Now.ToString("yyMMddhhmmss")}{DateTime.Now.Millisecond}")

        FileClose(1)
        FileStatusLabel.Text = $"{filename}"
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

    'Events below here ---------------------------------------------------------------
    Private Sub LoggingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDefaults()
        For i = 0 To xMax
            GetNewData()
        Next
        Timer1.Interval = CInt(1000 / SampleRateTrackBar.Value)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Me.Close()
    End Sub

    'Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
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

    Private Sub SampleRateTrackBar_Scroll(sender As Object, e As EventArgs) Handles SampleRateTrackBar.Scroll
        Dim plotdata(Me.xMax - 1) As Integer
        Timer1.Stop()
        Timer1.Interval = CInt(1000 / SampleRateTrackBar.Value)
        CurrentSampleRateLabel.Text = $"{SampleRateTrackBar.Value} samples / sec"
        xMax = (SampleRateTrackBar.Value * 30)
        dataQ.Clear()
        For i = 0 To xMax
            Me.dataQ.Enqueue(0)
        Next
        SetDefaults()
        LogPictureBox.Refresh()
        Timer1.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Updategraph()
        Me.Text = $"XMax = {xMax}, Timer = {Me.Timer1.Interval}, Queue = {dataQ.Count}, Samples = {sampleCount}"
        sampleCount = 0
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        GetNewData()
        sampleCount += 1
        'Me.Text = $"XMax = {xMax}, Timer = {Me.Timer1.Interval}, Queue = {dataQ.Count}"
    End Sub

    Private Sub LogPictureBox_Resize(sender As Object, e As EventArgs) Handles LogPictureBox.Resize

        LogPictureBox.Refresh()
    End Sub
End Class
