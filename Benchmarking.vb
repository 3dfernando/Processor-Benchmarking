Public Class frmBenchmark
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdRun.Click
        'Dim n As Integer = 10000000
        'Dim i As Integer
        'Dim a(), b(), c(), d() As Double
        'ReDim a(n - 1)
        'ReDim b(n - 1)
        'ReDim c(n - 1)
        'ReDim d(n - 1)

        'For i = 0 To n - 1
        '    a(i) = Rnd()
        '    b(i) = Rnd()
        '    c(i) = Rnd()
        '    d(i) = Rnd()
        'Next

        'Solve_Tridiag(a, b, c, d)

        Dim k1 As Double = 0.4
        Dim k2 As Double = Rnd()
        Dim r As Double
        Dim i As Long
        Dim N As Long = 100000000
        Dim j() As Double
        Dim b As Boolean = False

        Dim t1 As DateTime = DateTime.Now() '<<Used to measure time performance>>
        ReDim j(N)

        For i = 1 To N
            If k1 > 0.5 Then
                r = k1 * k1
            Else
                r = k1
            End If
        Next

        Dim t2 As DateTime = DateTime.Now() '<<Used to measure time performance>>
        Dim dt As TimeSpan = t2 - t1 '<<Used to measure time performance>>

        MsgBox(Trim(Str((dt.TotalMilliseconds / (N * 1000)) * 1000000000)) & " ns")
    End Sub
End Class
