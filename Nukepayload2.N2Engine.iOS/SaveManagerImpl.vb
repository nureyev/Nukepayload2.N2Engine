﻿Imports Foundation
Namespace Global.Nukepayload2.N2Engine.Storage
    Friend Class SaveManagerImpl
        Public Overrides Async Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
            Dim folder As String
            Dim localData = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last
            Select Case Location
                Case SaveLocations.Local
                    folder = localData
                Case SaveLocations.Roaming
                    Throw New PlatformNotSupportedException("iOS 不支持 iCloud 漫游应用数据")
                Case Else
                    Throw New PlatformNotSupportedException("iOS 不支持 开发商共享数据")
            End Select
            Return Await SaveFolder.CreateAsync(folder)
        End Function
    End Class
End Namespace