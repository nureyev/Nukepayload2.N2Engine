﻿Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.Win32

Class MainWindow

    WithEvents gameHandler As MonoGameHandler

    Private Async Sub MainWindow_LoadedAsync(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        MonoImplRegistration.Register()
        gameHandler = New MonoGameHandler(Sub(ctl) winformHost.Child = ctl, Sub()
                                                                                Focus()
                                                                                Width += 1
                                                                            End Sub, New Nukepayload2.N2Engine.Foundation.SizeInInteger(CInt(ActualWidth), CInt(ActualHeight))) With {
                                                                                .IsMouseVisible = True
                                                                            }
        sparks = New MainCanvas
        Await sparks.LoadSceneAsync
        sparksRenderer = New GameCanvasRenderer(sparks, gameHandler)
        gameHandler.Run()
    End Sub

    Private Sub MainWindow_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        gameHandler.Exit()
        gameHandler.Dispose()
        End
    End Sub

    Dim sparks As MainCanvas
    Dim sparksRenderer As GameCanvasRenderer

    Private Sub BtnQuickSave_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnQuickLoad_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnBreakSave_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Close()
    End Sub

    Private Sub Rectangle_PreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

End Class