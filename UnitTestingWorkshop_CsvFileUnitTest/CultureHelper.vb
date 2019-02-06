Imports System.Globalization
Imports System.Threading

Public Class CultureHelper
    Implements IDisposable

    Private m_originalCulture As CultureInfo

    Public Sub New(ByVal useCulture As CultureInfo)

        m_originalCulture = Thread.CurrentThread.CurrentCulture
        Thread.CurrentThread.CurrentCulture = useCulture
        Thread.CurrentThread.CurrentUICulture = useCulture

    End Sub

    Public Shared Function TestCulture() As CultureHelper

        Dim culture As New CultureInfo("fi-FI")
        With culture.DateTimeFormat

            .ShortDatePattern = "d.M.yyyy"
            .LongDatePattern = "dd.MM.yyyy"
            .DateSeparator = "."

            .ShortTimePattern = "H:m:s"
            .LongTimePattern = "HH:mm:ss"
            .TimeSeparator = ":"

        End With

        culture.NumberFormat.NumberDecimalSeparator = ","

        Return New CultureHelper(culture)

    End Function



#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Thread.CurrentThread.CurrentCulture = m_originalCulture
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region


End Class
