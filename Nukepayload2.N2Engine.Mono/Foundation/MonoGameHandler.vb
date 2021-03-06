﻿Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information
Imports RaisingStudio.Xna.Graphics

#If ANDROID_APP Then
Imports Android.Views
#End If

Public Class MonoGameHandler
    Inherits Game

    Dim graphics As GraphicsDeviceManager
    Dim spriteBatch As SpriteBatch

    Public Event GameLoopStarting As TypedEventHandler(Of Game, Object)
    Public Event CreateResources As TypedEventHandler(Of Game, MonogameCreateResourcesEventArgs)
    Public Event GameLoopStopped As TypedEventHandler(Of Game, Object)
    Public Event Drawing As TypedEventHandler(Of Game, MonogameDrawEventArgs)
    Public Event Updating As TypedEventHandler(Of Game, MonogameUpdateEventArgs)

#If WINDOWS_DESKTOP Then
    ''' <summary>
    ''' Winform 互操作
    ''' </summary>
    Public ReadOnly Property GameForm As Windows.Forms.Form
    ''' <summary>
    ''' 对于 WPF 应用程序，使用这个构造函数可以轻松地让 WindowsFormsHost 承载游戏窗口。
    ''' </summary>
    ''' <param name="setChild">将 WindowsFormsHost 的 Child 属性设置为参数中的 Control</param>
    ''' <param name="focusResizeWpfWindow">调用 WPF 窗口的 Focus 方法, 并且 WPF 窗口的宽度 +1。</param>
    Sub New(setChild As Action(Of Windows.Forms.Control), focusResizeWpfWindow As Action, windowSize As SizeInInteger)
        MyClass.New
        BackBufferInformation.SetSize(windowSize)
        AddHandler graphics.PreparingDeviceSettings,
            Sub(sender, e)
                _GameForm = Windows.Forms.Control.FromHandle(e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle)
                GameForm.TopLevel = False
                GameForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                setChild(GameForm)
                focusResizeWpfWindow()
                BackBufferInformation.SetSize(New Foundation.SizeInInteger(GameForm.Width, GameForm.Height))
                AddHandler GameForm.SizeChanged,
                    Sub()
                        graphics.PreferredBackBufferHeight = GameForm.Height
                        graphics.PreferredBackBufferWidth = GameForm.Width
                        graphics.ApplyChanges()
                        BackBufferInformation.SetSize(New Foundation.SizeInInteger(GameForm.Width, GameForm.Height))
                        Debug.WriteLine($"分辨率重置： {GameForm.Width} x {GameForm.Height}")
                    End Sub
                GameForm.Width += 1
            End Sub
    End Sub

#End If

#If ANDROID_APP Then
    Public ReadOnly Property OpenGLView As View
        Get
            Return Services.GetService(GetType(View))
        End Get
    End Property
#End If

    Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
        graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft Or DisplayOrientation.LandscapeRight
    End Sub

    ''' <summary>
    ''' 在执行游戏之前初始化游戏逻辑。
    ''' 这里可以查询任何所需的服务和装载与图像无关的内容。
    ''' MyBase.Initialize 能够初始化基础组件。
    ''' </summary>
    Protected Overrides Sub Initialize()
        ' TODO: 初始化游戏逻辑
        GraphicsDeviceManagerExtension.SharedDevice = GraphicsDevice
        BackBufferInformation.SetDpi(96)
        Dim viewport = GraphicsDevice.Viewport
        BackBufferInformation.SetSize(New Foundation.SizeInInteger(viewport.Width, viewport.Height))
        RaiseEvent GameLoopStarting(Me, Nothing)

        MyBase.Initialize()
    End Sub

    ''' <summary>
    ''' 每个游戏中只会调用一次。用于装载游戏特定的资源。
    ''' </summary>
    Protected Overrides Sub LoadContent()
        ' 新建用于绘制纹理的 SpriteBatch
        spriteBatch = New SpriteBatch(GraphicsDevice)
        ' TODO: 使用 Me.Content 装载游戏内容
        RaiseEvent CreateResources(Me, New MonogameCreateResourcesEventArgs(GraphicsDevice))
    End Sub

    ''' <summary>
    ''' 每个游戏中只会调用一次。用于卸载游戏特定的资源。
    ''' </summary>
    Protected Overrides Sub UnloadContent()
        ' TODO: 卸载任何非 ContentManager 内容
        RaiseEvent GameLoopStopped(Me, Nothing)
    End Sub

    ''' <summary>
    ''' 允许游戏逻辑运行，例如更新世界，检测碰撞，收集输入 和 播放声音。
    ''' </summary>
    ''' <param name="timing">提供时间的快照</param>
    Protected Overrides Sub Update(timing As GameTime)
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed Then
#If iOS_APP Then
            Throw New PlatformNotSupportedException("iOS系统不支持使用代码退出应用")
#Else
            Me.Exit()
#End If
        End If
        ' TODO: 在此添加更新逻辑
        RaiseEvent Updating(Me, New MonogameUpdateEventArgs(timing))

        MyBase.Update(timing)
    End Sub

    ''' <summary>
    ''' 在游戏应该绘制的时候调用
    ''' </summary>
    ''' <param name="timing">提供时间的快照</param>
    Protected Overrides Sub Draw(timing As GameTime)
        MyBase.Draw(timing)

        ' TODO: 在此添加绘制代码
        RaiseEvent Drawing(Me, New MonogameDrawEventArgs(spriteBatch, timing))

    End Sub
End Class