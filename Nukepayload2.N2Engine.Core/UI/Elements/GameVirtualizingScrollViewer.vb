﻿Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Elements
    ''' <summary>
    ''' 用于滚动查看游戏对象。如果不使用场景，则建议使用此元素作为游戏对象树的根。
    ''' </summary>
    Public Class GameVirtualizingScrollViewer
        Inherits GameVisualContainer
        ''' <summary>
        ''' 内部元素的位置偏移。如果不绑定值则不偏移对待。
        ''' </summary>
        Public ReadOnly Property Offset As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 缩放比例。如果不绑定值则按 1 对待。
        ''' </summary>
        Public ReadOnly Property Zoom As New PropertyBinder(Of Single)
        ''' <summary>
        ''' 被通知正在卷动时引发此事件。
        ''' </summary>
        Public Event Scrolling As GameObjectEventHandler(Of GameVirtualizingScrollViewer, EventArgs)
        ''' <summary>
        ''' 显式通知此视图正在卷动
        ''' </summary>
        Public Sub RaiseScrolling(e As EventArgs)
            RaiseEvent Scrolling(Me, e)
        End Sub
    End Class

End Namespace
