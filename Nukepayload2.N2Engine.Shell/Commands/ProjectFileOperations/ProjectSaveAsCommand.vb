﻿Imports Nukepayload2.N2Engine.Shell.Models
Imports Nukepayload2.N2Engine.Shell.Utilities

Namespace Commands
    Public Class ProjectSaveAsCommand
        Implements ICommand

        WithEvents Model As N2EngineProject

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Async Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
            Try
                Await N2EngineProjectIO.PickNewFileAndSaveAsAsync(Model)
            Catch ex As Exception
                Model.ErrorMessage = ex.Message
            End Try
        End Sub

        Private Sub Model_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Model.PropertyChanged
            If e.PropertyName = NameOf(Model.IsBusy) Then
                RaiseEvent CanExecuteChanged(Me, e)
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Model = N2EngineProject.ActiveProject
            Return Not Model.IsBusy
        End Function
    End Class
End Namespace
