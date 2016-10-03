﻿Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示一个合成的二维变换
    ''' </summary>
    Public Class CompositeTransform
        Inherits PlaneTransform

        Public ReadOnly Property Translate As New PropertyBinder(Of Vector2)
        Public ReadOnly Property Rotate As New PropertyBinder(Of Single)
        Public ReadOnly Property Skew As New PropertyBinder(Of Vector2)
        Public ReadOnly Property Scale As New PropertyBinder(Of Vector2)

        Public Overrides Function GetTransformMatrix() As Matrix3x2
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace