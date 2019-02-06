Public NotInheritable Class FinnishMobilePhoneNumber

    Private Const SKIP_FIRST_ZERO_CHARACTER As Integer = 1
    Private Const FINNISH_COUNTRY_CODE As String = "+358"

    Private ReadOnly m_number As String

    Public Sub New(number As String)

        If (Not IsValid(number)) Then

            Throw New ArgumentException("Given number is not valid Finnish consumer mobile number", NameOf(number))
        End If

        m_number = number

    End Sub


    Public Function GetFormattedNumber() As String

        Dim formatted As String = FormatPhoneNumber(m_number)
        If (formatted.StartsWith(FINNISH_COUNTRY_CODE)) Then

            Return formatted
        End If

        Return FINNISH_COUNTRY_CODE & formatted.Substring(SKIP_FIRST_ZERO_CHARACTER)

    End Function

    Public Shared Function IsValid(number As String) As Boolean

        If (String.IsNullOrWhiteSpace(number)) Then

            Return False
        End If

        If (Not ContainsOnlyAllowedCharacters(number)) Then

            Return False
        End If

        Dim formattedPhoneNumber As String = FormatPhoneNumber(number)
        If (Not IsValidAreaCode(formattedPhoneNumber)) Then

            Return False
        End If

        If (number.StartsWith(FINNISH_COUNTRY_CODE)) Then
            Dim finnishPhoneNumberWithoutCountryCode As String = formattedPhoneNumber.Substring(FINNISH_COUNTRY_CODE.Length)
            Return IsValidLength(finnishPhoneNumberWithoutCountryCode)
        End If

        Dim finnishPhoneNumberWithoutFirstZero As String = formattedPhoneNumber.Substring(SKIP_FIRST_ZERO_CHARACTER)
        Return IsValidLength(finnishPhoneNumberWithoutFirstZero)

    End Function

    Private Shared Function ContainsOnlyAllowedCharacters(number As String) As Boolean

        Dim formatter As New PhoneNumberFormat(number)
        Return formatter.ContainsOnlyAllowedCharacters()
    End Function

    Private Shared Function FormatPhoneNumber(number As String) As String

        Dim formatter As New PhoneNumberFormat(number)
        Return formatter.FormattedNumber()
    End Function

    Private Shared Function IsValidLength(number As String) As Boolean

        Const NumberMinimumLength As Integer = 5
        Const NumberMaximumLength As Integer = 12

        Return NumberMinimumLength <= number.Length AndAlso number.Length <= NumberMaximumLength
    End Function

    Public Shared Function IsValidAreaCode(phoneNumber As String) As Boolean

        If phoneNumber Is Nothing Then
            Return False
        End If

        Dim consumerAreaCodes As New List(Of String)() From {
                "04", "05", "+3584", "+3585"
        }
        Dim businessAreaCodes As New List(Of String)() From
        {
            "010", "020", "029", "030", "039", "071", "073", "075",
            "+35810", "+35820", "+35829", "+35830", "+35839", "+35871", "+35873", "+35875"
        }

        Return consumerAreaCodes.Any(Function(x) phoneNumber.StartsWith(x)) OrElse
               businessAreaCodes.Any(Function(x) phoneNumber.StartsWith(x))

    End Function


End Class
