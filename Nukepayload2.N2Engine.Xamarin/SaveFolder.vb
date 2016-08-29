﻿Imports System.Text
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Platform

Namespace Global.Nukepayload2.N2Engine.Storage
    Public Class SaveFolder
        Inherits PlatformSaveDirectoryBase

        Dim curFolder As IDirectory
        Private Sub New(curFolder As String)
            Me.curFolder = PlatformActivator.CreateBaseInstance(Of IDirectory)(curFolder)
        End Sub
        ''' <summary>
        ''' 打开或创建存档文件夹
        ''' </summary>
        Public Shared Async Function CreateAsync(curFolder As String) As Task(Of PlatformSaveDirectoryBase)
            Dim dir As New SaveFolder(curFolder)
            If Not dir.curFolder.Exists Then
                Await dir.curFolder.CreateAsync
            End If
            Return dir
        End Function

        Public Overrides Async Function GetSaveFilesAsync() As Task(Of IEnumerable(Of SaveFile))
            Return From f In Await curFolder.EnumerateFilesAsync
                   Where f.ToLowerInvariant.EndsWith(".n2sav")
                   Select New SaveFile() With {.OriginalFileName = f}
        End Function

        Public Overrides Async Function LoadAsync(Of TData)(save As SaveFile(Of TData), decrypt As Func(Of Stream, Stream)) As Task
            Using strm = Await curFolder.OpenStreamForReadAsync(save.FileName), sr = New StreamReader(decrypt(strm), Encoding.Unicode)
                save.SaveData = JsonConvert.DeserializeObject(Of TData)((Await sr.ReadToEndAsync))
            End Using
        End Function

        Public Overrides Async Function SaveAsync(Of TData)(save As SaveFile(Of TData), encrypt As Func(Of Stream, Stream)) As Task
            Select Case save.Status
                Case SaveFileStatus.Loaded
                Case SaveFileStatus.Modified
                    Using strm = Await curFolder.OpenStreamForWriteAsync(save.FileName), sw = New StreamWriter(encrypt(strm), Encoding.Unicode)
                        Await sw.WriteAsync(JsonConvert.SerializeObject(save.SaveData))
                        Await sw.FlushAsync()
                    End Using
                Case Else
                    Throw New InvalidOperationException($"存档文件的状态{save.Status}导致无法存档")
            End Select
        End Function
    End Class
End Namespace