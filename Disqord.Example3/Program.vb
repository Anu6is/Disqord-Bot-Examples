Imports System.Threading
Imports System.Windows.Forms

Friend Class Program
    ''' <summary>
    '''     The main entry point for the application.
    ''' </summary>
    <STAThread>
    Shared Sub Main()
        Dim singleInstance As Boolean = False

        Using mutex As Mutex = New Mutex(True, "DisqordExample3", singleInstance)
            If singleInstance Then
                Application.EnableVisualStyles()
                Application.SetCompatibleTextRenderingDefault(False)
                Application.Run(New MainForm())
                mutex.ReleaseMutex()
            Else
                MessageBox.Show("Another instance of this application is currently active!")
            End If
        End Using
    End Sub
End Class