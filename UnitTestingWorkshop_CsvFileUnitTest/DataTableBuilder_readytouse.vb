

Namespace readytousebuilder

    Public Class DataTableBuilder

        Private m_columnNames As New List(Of String)
        Private m_listOfRowValues As New List(Of List(Of Object))
        Private m_currentRowValues As New List(Of Object)

        Public Function withColumnName(ByVal name As String) As DataTableBuilder
            m_columnNames.Add(name)
            Return Me
        End Function

        Public Function withRowValue(ByVal value As Object) As DataTableBuilder
            m_currentRowValues.Add(value)
            Return Me
        End Function

        Public Function withNewRow() As DataTableBuilder

            m_listOfRowValues.Add(m_currentRowValues)
            m_currentRowValues = New List(Of Object)

            Return Me

        End Function

        Public Function build() As DataTable

            Dim table As New DataTable()

            For Each columnName As String In m_columnNames
                table.Columns.Add(New DataColumn(columnName))
            Next

            If m_currentRowValues.Any() Then
                m_listOfRowValues.Add(m_currentRowValues)
            End If

            For Each rowValues As List(Of Object) In m_listOfRowValues

                Dim row As DataRow = table.NewRow()
                row.ItemArray = rowValues.ToArray()

                table.Rows.Add(row)

            Next

            Return table

        End Function

        Public Shared Widening Operator CType(b As DataTableBuilder) As DataTable
            Return b.build()
        End Operator

        Public Shared Operator +(a As DataTableBuilder, b As DataTableBuilder) As DataTableBuilder
            a.m_columnNames.AddRange(b.m_columnNames)
            a.m_currentRowValues.AddRange(b.m_currentRowValues)

            Return a
        End Operator


    End Class

End Namespace