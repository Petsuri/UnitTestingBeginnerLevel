Imports System.Text.RegularExpressions

Public NotInheritable Class PhoneNumberFormat

    Private ReadOnly m_number As String

    Public Sub New(number As String)

        m_number = number
    End Sub


    Public Function FormattedNumber() As String

        If (m_number Is Nothing) Then

            Return String.Empty
        End If
        Return FormatNumber()
    End Function

    Private Function FormatNumber() As String

        Const InternationalCallPrefix As String = "00"
        Const SkipInternationalCallPrefixIndex As Integer = 2
        Const RemoveExtraCharactersPattern As String = "[\s-()]"

        Dim formatted As String = Regex.Replace(m_number, RemoveExtraCharactersPattern, String.Empty)
        If (formatted.StartsWith(InternationalCallPrefix)) Then

            Return "+" + formatted.Substring(SkipInternationalCallPrefixIndex)
        End If
        Return formatted
    End Function

    Public Function ContainsOnlyAllowedCharacters() As Boolean

        If (m_number Is Nothing) Then

            Return True
        End If

        Const SearchLettersPattern As String = "[^\s-()+0-9]+"
        Return Not Regex.IsMatch(m_number, SearchLettersPattern)
    End Function

End Class