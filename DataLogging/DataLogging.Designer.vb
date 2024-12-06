<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataLogging
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LogButton = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComButton = New System.Windows.Forms.Button()
        Me.LogPictureBox = New System.Windows.Forms.PictureBox()
        Me.SerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.SampleRateLabel = New System.Windows.Forms.Label()
        Me.SampleRateTrackBar = New System.Windows.Forms.TrackBar()
        Me.MinSampleLabel = New System.Windows.Forms.Label()
        Me.MaxSampleLabel = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileStatusLabel = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CurrentSampleRateLabel = New System.Windows.Forms.Label()
        Me.PortComboBox = New System.Windows.Forms.ComboBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.LogPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SampleRateTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'LogButton
        '
        Me.LogButton.Location = New System.Drawing.Point(6, 21)
        Me.LogButton.Name = "LogButton"
        Me.LogButton.Size = New System.Drawing.Size(140, 72)
        Me.LogButton.TabIndex = 2
        Me.LogButton.Text = "Log"
        Me.LogButton.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.StopButton.Location = New System.Drawing.Point(152, 21)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(140, 72)
        Me.StopButton.TabIndex = 3
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(298, 21)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(140, 72)
        Me.SaveButton.TabIndex = 4
        Me.SaveButton.Text = "&Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComButton)
        Me.GroupBox1.Controls.Add(Me.SaveButton)
        Me.GroupBox1.Controls.Add(Me.LogButton)
        Me.GroupBox1.Controls.Add(Me.StopButton)
        Me.GroupBox1.Location = New System.Drawing.Point(351, 359)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(591, 110)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'ComButton
        '
        Me.ComButton.Location = New System.Drawing.Point(444, 21)
        Me.ComButton.Name = "ComButton"
        Me.ComButton.Size = New System.Drawing.Size(140, 72)
        Me.ComButton.TabIndex = 5
        Me.ComButton.Text = "&Com"
        Me.ComButton.UseVisualStyleBackColor = True
        '
        'LogPictureBox
        '
        Me.LogPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LogPictureBox.Location = New System.Drawing.Point(0, 28)
        Me.LogPictureBox.Name = "LogPictureBox"
        Me.LogPictureBox.Size = New System.Drawing.Size(942, 288)
        Me.LogPictureBox.TabIndex = 6
        Me.LogPictureBox.TabStop = False
        '
        'SampleRateLabel
        '
        Me.SampleRateLabel.AutoSize = True
        Me.SampleRateLabel.Location = New System.Drawing.Point(116, 18)
        Me.SampleRateLabel.Name = "SampleRateLabel"
        Me.SampleRateLabel.Size = New System.Drawing.Size(86, 16)
        Me.SampleRateLabel.TabIndex = 8
        Me.SampleRateLabel.Text = "Sample Rate"
        '
        'SampleRateTrackBar
        '
        Me.SampleRateTrackBar.Location = New System.Drawing.Point(6, 37)
        Me.SampleRateTrackBar.Minimum = 1
        Me.SampleRateTrackBar.Name = "SampleRateTrackBar"
        Me.SampleRateTrackBar.Size = New System.Drawing.Size(319, 56)
        Me.SampleRateTrackBar.TabIndex = 9
        Me.SampleRateTrackBar.Value = 10
        '
        'MinSampleLabel
        '
        Me.MinSampleLabel.AutoSize = True
        Me.MinSampleLabel.Location = New System.Drawing.Point(3, 18)
        Me.MinSampleLabel.Name = "MinSampleLabel"
        Me.MinSampleLabel.Size = New System.Drawing.Size(73, 16)
        Me.MinSampleLabel.TabIndex = 10
        Me.MinSampleLabel.Text = "1 sample/s"
        '
        'MaxSampleLabel
        '
        Me.MaxSampleLabel.AutoSize = True
        Me.MaxSampleLabel.Location = New System.Drawing.Point(231, 18)
        Me.MaxSampleLabel.Name = "MaxSampleLabel"
        Me.MaxSampleLabel.Size = New System.Drawing.Size(87, 16)
        Me.MaxSampleLabel.TabIndex = 11
        Me.MaxSampleLabel.Text = "10 samples/s"
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(942, 28)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.OpenFileToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(155, 26)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(155, 26)
        Me.OpenFileToolStripMenuItem.Text = "&Open File"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(76, 24)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'FileStatusLabel
        '
        Me.FileStatusLabel.AutoEllipsis = True
        Me.FileStatusLabel.AutoSize = True
        Me.FileStatusLabel.Location = New System.Drawing.Point(12, 449)
        Me.FileStatusLabel.Name = "FileStatusLabel"
        Me.FileStatusLabel.Size = New System.Drawing.Size(48, 16)
        Me.FileStatusLabel.TabIndex = 13
        Me.FileStatusLabel.Text = "Label1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CurrentSampleRateLabel)
        Me.GroupBox2.Controls.Add(Me.MaxSampleLabel)
        Me.GroupBox2.Controls.Add(Me.MinSampleLabel)
        Me.GroupBox2.Controls.Add(Me.SampleRateTrackBar)
        Me.GroupBox2.Controls.Add(Me.SampleRateLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 322)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(333, 110)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        '
        'CurrentSampleRateLabel
        '
        Me.CurrentSampleRateLabel.AutoSize = True
        Me.CurrentSampleRateLabel.Location = New System.Drawing.Point(99, 77)
        Me.CurrentSampleRateLabel.Name = "CurrentSampleRateLabel"
        Me.CurrentSampleRateLabel.Size = New System.Drawing.Size(131, 16)
        Me.CurrentSampleRateLabel.TabIndex = 12
        Me.CurrentSampleRateLabel.Text = "Current Sample Rate"
        '
        'PortComboBox
        '
        Me.PortComboBox.FormattingEnabled = True
        Me.PortComboBox.Location = New System.Drawing.Point(351, 329)
        Me.PortComboBox.Name = "PortComboBox"
        Me.PortComboBox.Size = New System.Drawing.Size(292, 24)
        Me.PortComboBox.TabIndex = 15
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'DataLogging
        '
        Me.AcceptButton = Me.LogButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.StopButton
        Me.ClientSize = New System.Drawing.Size(942, 474)
        Me.Controls.Add(Me.PortComboBox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.FileStatusLabel)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LogPictureBox)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "DataLogging"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Logger"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.LogPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SampleRateTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents LogButton As Button
    Friend WithEvents StopButton As Button
    Friend WithEvents SaveButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LogPictureBox As PictureBox
    Friend WithEvents SerialPort As IO.Ports.SerialPort
    Friend WithEvents SampleRateLabel As Label
    Friend WithEvents SampleRateTrackBar As TrackBar
    Friend WithEvents MinSampleLabel As Label
    Friend WithEvents MaxSampleLabel As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileStatusLabel As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CurrentSampleRateLabel As Label
    Friend WithEvents ComButton As Button
    Friend WithEvents PortComboBox As ComboBox
    Friend WithEvents Timer2 As Timer
End Class
