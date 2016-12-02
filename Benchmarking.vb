Imports System.Threading.Tasks

Public Class frmBenchmark
    Public NLoops As Long
    Public NReps As Long

    Private Sub frmBenchmark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inits the list
        lstResults.Columns.Clear()
        lstResults.Columns.Add("Operation", 125)
        lstResults.Columns.Add("Time per Operation", 150)
        lstResults.View = View.Details

        cmbLength.SelectedIndex = 0
        cmbRep.SelectedIndex = 0
    End Sub

    Private Sub RunTest(sender As Object, e As EventArgs) Handles cmdRun.Click
        'Runs the test and fills the list
        Try
            NLoops = CLng(Double.Parse(cmbLength.Text))
            NReps = CLng(Double.Parse(cmbRep.Text))
        Catch ex As Exception
            Exit Sub
        End Try

        If NLoops = 0 Or NReps = 0 Then Exit Sub

        lstResults.Items.Clear()
        Dim tOper As Double
        Dim varT As Double
        Dim item As ListViewItem

        ParallelMultiplication(tOper, varT, 1000)
        item = lstResults.Items.Add("par double*double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        ParallelDivision(tOper, varT, 1000)
        item = lstResults.Items.Add("par double/double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        ParallelExponentiation(tOper, varT, 1000)
        item = lstResults.Items.Add("par double^double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestAddSubtract(tOper, varT)
        item = lstResults.Items.Add("double+double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestMultiply(tOper, varT)
        item = lstResults.Items.Add("double*double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestDivide(tOper, varT)
        item = lstResults.Items.Add("double/double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestExpInteger(tOper, varT, 0)
        item = lstResults.Items.Add("double^0")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestExpInteger(tOper, varT, 1)
        item = lstResults.Items.Add("double^1")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestExpInteger(tOper, varT, 2)
        item = lstResults.Items.Add("double^2")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestExpInteger(tOper, varT, 10)
        item = lstResults.Items.Add("double^10")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestExpDouble(tOper, varT)
        item = lstResults.Items.Add("double^double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestIfLargerThan(tOper, varT)
        item = lstResults.Items.Add("if double>double")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestElseLargerThan(tOper, varT)
        item = lstResults.Items.Add("if > else")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestIfStringEqual(tOper, varT, 10)
        item = lstResults.Items.Add("if string10=string10")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        TestIfStringEqual(tOper, varT, 100)
        item = lstResults.Items.Add("if string100=string100")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        OrderedLookupArray(tOper, varT, 50)
        item = lstResults.Items.Add("binary search 50")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()

        OrderedLookupArray(tOper, varT, 100)
        item = lstResults.Items.Add("binary search 100")
        item.SubItems.Add(Trim(Str(Int(tOper * 100) / 100)) & "±" & Trim(Str(Int(varT * 100) / 100)) & " ns")
        lstResults.Update()


        MsgBox("Test completed successfully.")
    End Sub

    Private Sub TestAddSubtract(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()

            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = k1 + k2
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If
    End Sub

    Private Sub TestMultiply(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()

            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = k1 * k2
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If
    End Sub

    Private Sub TestDivide(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()

            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = k1 / k2
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If
    End Sub

    Private Sub TestExpInteger(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, exp As Integer)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        NLoops = NLoops / 10 'Performs 10 times less tests because this test is more lengthy
        For rep = 0 To NReps - 1
            k1 = Rnd()

            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = k1 ^ exp
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

        NLoops = NLoops * 10 'Recovers the original value of the variable

    End Sub

    Private Sub TestExpDouble(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        NLoops = NLoops / 10 'Performs 10 times less tests because this test is more lengthy
        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()

            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = k1 ^ k2
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

        NLoops = NLoops * 10 'Recovers the original value of the variable

    End Sub

    Private Sub TestIfLargerThan(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        For rep = 0 To NReps - 1
            k1 = 1250.2543
            k2 = -21345.98

            t1 = DateTime.Now()
            For i = 1 To NLoops
                If k1 > k2 Then
                    r = k1
                End If
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Sub TestElseLargerThan(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        For rep = 0 To NReps - 1
            k1 = 1250.2543
            k2 = -21345.98

            t1 = DateTime.Now()
            For i = 1 To NLoops
                If k2 > k1 Then
                    r = k1
                Else
                    r = k2
                End If
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Sub TestIfStringEqual(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, nChars As Long)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        Dim k1 As String
        Dim k2 As String
        Dim r As String

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        k1 = ""
        For i = 1 To nChars
            k1 = k1 & Chr(Int(Rnd() * 255) + 1)
        Next
        k2 = k1

        For rep = 0 To NReps - 1

            t1 = DateTime.Now()
            For i = 1 To NLoops
                If k1 = k2 Then
                    r = k1
                End If
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Sub OrderedLookupArray(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, nItems As Long)
        'gets the time of lookup in an array of nItems
        'creates the arrays
        Dim FromArray() As Double
        Dim ToArray() As Double

        ReDim FromArray(nItems)
        ReDim ToArray(nItems)

        For i = 0 To nItems - 1
            FromArray(i) = i
            ToArray(i) = Rnd()
        Next

        'Performs the test
        Dim r As Double
        Dim lookupvalue As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        Dim times() As Double
        ReDim times(NReps - 1)

        NLoops = NLoops / 100 'Performs 100 times less tests because this test is more lengthy
        For rep = 0 To NReps - 1
            lookupvalue = Rnd() * nItems
            t1 = DateTime.Now()
            For i = 1 To NLoops
                r = LookUpBinary(FromArray, ToArray, lookupvalue)
            Next

            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * 1000)) * 1000000000
        Next
        NLoops = NLoops * 100 'Recovers

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Function LookUpBinary(a1() As Double, a2() As Double, value As Double) As Double
        'This is what actually performs the lookup
        Dim L As Long = 0
        Dim R As Long = UBound(a1)
        Dim m As Long

        Do While 1
            If L > R Then Return 0

            m = Int((L + R) / 2)
            If m = L Then
                'We cannot resolve this any further
                Exit Do
            ElseIf a1(m) < value Then
                L = m
            ElseIf a1(m) > value Then
                R = m
            Else
            End If
        Loop

        Return a2(m)

    End Function

    Private Sub ParallelMultiplication(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, nNest As Long)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r(,) As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        'NLoops = NLoops * 10 'More loops because the parallelization is quite effective
        NLoops = NLoops / nNest 'Resize nLoops to nest in the parallel task
        ReDim r(NLoops - 1, nNest - 1)

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()
            t1 = DateTime.Now()

            Parallel.For(0, NLoops - 1,
                                  Sub(j)
                                      For p As Long = 0 To nNest - 1
                                          r(j, p) = k1 * k2
                                      Next
                                  End Sub)
            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * nNest * 1000)) * 1000000000
        Next
        NLoops = NLoops * nNest 'Resize nLoops to nest in the parallel task
        'NLoops = NLoops / 10 'Finally recovers it to the normal size

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Sub ParallelDivision(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, nNest As Long)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r(,) As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        'NLoops = NLoops * 10 'More loops because the parallelization is quite effective
        NLoops = NLoops / nNest 'Resize nLoops to nest in the parallel task
        ReDim r(NLoops - 1, nNest - 1)

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()
            t1 = DateTime.Now()

            Parallel.For(0, NLoops - 1,
                                  Sub(j)
                                      For p As Long = 0 To nNest - 1
                                          r(j, p) = k1 / k2
                                      Next
                                  End Sub)
            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * nNest * 1000)) * 1000000000
        Next
        NLoops = NLoops * nNest 'Resize nLoops to nest in the parallel task
        'NLoops = NLoops / 10 'Finally recovers it to the normal size

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub

    Private Sub ParallelExponentiation(ByRef t_oper_ns As Double, ByRef t_variance_ns As Double, nNest As Long)
        'Returns the time per operation and variability
        Dim rep As Long
        Dim i As Long
        Dim times() As Double
        ReDim times(NReps - 1)

        'Performs the test
        'Performs the test
        Dim k1 As Double
        Dim k2 As Double
        Dim r(,) As Double

        Dim t1 As DateTime
        Dim t2 As DateTime
        Dim dt As TimeSpan

        'NLoops = NLoops * 10 'More loops because the parallelization is quite effective
        NLoops = NLoops / nNest 'Resize nLoops to nest in the parallel task
        ReDim r(NLoops - 1, nNest - 1)

        For rep = 0 To NReps - 1
            k1 = Rnd()
            k2 = Rnd()
            t1 = DateTime.Now()

            Parallel.For(0, NLoops - 1,
                                  Sub(j)
                                      For p As Long = 0 To nNest - 1
                                          r(j, p) = k1 ^ k2
                                      Next
                                  End Sub)
            t2 = DateTime.Now()
            dt = t2 - t1

            times(rep) = (dt.TotalMilliseconds / (NLoops * nNest * 1000)) * 1000000000
        Next
        NLoops = NLoops * nNest 'Resize nLoops to nest in the parallel task
        'NLoops = NLoops / 10 'Finally recovers it to the normal size

        'Returns the stats
        Dim tmin As Double
        Dim tmax As Double

        t_oper_ns = 0
        tmax = 0
        tmin = 1000000
        For i = 0 To NReps - 1
            t_oper_ns = t_oper_ns + times(i)
            If times(i) < tmin Then tmin = times(i)
            If times(i) > tmax Then tmax = times(i)
        Next

        t_oper_ns = t_oper_ns / NReps
        If (tmax - t_oper_ns) > (t_oper_ns - tmin) Then
            t_variance_ns = (tmax - t_oper_ns)
        Else
            t_variance_ns = (t_oper_ns - tmin)
        End If

    End Sub
End Class
