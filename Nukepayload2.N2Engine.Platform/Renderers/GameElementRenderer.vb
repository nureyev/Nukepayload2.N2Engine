﻿Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Partial Public Class GameElementRenderer(Of T As GameElement)
    ''' <summary>
    ''' 警告：如果你的渲染器使用了 <see cref="PlatformImplAttribute"/> 标注它属于哪个 <see cref="GameVisual"/>, 则不要显式调用这个构造函数。因为游戏元素渲染器会被相应的游戏元素自动创建。
    ''' </summary>
    Sub New(view As T)
        MyBase.New(view)
        AddHandler view.HandleRendererRequested, AddressOf OnHandleRendererRequested
        AddHandler view.RendererUnloadRequested, AddressOf OnRendererUnhandleRequested
    End Sub

    Private Sub OnRendererUnhandleRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
#If WIN2D Then
        With DirectCast(e.ParentRenderer, GameCanvasRenderer)
            RemoveHandler .Draw, AddressOf OnDraw
            RemoveHandler .Update, AddressOf OnUpdate
#Else
        With DirectCast(e.ParentRenderer, GameCanvasRenderer).Game
            RemoveHandler .Drawing, AddressOf OnDraw
            RemoveHandler .Updating, AddressOf OnUpdate
#End If
            RemoveHandler .CreateResources, AddressOf OnCreateResources
            RemoveHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            RemoveHandler .GameLoopStopped, AddressOf OnGameLoopStopped
        End With
    End Sub

    Private Sub OnHandleRendererRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
#If WIN2D Then
        With DirectCast(e.ParentRenderer, GameCanvasRenderer)
            AddHandler .Draw, AddressOf OnDraw
            AddHandler .Update, AddressOf OnUpdate
#Else
        With DirectCast(e.ParentRenderer, GameCanvasRenderer).Game
            AddHandler .Drawing, AddressOf OnDraw
            AddHandler .Updating, AddressOf OnUpdate
#End If
            AddHandler .CreateResources, AddressOf OnCreateResources
            AddHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            AddHandler .GameLoopStopped, AddressOf OnGameLoopStopped
        End With
    End Sub

    Public Overrides Sub DisposeResources()
        RemoveHandler View.HandleRendererRequested, AddressOf OnHandleRendererRequested
        RemoveHandler View.RendererUnloadRequested, AddressOf OnRendererUnhandleRequested
    End Sub
End Class