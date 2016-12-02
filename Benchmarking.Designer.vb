<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBenchmark
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
        Me.cmdRun = New System.Windows.Forms.Button()
        Me.lstResults = New System.Windows.Forms.ListView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbLength = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbRep = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cmdRun
        '
        Me.cmdRun.Location = New System.Drawing.Point(8, 9)
        Me.cmdRun.Name = "cmdRun"
        Me.cmdRun.Size = New System.Drawing.Size(84, 34)
        Me.cmdRun.TabIndex = 0
        Me.cmdRun.Text = "Run Test"
        Me.cmdRun.UseVisualStyleBackColor = True
        '
        'lstResults
        '
        Me.lstResults.Location = New System.Drawing.Point(209, 25)
        Me.lstResults.Name = "lstResults"
        Me.lstResults.Size = New System.Drawing.Size(275, 312)
        Me.lstResults.TabIndex = 1
        Me.lstResults.UseCompatibleStateImageBehavior = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(206, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Benchmarking Test Results:"
        '
        'cmbLength
        '
        Me.cmbLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLength.FormattingEnabled = True
        Me.cmbLength.Items.AddRange(New Object() {"100e+6", "500e+6", "1e+9", "10e+9"})
        Me.cmbLength.Location = New System.Drawing.Point(8, 86)
        Me.cmbLength.Name = "cmbLength"
        Me.cmbLength.Size = New System.Drawing.Size(116, 21)
        Me.cmbLength.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Test Length (Number of Cycles):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Number of Repetitions:"
        '
        'cmbRep
        '
        Me.cmbRep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRep.FormattingEnabled = True
        Me.cmbRep.Items.AddRange(New Object() {"10", "25", "100", "250", "1000"})
        Me.cmbRep.Location = New System.Drawing.Point(8, 134)
        Me.cmbRep.Name = "cmbRep"
        Me.cmbRep.Size = New System.Drawing.Size(116, 21)
        Me.cmbRep.TabIndex = 6
        '
        'frmBenchmark
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 349)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbRep)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbLength)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstResults)
        Me.Controls.Add(Me.cmdRun)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBenchmark"
        Me.Text = "Benchmark Processing Time Testing"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdRun As Button
    Friend WithEvents lstResults As ListView
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbLength As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbRep As ComboBox
End Class
