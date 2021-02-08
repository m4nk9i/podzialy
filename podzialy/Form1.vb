Public Class Form1

    Private graf As Graphics
    Dim szerokosc_ark As Single
    Dim wysokosc_ark As Single
    Dim szerokosc_uz As Single
    Dim wysokosc_uz As Single
    Dim wycinki As Single
    Dim marg_gora As Single
    Dim marg_lewa As Single
    Dim marg_dol As Single
    Dim marg_prawa As Single

    Dim pole_wys As Single
    Dim pole_szer As Single
    Dim liczba_wersow1 As Integer
    Dim liczba_kolumn1 As Integer
    Dim liczba_wersow2 As Integer
    Dim liczba_kolumn2 As Integer
    Dim skala_rys As Integer = 65





    Dim formaty_pap = {
        {"297", "210", "A4"},
        {"350", "250", "B4"},
        {"430", "305", "A3"},
        {"500", "350", "B3"},
        {"610", "430", "A2"},
        {"630", "440", "A2+"},
        {"640", "450", "C2"},
        {"640", "460", "C2 większe"},
        {"660", "485", "MAX"},
        {"610", "286", "kiszka 610"},
        {"650", "350", "kiszka 650"},
        {"660", "330", "kiszka 660"},
        {"700", "500", "B2"}
    }

    Dim formaty_uz = {
    {"90", "50", "wizytowka"},
    {"210", "148", "A5"},
    {"297", "210", "A4"},
    {"250", "175", "B5"},
    {"350", "250", "B4"},
    {"210", "99", "DL"}
    }

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        For index0 = 0 To formaty_pap.GetUpperBound(0)
            ComboBox1.Items.Add(formaty_pap(index0, 2) + "(" + formaty_pap(index0, 0) + "x" + formaty_pap(index0, 1) + ")")

        Next


        For index0 = 0 To formaty_uz.GetUpperBound(0)
            ComboBox2.Items.Add(formaty_uz(index0, 2) + "(" + formaty_uz(index0, 0) + "x" + formaty_uz(index0, 1) + ")")

        Next

        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0

        graf = Panel1.CreateGraphics
        Button1.PerformClick()

        'policz()
        'rysuj(graf, 1)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        MaskedTextBox1.Text = formaty_pap(ComboBox1.SelectedIndex, 0)
        MaskedTextBox2.Text = formaty_pap(ComboBox1.SelectedIndex, 1)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        MaskedTextBox3.Text = formaty_uz(ComboBox2.SelectedIndex, 0)
        MaskedTextBox4.Text = formaty_uz(ComboBox2.SelectedIndex, 1)


    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub

    Private Sub prostokat(g As Graphics, kol As Pen, px As Single, py As Single, pw As Single, ph As Single, dod_sk As Single)
        Dim skala As Single = dod_sk * skala_rys / 100.0
        g.DrawRectangle(kol, (px * skala), (py * skala), (pw * skala), (ph * skala))
        'graf.DrawRectangle(kol, CInt(px * skala), CInt(py * skala), CInt(pw * skala), CInt(ph * skala))

    End Sub

    Private Sub policz()

        szerokosc_ark = CDbl(MaskedTextBox1.Text)
        wysokosc_ark = CDbl(MaskedTextBox2.Text)
        szerokosc_uz = CDbl(MaskedTextBox3.Text)
        wysokosc_uz = CDbl(MaskedTextBox4.Text)
        wycinki = CDbl(MaskedTextBox5.Text)
        marg_gora = CDbl(MaskedTextBox6.Text)
        marg_lewa = CDbl(MaskedTextBox8.Text)
        marg_dol = CDbl(MaskedTextBox7.Text)
        marg_prawa = CDbl(MaskedTextBox9.Text)


        pole_wys = wysokosc_ark - marg_dol - marg_gora
        pole_szer = szerokosc_ark - marg_lewa - marg_prawa
        liczba_wersow1 = Math.Floor((pole_wys + wycinki) / (wysokosc_uz + wycinki))
        liczba_kolumn1 = Math.Floor((pole_szer + wycinki) / (szerokosc_uz + wycinki))
        liczba_wersow2 = Math.Floor((pole_wys + wycinki) / (szerokosc_uz + wycinki))
        liczba_kolumn2 = Math.Floor((pole_szer + wycinki) / (wysokosc_uz + wycinki))

        TextBox1.AppendText("=================" + System.Environment.NewLine)
        TextBox1.AppendText("wyliczenia dla arkusza " + CStr(szerokosc_ark) + " x " + CStr(wysokosc_ark) + System.Environment.NewLine)
        TextBox1.AppendText("pole zadruku z uzglednieniem marginesow: " + CStr(pole_szer) + " x " + CStr(pole_wys) + System.Environment.NewLine)
        TextBox1.AppendText("uzytek " + CStr(szerokosc_uz) + " x " + CStr(wysokosc_uz) + " wycinki " + CStr(wycinki) + System.Environment.NewLine)
        TextBox1.AppendText("liczba uzytkow " + CStr(liczba_kolumn1) + " x " + CStr(liczba_wersow1) + " = " + CStr(liczba_kolumn1 * liczba_wersow1) + System.Environment.NewLine)
        TextBox1.AppendText("liczba uzytkow " + CStr(liczba_kolumn2) + " x " + CStr(liczba_wersow2) + " = " + CStr(liczba_kolumn2 * liczba_wersow2) + System.Environment.NewLine)
        TextBox1.AppendText(System.Environment.NewLine + System.Environment.NewLine)


    End Sub

    Private Sub rysuj(g As Graphics, dodsk As Single)
        graf.Clear(Color.White)

        prostokat(g, Pens.Black, 0, 0, szerokosc_ark, wysokosc_ark, dodsk)

        prostokat(g, Pens.Black, 0, (wysokosc_ark), (szerokosc_ark), CInt(wysokosc_ark), dodsk)
        prostokat(g, Pens.Green, marg_lewa, marg_gora, szerokosc_ark - marg_prawa - marg_lewa, wysokosc_ark - marg_dol - marg_gora, dodsk)
        prostokat(g, Pens.Green, marg_lewa, wysokosc_ark + marg_gora, szerokosc_ark - marg_prawa - marg_lewa, wysokosc_ark - marg_dol - marg_gora, dodsk)

        If ((liczba_kolumn1 > 0) And (liczba_wersow1 > 0)) Then
            Dim startx = marg_lewa + (pole_szer - (liczba_kolumn1 * szerokosc_uz + (liczba_kolumn1 - 1) * wycinki)) / 2
            Dim starty = marg_gora + (pole_wys - (liczba_wersow1 * wysokosc_uz + (liczba_wersow1 - 1) * wycinki)) / 2
            For i As Integer = 0 To liczba_kolumn1 - 1
                For j As Integer = 0 To liczba_wersow1 - 1
                    prostokat(g, Pens.Black, CInt(startx + i * (szerokosc_uz + wycinki)), CInt(starty + j * (wysokosc_uz + wycinki)), szerokosc_uz, wysokosc_uz, dodsk)
                Next j
            Next i
        End If


        If ((liczba_kolumn2 > 0) And (liczba_wersow2 > 0)) Then
            Dim startx = marg_lewa + (pole_szer - (liczba_kolumn2 * wysokosc_uz + (liczba_kolumn2 - 1) * wycinki)) / 2
            Dim starty = wysokosc_ark + marg_gora + (pole_wys - (liczba_wersow2 * szerokosc_uz + (liczba_wersow2 - 1) * wycinki)) / 2
            For i As Integer = 0 To liczba_kolumn2 - 1
                For j As Integer = 0 To liczba_wersow2 - 1
                    prostokat(g, Pens.Black, (startx + i * (wysokosc_uz + wycinki)), (starty + j * (szerokosc_uz + wycinki)), wysokosc_uz, szerokosc_uz, dodsk)
                Next j
            Next i
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        policz()
        Dim sk1 = Panel1.Height / (2 * wysokosc_ark)
        Dim sk2 = Panel1.Width / szerokosc_ark
        If (sk1 > sk2) Then
            TrackBar1.Value = sk2 * 100
        Else
            TrackBar1.Value = sk1 * 100
        End If
        skala_rys = TrackBar1.Value
        rysuj(graf, 1)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        rysuj(graf, 1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MaskedTextBox6.Text = "0"
        MaskedTextBox7.Text = "0"
        MaskedTextBox8.Text = "0"
        MaskedTextBox9.Text = "0"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MaskedTextBox6.Text = "10"
        MaskedTextBox7.Text = "15"
        MaskedTextBox8.Text = "10"
        MaskedTextBox9.Text = "10"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MaskedTextBox5.Text = "0"

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MaskedTextBox5.Text = "4"
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        skala_rys = TrackBar1.Value


        rysuj(graf, 1)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        PrintPreviewDialog1.ShowDialog()

        rysuj(graf, 1)
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim fon As Font
        fon = New Drawing.Font("Arial", 12)

        rysuj(e.Graphics, 1.75)
        e.Graphics.DrawString("Format arkusza " + CStr(szerokosc_ark) + " x " + CStr(wysokosc_ark), fon, Brushes.Black, 20, 20)
        e.Graphics.DrawString("pole zadruku " + CStr(pole_szer) + " x " + CStr(pole_wys), fon, Brushes.Black, 20, 40)
        e.Graphics.DrawString("uzytek " + CStr(szerokosc_uz) + " x " + CStr(wysokosc_uz) + " wycinki " + CStr(wycinki), fon, Brushes.Black, 20, 60)
        e.Graphics.DrawString("PODZIAŁ A " + CStr(liczba_kolumn1) + " x " + CStr(liczba_wersow1) + " = " + CStr(liczba_kolumn1 * liczba_wersow1), fon, Brushes.Black, 20, 80)
        e.Graphics.DrawString("PODZIAŁ B " + CStr(liczba_kolumn2) + " x " + CStr(liczba_wersow2) + " = " + CStr(liczba_kolumn2 * liczba_wersow2), fon, Brushes.Black, 20, 100)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        PrintDialog1.ShowDialog()
    End Sub
End Class
